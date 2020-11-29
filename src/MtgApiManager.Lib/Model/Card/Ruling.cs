namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a ruling for a card.
    /// </summary>
    internal class Ruling : IRuling
    {
        /// <inheritdoc />
        public string Date { get; set; }

        /// <inheritdoc />
        public string Text { get; set; }
    }
}