using BrawlhallaNet.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrawlhallaNet
{
    public class BrawlhallaApiClient : IDisposable
    {
        /// <summary>
        /// All values from GetLegendInfoAsync() will be cached here. Call GetLegendInfoAsync() with useCache to false to update values.
        /// </summary>
        public Dictionary<Constants.Legends, Legend> LegendCache = new Dictionary<Constants.Legends, Legend>();

        /// <summary>
        /// Subscribe to this event for debug information
        /// </summary>
        public EventHandler<LogEventArgs> Log = delegate { };

        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient _httpClient = new HttpClient();
        private string _apiKey;

        // As of 12/11
        // Ratelimits are:
        // 180 per 15 minutes
        // 10 per second
        private int requestsPer15Minutes = 180;
        private int requestsPer1Second = 10;

        private List<DateTime> requestsWithin15Minutes = new List<DateTime>();
        private List<DateTime> requestsWithin1Second = new List<DateTime>();

        /// <summary>
        /// Initializes a new client from an apiKey. If you have different ratelimits from the default 180/15 and 10/1, use the other constructor.
        /// </summary>
        /// <param name="apiKey">The API key to make requests with.</param>
        public BrawlhallaApiClient(string apiKey)
        {
            this._apiKey = apiKey ?? throw new ArgumentNullException("apiKey", "The API key cannot be null.");
        }

        /// <summary>
        /// Initializes a new client with an api key and custom ratelimits. If you do not have custom ratelimits, use the other constructor.
        /// </summary>
        /// <param name="apiKey">The API key to make requests with.</param>
        /// <param name="requestsPer15Minutes">The amount of requests allowed within 15 minutes.</param>
        /// <param name="requestsPer1Second">The amount of requests allowed within 1 second.</param>
        public BrawlhallaApiClient(string apiKey, int requestsPer15Minutes, int requestsPer1Second) : this(apiKey)
        {
            this.requestsPer15Minutes = requestsPer15Minutes;
            this.requestsPer1Second = requestsPer1Second;
        }

        /// <summary>
        /// Gets a brawlhalla id and name from a steam id.
        /// </summary>
        /// <param name="steamId">The steam id of the player to get.</param>
        /// <returns>A SearchedPlayer with the brawlhalla id and name from the steam id.</returns>
        public async Task<SearchedPlayer> GetPlayerFromSteamIdAsync(ulong steamId)
        {
            return await GetResponseAsync<SearchedPlayer>($"{Constants.BaseEndpoint}/search?steamid={steamId}/");
        }

        /// <summary>
        /// Sends a GET request for a ranked page.
        /// </summary>
        /// <param name="bracket">The bracket to get, must be either 1v1 or 2v2.</param>
        /// <param name="region">The reigon to get, must be one of us-e, us-w, eu, sea, brz, or aus.</param>
        /// <param name="page">The page number to get, defaults to 1.</param>
        /// <param name="name">The optional player name to search for.</param>
        /// <returns>A list of players found.</returns>
        public async Task<List<RankedPagePlayer>> GetRankedPageAsync(string bracket, string region, int page = 1, string name = null)
        {
            if (bracket != "1v1" && bracket != "2v2")
            {
                throw new ArgumentException("Bracket must be either 1v1 or 2v2.", bracket);
            }
            else if (region != "us-e" && region != "us-w" && region != "eu" && region != "sea" && region != "brz" && region != "aus" && region != "all")
            {
                throw new ArgumentException("Region must be one of us-e, us-w, eu, sea, brz, or aus.", region);
            }

            return await GetResponseAsync<List<RankedPagePlayer>>($"{Constants.BaseEndpoint}/rankings/{bracket}/{region}/{page}/" + (name == null ? "" : $"?name={name}/"));
        }

        /// <summary>
        /// Gets general stats for a player from an id. See GetRankedPlayerAsync() for ranked stats.
        /// </summary>
        /// <param name="playerId">The brawlhalla id of the player to get general information for.</param>
        /// <returns>A Player object that represents the player.</returns>
        public async Task<Player> GetPlayerAsync(ulong playerId)
        {
            return await GetResponseAsync<Player>($"{Constants.BaseEndpoint}/player/{playerId}/stats");
        }

        /// <summary>
        /// Gets ranked stats for a player from an id. See GetPlayerAsync() for general stats.
        /// </summary>
        /// <param name="playerId">The brawlhalla id of the player to get ranked information for.</param>
        /// <returns>A RankedPlayer object that represents the player.</returns>
        public async Task<RankedPlayer> GetRankedPlayerAsync(ulong playerId)
        {
            return await GetResponseAsync<RankedPlayer>($"{Constants.BaseEndpoint}/player/{playerId}/ranked");
        }

        /// <summary>
        /// Returns a clan object from the clan id. There is currently no way to search clans by name.
        /// </summary>
        /// <param name="clanId">The id of the clan to get information for.</param>
        /// <returns>A clan object that represents the clan id.</returns>
        public async Task<Clan> GetClanAsync(ulong clanId)
        {
            return await GetResponseAsync<Clan>($"{Constants.BaseEndpoint}/clan/{clanId}");
        }

        /// <summary>
        /// Gets info for a legend.
        /// </summary>
        /// <param name="legend">THe legend to get information for.</param>
        /// <param name="useCache">Whether or not we should use the cached value.</param>
        /// <returns>Information for the legend.</returns>
        public async Task<Legend> GetLegendInfoAsync(Constants.Legends legend, bool useCache = true)
        {
            if (useCache)
            {
                Legend cachedLegend;
                if (LegendCache.TryGetValue(legend, out cachedLegend))
                {
                    return cachedLegend;
                }
            }

            var leg = await GetResponseAsync<Legend>($"{Constants.BaseEndpoint}/legend/{(int)legend}/");
            LegendCache[legend] = leg;
            return leg;
        }


        /// <summary>
        /// Sends a GET request to the specified url. Automatically authorizes using the provided api key.
        /// </summary>
        /// <typeparam name="T">The response to automatically deserialize to. Valid types are in BrawlhallaNet.Responses.</typeparam>
        /// <param name="url">The url to make a GET request to.</param>
        /// <returns>The object with the specified type from the request.</returns>
        public async Task<T> GetResponseAsync<T>(string url)
        {
            await HandleRateLimits();

            url += $"&api_key={_apiKey}";
            Log.Invoke(this, new LogEventArgs($"Sending GET request for {url}...", null));

            var data = await _httpClient.GetAsync(url);

            if (!data.IsSuccessStatusCode)
            {
                Log.Invoke(this, new LogEventArgs($"GET request for {url} returned an error of {data.StatusCode}.", null));
                throw new BrawlhallaNetException((int)data.StatusCode, data.ReasonPhrase);
            }

            using (var streamReader = new StreamReader(await data.Content.ReadAsStreamAsync(), encoding: Encoding.UTF8))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var result = _serializer.Deserialize<T>(jsonReader);
                    Log.Invoke(this, new LogEventArgs($"GET request for {url} returned an object of type {result.GetType()}.", result));
                    return result;
                }
            }
        }

        /// <summary>
        /// Sends a GET request to the specified url. Automatically authorizes using the provided api key.
        /// </summary>
        /// <param name="url">The url to make a GET request to.</param>
        /// <returns>The contents of the GET request as a string.</returns>
        public async Task<string> GetResponseAsync(string url)
        {
            await HandleRateLimits();

            var data = await _httpClient.GetStreamAsync(url + $"&api_key={_apiKey}");
            using (var streamReader = new StreamReader(data, encoding: Encoding.UTF8))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        private async Task HandleRateLimits()
        {
            requestsWithin15Minutes.Add(DateTime.Now);
            requestsWithin1Second.Add(DateTime.Now);

            // Remove expired requests
            for (int i = requestsWithin15Minutes.Count - 1; i > -1; i--)
            {
                if ((DateTime.Now - requestsWithin15Minutes[i]).TotalMinutes > 15)
                {
                    requestsWithin15Minutes.RemoveAt(i);
                }
            }
            for (int i = requestsWithin1Second.Count - 1; i > -1; i--)
            {
                if ((DateTime.Now - requestsWithin1Second[i]).TotalSeconds > 1)
                {
                    requestsWithin1Second.RemoveAt(i);
                }
            }

            if (requestsWithin1Second.Count >= requestsPer1Second)
            {
                Log.Invoke(this, new LogEventArgs("Preemptive rate limit hit! Sleeping for 1 second.", null));
                await Task.Delay(1000);
                // Request will get removed the next time HandleRateLimits() gets called
            }
            if (requestsWithin15Minutes.Count > requestsPer15Minutes)
            {
                var request = requestsWithin15Minutes[0]; // The request that was first sent
                var timeDifference = (int)(15 - (DateTime.Now - request).TotalMinutes) + 1; // Add an additional minute to make up for int truncation and just to be sure.
                Log.Invoke(this, new LogEventArgs($"Preemptive rate limit hit! Sleeping for {timeDifference} minutes.", null));
                await Task.Delay(timeDifference * 1000 * 60);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
