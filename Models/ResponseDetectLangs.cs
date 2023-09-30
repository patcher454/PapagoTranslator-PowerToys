using System.Text.Json.Serialization;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator.Models
{
    class ResponseDetectLangs
    {
        [JsonPropertyName("langCode")]
        public string LangCode { get; set; }
    }
}
