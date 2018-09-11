// Generated by https://quicktype.io

namespace swnbot.Classes {
    using System.Collections.Generic;
    using System.Globalization;
    using System;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json;

    public partial class Armor {
        [JsonProperty ("ID")]
        public long Id { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("ac")]
        public int Ac { get; set; }

        [JsonProperty ("cost")]
        public long Cost { get; set; }

        [JsonProperty ("encumbrance")]
        public long Encumbrance { get; set; }

        [JsonProperty ("tech_level")]
        public long TechLevel { get; set; }
    }

    public partial class Armor {
        public static Armor[] InitArmor (string json) => JsonConvert.DeserializeObject<Armor[]> (json, Converter.Settings);
    }

    public static class SerializeArmor {
        public static string ToJson (this Armor[] self) => JsonConvert.SerializeObject (self, Converter.Settings);
    }
}