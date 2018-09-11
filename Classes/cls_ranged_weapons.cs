namespace swnbot.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RangedWeapons
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Damage")]
        public string Damage { get; set; }

        [JsonProperty("Range")]
        public string Range { get; set; }

        [JsonProperty("Cost")]
        public string Cost { get; set; }

        [JsonProperty("Magazine")]
        [JsonConverter(typeof(ParseStringConverter))]
        public int Magazine { get; set; }

        [JsonProperty("Attribute")]
        public string Attribute { get; set; }

        [JsonProperty("Encumbrance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public int Encumbrance { get; set; }

        [JsonProperty("TechLevel")]
        [JsonConverter(typeof(ParseStringConverter))]
        public int TechLevel { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }
    }

    public partial class RangedWeapons
    {
        public static RangedWeapons[] FromJson(string json) => JsonConvert.DeserializeObject<RangedWeapons[]>(json, Converter.Settings);
    }

    public static class SerializeRanged
    {
        public static string ToJson(this RangedWeapons[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class ConvertRanged
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
