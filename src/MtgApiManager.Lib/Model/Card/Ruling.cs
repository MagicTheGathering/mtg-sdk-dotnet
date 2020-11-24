namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a ruling for a card.
    /// </summary>
    public class Ruling
    {
        /// <summary>
        /// Gets or sets the date of the ruling.
        /// </summary>
        public string Date { get; init; }

        /// <summary>
        /// Gets or sets the text of the ruling.
        /// </summary>
        public string Text { get; init; }
    }
}