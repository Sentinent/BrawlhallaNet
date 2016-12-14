using System.Collections.Generic;
using Newtonsoft.Json;

namespace BrawlhallaNet.Responses
{
    public class Player
    {
        [JsonProperty("brawlhalla_id")]
        public ulong BrawlhallaId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("xp_percentage")]
        public double XPPercent { get; set; }
        [JsonProperty("games")]
        public int GamesPlayed { get; set; }
        [JsonProperty("wins")]
        public int GamesWon { get; set; }
        [JsonProperty("damagebomb")]
        public string BombDamage { get; set; }
        [JsonProperty("damagemine")]
        public string MineDamage { get; set; }
        [JsonProperty("damagespikeball")]
        public string SpikeballDamage { get; set; }
        [JsonProperty("damagesidekick")]
        public string SidekickDamage { get; set; }
        [JsonProperty("hitsnowball")]
        public int SnowballHits { get; set; }
        [JsonProperty("kobomb")]
        public int BombKills { get; set; }
        [JsonProperty("komine")]
        public int MineKills { get; set; }
        [JsonProperty("kospikeball")]
        public int SpikeBallKills { get; set; }
        [JsonProperty("kosidekick")]
        public int SidekickKills { get; set; }
        [JsonProperty("kosnowball")]
        public int SnowballKills { get; set; }
        [JsonProperty("legends")]
        public IEnumerable<PlayerLegend> Legends { get; set; }
        [JsonProperty("clan")]
        public PlayerClan Clan { get; set; }
    }

    public class PlayerLegend
    {
        [JsonProperty("legend_id")]
        public int ID { get; set; }
        [JsonProperty("legend_name_key")]
        public string Name { get; set; }
        [JsonProperty("damagedealt")]
        public string DamageDealt { get; set; }
        [JsonProperty("damagetaken")]
        public string DamageTaken { get; set; }
        [JsonProperty("kos")]
        public int Kills { get; set; }
        [JsonProperty("falls")]
        public int Deaths { get; set; }
        [JsonProperty("suicides")]
        public int Suicides { get; set; }
        [JsonProperty("teamkos")]
        public int TeamKills { get; set; }
        [JsonProperty("matchtime")]
        public int MatchTime { get; set; }
        [JsonProperty("games")]
        public int Games { get; set; }
        [JsonProperty("wins")]
        public int Wins { get; set; }
        [JsonProperty("damageunarmed")]
        public string UnarmedDamage { get; set; }
        [JsonProperty("damagethrownitem")]
        public string ThrownItemDamage { get; set; }
        [JsonProperty("damageweaponone")]
        public string WeaponOneDamage { get; set; }
        [JsonProperty("damageweapontwo")]
        public string WeaponTwoDamage { get; set; }
        [JsonProperty("damagegadgets")]
        public string GadgetDamage { get; set; }
        [JsonProperty("kounarmed")]
        public int UnarmedKills { get; set; }
        [JsonProperty("kothrownitem")]
        public int ItemKills { get; set; }
        [JsonProperty("koweaponone")]
        public int WeaponOneKills { get; set; }
        [JsonProperty("koweapontwo")]
        public int WeaponTwoKills { get; set; }
        [JsonProperty("kogadgets")]
        public int GadgetKills { get; set; }
        [JsonProperty("timeheldweaponone")]
        public int WeaponOneHeldTime { get; set; }
        [JsonProperty("timeheldweapontwo")]
        public int WeaponTwoHeldTime { get; set; }
        [JsonProperty("xp")]
        public int XP { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("xp_percentage")]
        public double XPPercent { get; set; }
    }

    public class PlayerClan
    {
        [JsonProperty("clan_name")]
        public string Name { get; set; }
        [JsonProperty("clan_id")]
        public ulong ID { get; set; }
        [JsonProperty("clan_xp")]
        public string XP { get; set; }
        [JsonProperty("personal_xp")]
        public int Contribution { get; set; }
    }
}
