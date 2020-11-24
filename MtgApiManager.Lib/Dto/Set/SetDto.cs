using System.Text.Json;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    public class SetDto
    {
        [JsonPropertyName("block")]
        public string Block { get; set; }

        [JsonPropertyName("booster")]
        public JsonElement Booster { get; set; }

        [JsonPropertyName("border")]
        public string Border { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("expansion")]
        public string Expansion { get; set; }

        [JsonPropertyName("gathererCode")]
        public string GathererCode { get; set; }

        [JsonPropertyName("magicCardsInfoCode")]
        public string MagicCardsInfoCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("oldCode")]
        public string OldCode { get; set; }

        [JsonPropertyName("onlineOnly")]
        public bool? OnlineOnly { get; set; }

        [JsonPropertyName("ReleaseDate")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}