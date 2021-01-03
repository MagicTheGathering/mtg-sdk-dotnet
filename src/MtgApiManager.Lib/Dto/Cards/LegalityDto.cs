using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class LegalityDto
    {
        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("legality")]
        public string LegalityName { get; set; }
    }
}