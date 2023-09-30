using System.Text.Json.Serialization;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator.Models
{
    class ResponseTranslation
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    class Message
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("@service")]
        public string Service { get; set; }

        [JsonPropertyName("@version")]
        public string Version { get; set; }

        [JsonPropertyName("result")]
        public ResultTranslation Result { get; set; }
    }

    class ResultTranslation
    {
        [JsonPropertyName("srcLangType")]
        public string SrcLangType { get; set; }

        [JsonPropertyName("tarLangType")]
        public string TarLangType { get; set; }

        [JsonPropertyName("translatedText")]
        public string TranslatedText { get; set; }

        [JsonPropertyName("engineType")]
        public string EngineType { get; set; }

        [JsonPropertyName("pivot")]
        public string Pivot { get; set; }
    }
}
