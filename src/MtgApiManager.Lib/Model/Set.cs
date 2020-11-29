using System.Collections.Generic;

namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a MTG Set.
    /// </summary>
    internal class Set : ISet
    {
        /// <inheritdoc />
        public string Block { get; set; }

        /// <inheritdoc />
        public List<object> Booster { get; set; }

        /// <inheritdoc />
        public string Border { get; set; }

        /// <inheritdoc />
        public string Code { get; set; }

        /// <inheritdoc />
        public string Expansion { get; set; }

        /// <inheritdoc />
        public string GathererCode { get; set; }

        /// <inheritdoc />
        public string MagicCardsInfoCode { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string OldCode { get; set; }

        /// <inheritdoc />
        public bool? OnlineOnly { get; set; }

        /// <inheritdoc />
        public string ReleaseDate { get; set; }

        /// <inheritdoc />
        public string Type { get; set; }
    }
}