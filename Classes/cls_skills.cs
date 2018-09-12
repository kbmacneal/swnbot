using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace swnbot.Classes {
    public class skills

    {
        
        [JsonProperty ("Level")]
        public long Level { get; set; }
        [JsonProperty ("ID")]
        public long Id { get; set; }
        [JsonProperty ("Name")]
        public string Name { get; set; }
        [JsonProperty ("Specialist")]
        public int Specialist { get; set; }
    }

    public partial class Skill {
        public static skills[] InitSkills (string json) => JsonConvert.DeserializeObject<skills[]> (json, Converter.Settings);
    }

    public static class Serialize {
        public static string ToJson (this Skill[] self) => JsonConvert.SerializeObject (self, Converter.Settings);
    }

    internal static class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}