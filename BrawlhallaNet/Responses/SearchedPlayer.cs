using Newtonsoft.Json;

namespace BrawlhallaNet.Responses
{
    public class SearchedPlayer
    {
        [JsonProperty("brawlhalla_id")]
        public ulong BrawlhallaID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
