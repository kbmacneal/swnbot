// Generated by https://quicktype.io

namespace swnbot.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Foci
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Level")]
        public int Level { get; set; }
    }

    public partial class Foci
    {
        public static Foci[] FromJson(string json) => JsonConvert.DeserializeObject<Foci[]>(json, Converter.Settings);
    }

    
}
