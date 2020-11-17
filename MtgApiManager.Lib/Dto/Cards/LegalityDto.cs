using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    public class LegalityDto
    {
        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("legality")]
        public string Legality { get; set; }
    }
}