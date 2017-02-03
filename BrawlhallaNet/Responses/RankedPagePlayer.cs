using Newtonsoft.Json;

namespace BrawlhallaNet.Responses
{
    public class RankedPagePlayer
    {
        [JsonProperty("rank")]
        public string Rank { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("brawlhalla_id")]
        public ulong ID { get; set; }
        [JsonProperty("best_legend")]
        public int BestLegend { get; set; }
        [JsonProperty("best_legend_games")]
        public int BestLegendGames { get; set; }
        [JsonProperty("best_legend_wins")]
        public int BestLegendWins { get; set; }
        [JsonProperty("rating")]
        public int Elo { get; set; }
        [JsonProperty("tier")]
        public string Tier { get; set; }
        [JsonProperty("games")]
        public int Games { get; set; }
        [JsonProperty("wins")]
        public int Wins { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("peak_rating")]
        public int PeakElo { get; set; }

        // 2v2 properties
        [JsonProperty("teamname")]
        public string TeamName { get; set; }
        [JsonProperty("brawlhalla_id_one")]
        public ulong PlayerOneID { get; set; }
        [JsonProperty("brawlhalla_id_two")]
        public ulong PlayerTwoID { get; set; }
    }
}
