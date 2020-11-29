namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a foreign name for an MTG card.
    /// </summary>
    public interface IForeignName
    {
        /// <summary>
        /// Gets the language it was printed in.
        /// </summary>
        string Language { get; }

        /// <summary>
        /// Gets the multiverse identifier of the card for the foreign name.
        /// </summary>
        int? MultiverseId { get; }

        /// <summary>
        /// Gets the name of the card in the foreign language.
        /// </summary>
        string Name { get; }
    }
}