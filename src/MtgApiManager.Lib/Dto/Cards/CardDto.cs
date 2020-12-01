using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class CardDto
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("border")]
        public string Border { get; set; }

        [JsonPropertyName("cmc")]
        public float? Cmc { get; set; }

        [JsonPropertyName("colorIdentity")]
        public string[] ColorIdentity { get; set; }

        [JsonPropertyName("colors")]
        public string[] Colors { get; set; }

        [JsonPropertyName("flavor")]
        public string Flavor { get; set; }

        [JsonPropertyName("foreignNames")]
        public ForeignNameDto[] ForeignNames { get; set; }

        [JsonPropertyName("hand")]
        public int? Hand { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("layout")]
        public string Layout { get; set; }

        [JsonPropertyName("legalities")]
        public LegalityDto[] Legalities { get; set; }

        [JsonPropertyName("life")]
        public int? Life { get; set; }

        [JsonPropertyName("loyalty")]
        public string Loyalty { get; set; }

        [JsonPropertyName("manaCost")]
        public string ManaCost { get; set; }

        [JsonPropertyName("multiverseid")]
        public int? MultiverseId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("names")]
        public string[] Names { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("originalText")]
        public string OriginalText { get; set; }

        [JsonPropertyName("originalType")]
        public string OriginalType { get; set; }

        [JsonPropertyName("power")]
        public string Power { get; set; }

        [JsonPropertyName("printings")]
        public string[] Printings { get; set; }

        [JsonPropertyName("rarity")]
        public string Rarity { get; set; }

        [JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("reserved")]
        public bool? Reserved { get; set; }

        [JsonPropertyName("rulings")]
        public RulingDto[] Rulings { get; set; }

        [JsonPropertyName("set")]
        public string Set { get; set; }

        [JsonPropertyName("setName")]
        public string SetName { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("starter")]
        public bool? Starter { get; set; }

        [JsonPropertyName("subtypes")]
        public string[] SubTypes { get; set; }

        [JsonPropertyName("supertypes")]
        public string[] SuperTypes { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("timeshifted")]
        public bool? Timeshifted { get; set; }

        [JsonPropertyName("toughness")]
        public string Toughness { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("types")]
        public string[] Types { get; set; }

        [JsonPropertyName("variations")]
        public string[] Variations { get; set; }

        [JsonPropertyName("watermark")]
        public string Watermark { get; set; }
    }
}