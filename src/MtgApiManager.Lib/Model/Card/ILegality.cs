namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing the legality of a card.
    /// </summary>
    public interface ILegality
    {
        /// <summary>
        /// Gets or sets the format of the legality.
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Gets or sets the name of the legality.
        /// </summary>
        string LegalityName { get; }
    }
}