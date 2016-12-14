using System.Collections.Generic;
using Newtonsoft.Json;

namespace BrawlhallaNet.Responses
{
    public class RankedPlayer
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("brawlhalla_id")]
        public ulong BrawlhallaId { get; set; }
        [JsonProperty("rating")]
        public int Elo { get; set; }
        [JsonProperty("peak_rating")]
        public int PeakElo { get; set; }
        [JsonProperty("tier")]
        public string Tier { get; set; }
        [JsonProperty("wins")]
        public int Wins { get; set; }
        [JsonProperty("games")]
        public int Games { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("global_rank")]
        public int GlobalRank { get; set; }
        [JsonProperty("region_rank")]
        public int RegionalRank { get; set; }
        [JsonProperty("legends")]
        public List<RankedLegend> Legends { get; set; }
        [JsonProperty("2v2")]
        public List<Ranked2sTeam> RankedTeams { get; set; }
    }

    public class RankedLegend
    {
        [JsonProperty("legend_id")]
        public int ID { get; set; }
        [JsonProperty("legend_name_key")]
        public string Name { get; set; }
        [JsonProperty("rating")]
        public int Elo { get; set; }
        [JsonProperty("peak_rating")]
        public int PeakElo { get; set; }
        [JsonProperty("tier")]
        public string Tier { get; set; }
        [JsonProperty("wins")]
        public int Wins { get; set; }
        [JsonProperty("games")]
        public int Games { get; set; }
    }

    public class Ranked2sTeam
    {
        [JsonProperty("brawlhalla_id_one")]
        public ulong PlayerOneID { get; set; }
        [JsonProperty("brawlhalla_id_two")]
        public ulong PlayerTwoID { get; set; }
        [JsonProperty("rating")]
        public int Elo { get; set; }
        [JsonProperty("peak_rating")]
        public int PeakElo { get; set; }
        [JsonProperty("tier")]
        public string Tier { get; set; }
        [JsonProperty("wins")]
        public int Wins { get; set; }
        [JsonProperty("games")]
        public int Games { get; set; }
        [JsonProperty("teamname")]
        public string TeamName { get; set; }
        [JsonProperty("global_rank")]
        public int GlobalRank { get; set; }
    }
}
