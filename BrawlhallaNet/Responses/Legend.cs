using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrawlhallaNet.Responses
{
    public class Legend
    {
        [JsonProperty("legend_id")]
        public int ID { get; set; }
        [JsonProperty("legend_name_key")]
        public string Name { get; set; }
        [JsonProperty("bio_name")]
        public string BioName { get; set; }
        [JsonProperty("bio_aka")]
        public string Title { get; set; }
        [JsonProperty("bio_quote")]
        public string Quote { get; set; }
        [JsonProperty("bio_quote_about_attrib")]
        public string QuoteAuthor { get; set; }
        [JsonProperty("bio_quote_from")]
        public string Quote2 { get; set; }
        [JsonProperty("bio_quote_from_attrib")]
        public string Quote2Author { get; set; }
        [JsonProperty("bot_name")]
        public string BotName { get; set; }
        [JsonProperty("weapon_one")]
        public string WeaponOne { get; set; }
        [JsonProperty("weapon_two")]
        public string WeaponTwo { get; set; }
        [JsonProperty("strength")]
        public string Strength { get; set; }
        [JsonProperty("dexterity")]
        public string Dexterity { get; set; }
        [JsonProperty("defense")]
        public string Defense { get; set; }
        [JsonProperty("speed")]
        public string Speed { get; set; }
    }
}
