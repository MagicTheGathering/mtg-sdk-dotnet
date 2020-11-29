namespace MtgApiManager.Lib.Model
{
    /// <inheritdoc />
    public class ForeignName : IForeignName
    {
        /// <inheritdoc />
        public string Language { get; set; }

        /// <inheritdoc />
        public int? MultiverseId { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }
    }
}