using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Query parameters for the <see cref="Model.Card"/> object.
    /// </summary>
    public class CardQueryParameter : QueryParameterBase
    {
        /// <summary>
        /// Gets or sets the artist of the card.
        /// </summary>
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the converted Mana cost.
        /// </summary>
        [JsonPropertyName("cmc")]
        public string Cmc { get; set; }

        /// <summary>
        /// Gets or sets the card colors by color code. [“Red”, “Blue”] becomes [“R”, “U”].
        /// </summary>
        [JsonPropertyName("colorIdentity")]
        public string[] ColorIdentity { get; set; }

        /// <summary>
        /// Gets or sets the card colors. Usually this is derived from the casting cost, but some cards are special (like the back of dual sided cards and Ghostfire).
        /// </summary>
        [JsonPropertyName("colors")]
        public string[] Colors { get; set; }

        /// <summary>
        /// Gets or sets the flavor text of the card.
        /// </summary>
        [JsonPropertyName("flavor")]
        public string Flavor { get; set; }

        /// <summary>
        /// Gets or sets the name of a card in a foreign language it was printed in.
        /// </summary>
        [JsonPropertyName("foreignName")]
        public string ForeignName { get; set; }

        /// <summary>
        /// Gets or sets the game format, such as Commander, Standard, Legacy, etc. (when used, legality defaults to Legal unless supplied).
        /// </summary>
        [JsonPropertyName("gameFormat")]
        public string GameFormat { get; set; }

        /// <summary>
        /// Gets or sets the language the card is printed in. Use this parameter when searching by foreignName.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the card layout. Possible values: normal, split, flip, double-faced, token, plane, scheme, phenomenon, leveler, vanguard
        /// </summary>
        [JsonPropertyName("layout")]
        public string Layout { get; set; }

        /// <summary>
        /// Gets or sets the legality of the card for a given format, such as Legal, Banned or Restricted.
        /// </summary>
        [JsonPropertyName("legality")]
        public string Legality { get; set; }

        /// <summary>
        /// Gets or sets the loyalty of the card. This is only present for planeswalkers.
        /// </summary>
        [JsonPropertyName("loyalty")]
        public string Loyalty { get; set; }

        /// <summary>
        /// Gets or sets the card name. For split, double-faced and flip cards, just the name of one side of the card. Basically each ‘sub-card’ has its own record.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the card number. This is printed at the bottom-center of the card in small text..
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the field to order by in the response results.
        /// </summary>
        [JsonPropertyName("orderBy")]
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the power of the card. This is only present for creatures.
        /// </summary>
        [JsonPropertyName("power")]
        public string Power { get; set; }

        /// <summary>
        /// Gets or sets the rarity of the card. Examples: Common, Uncommon, Rare, Mythic Rare, Special, Basic Land
        /// </summary>
        [JsonPropertyName("rarity")]
        public string Rarity { get; set; }

        /// <summary>
        /// Gets or sets the set the card belongs to (set code).
        /// </summary>
        [JsonPropertyName("set")]
        public string Set { get; set; }

        /// <summary>
        /// Gets or sets the set the card belongs to.
        /// </summary>
        [JsonPropertyName("setName")]
        public string SetName { get; set; }

        /// <summary>
        /// Gets or sets the he subtypes of the card. These appear to the right of the dash in a card type. Usually each word is its own subtype. Example values: Trap, Arcane, Equipment, Aura, Human, Rat, Squirrel, etc.
        /// </summary>
        [JsonPropertyName("subtypes")]
        public string[] SubTypes { get; set; }

        /// <summary>
        /// Gets or sets the super types of the card. These appear to the far left of the card type. Example values: Basic, Legendary, Snow, World, Ongoing
        /// </summary>
        [JsonPropertyName("supertypes")]
        public string[] SuperTypes { get; set; }

        /// <summary>
        /// Gets or sets the oracle text of the card. May contain mana symbols and other symbols.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the toughness of the card. This is only present for creatures.
        /// </summary>
        [JsonPropertyName("toughness")]
        public string Toughness { get; set; }

        /// <summary>
        /// Gets or sets the card type. This is the type you would see on the card if printed today. Note: The dash is a UTF8 'long dash’ as per the MTG rules
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the types of the card. These appear to the left of the dash in a card type. Example values: Instant, Sorcery, Artifact, Creature, Enchantment, Land, Planeswalker
        /// </summary>
        [JsonPropertyName("types")]
        public string Types { get; set; }
    }
}