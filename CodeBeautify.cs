namespace CodeBeautify
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Dsec
   {
        [JsonProperty("Characters.json")]
        public string[] CharactersJson { get; set; }

        public override string ToString()
        {
            if (CharactersJson == null || CharactersJson.Length == 0)
            {
                return string.Empty;
            }

            // Assuming each element in CharactersJson is a JSON string
            var formattedCharacters = new List<string>();
            foreach (var characterJson in CharactersJson)
            {
                var characterObject = JsonConvert.DeserializeObject<Dsec>(characterJson); // Replace YourCharacterClass with the actual class
                var formattedCharacter = JsonConvert.SerializeObject(characterObject, Formatting.Indented);
                formattedCharacters.Add(formattedCharacter);
            }

            return $"{{\r\n\t\"Characters.json\": [\r\n\t\t{string.Join(",\r\n\t\t", formattedCharacters)}\r\n\t]\r\n}}";
        }

        internal static object FromJson(string[] jsonContent)
        {
            throw new NotImplementedException();
        }
    }

    public partial class Dsec
    {
        public static Dsec FromJson(string json) => JsonConvert.DeserializeObject<Dsec>(json, CodeBeautify.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dsec self) => JsonConvert.SerializeObject(self, CodeBeautify.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
