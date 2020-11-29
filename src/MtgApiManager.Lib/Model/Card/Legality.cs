namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing the legality of a card.
    /// </summary>
    internal class Legality : ILegality
    {
        /// <inheritdoc/>
        public string Format { get; set; }

        /// <inheritdoc/>
        public string LegalityName { get; set; }
    }
}