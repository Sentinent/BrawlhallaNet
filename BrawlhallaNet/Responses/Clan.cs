using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BrawlhallaNet.Responses
{
    public class Clan
    {
        [JsonProperty("clan_id")]
        public ulong ID { get; set; }
        [JsonProperty("clan_name")]
        public string Name { get; set; }
        [JsonProperty("clan_create_date")]
        public long ClanCreationDateUnix { get; set; }
        public DateTimeOffset ClanCreationDate => DateTimeOffset.FromUnixTimeSeconds(ClanCreationDateUnix);
        [JsonProperty("clan_xp")]
        public string ClanXP { get; set; }
        [JsonProperty("clan")]
        public List<ClanMember> Members { get; set; }
    }

    public class ClanMember
    {
        [JsonProperty("brawlhalla_id")]
        public ulong ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("rank")]
        public string Rank { get; set; }
        [JsonProperty("join_date")]
        public long JoinDateUnix { get; set; }
        public DateTimeOffset JoinDate => DateTimeOffset.FromUnixTimeSeconds(JoinDateUnix);
        [JsonProperty("xp")]
        public int Contribution { get; set; }
    }
}
