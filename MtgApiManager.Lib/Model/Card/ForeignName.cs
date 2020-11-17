namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a foreign name for an MTG card.
    /// </summary>
    public class ForeignName
    {
        /// <summary>
        /// Gets the language it was printed in.
        /// </summary>
        public string Language { get; init; }

        /// <summary>
        /// Gets the multiverse identifier of the card for the foreign name.
        /// </summary>
        public int? MultiverseId { get; init; }

        /// <summary>
        /// Gets the name of the card in the foreign language.
        /// </summary>
        public string Name { get; init; }
    }
}