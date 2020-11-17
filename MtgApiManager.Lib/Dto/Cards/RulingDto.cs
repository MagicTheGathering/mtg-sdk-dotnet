using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    public class RulingDto
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}