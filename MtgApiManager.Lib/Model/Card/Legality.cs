namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing the legality of a card.
    /// </summary>
    public class Legality
    {
        /// <summary>
        /// Gets or sets the format of the legality.
        /// </summary>
        public string Format { get; init; }

        /// <summary>
        /// Gets or sets the name of the legality.
        /// </summary>
        public string LegalityName { get; init; }
    }
}