// Generated by https://quicktype.io

namespace swnbot.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Melee
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Damage")]
        public string Damage { get; set; }

        [JsonProperty("ShockDamage")]
        public string ShockDamage { get; set; }

        [JsonProperty("Attribute")]
        public string Attribute { get; set; }

        [JsonProperty("Cost")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Cost { get; set; }

        [JsonProperty("Encumbrance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Encumbrance { get; set; }

        [JsonProperty("TechLevel")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TechLevel { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }
    }

    public partial class Melee
    {
        public static Melee[] FromJson(string json) => JsonConvert.DeserializeObject<Melee[]>(json, Converter.Settings);
    }
    
}
