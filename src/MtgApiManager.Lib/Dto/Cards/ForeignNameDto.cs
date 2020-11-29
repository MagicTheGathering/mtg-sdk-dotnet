using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class ForeignNameDto
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("multiverseid")]
        public int? MultiverseId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}