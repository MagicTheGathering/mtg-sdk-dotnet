using System;
using System.Collections.Generic;

namespace MtgApiManager.Lib.Model
{
    /// <inheritdoc />
    internal class Card : ICard
    {
        /// <inheritdoc />
        public string Artist { get; set; }

        /// <inheritdoc />
        public string Border { get; set; }

        /// <inheritdoc />
        public float? Cmc { get; set; }

        /// <inheritdoc />
        public string[] ColorIdentity { get; set; }

        /// <inheritdoc />
        public string[] Colors { get; set; }

        /// <inheritdoc />
        public string Flavor { get; set; }

        /// <inheritdoc />
        public List<IForeignName> ForeignNames { get; set; }

        /// <inheritdoc />
        public int? Hand { get; set; }

        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public Uri ImageUrl { get; set; }

        /// <inheritdoc />
        public bool IsColorless => (Colors?.Length ?? 0) < 1;

        /// <inheritdoc />
        public bool IsMultiColor => Colors?.Length > 1;

        /// <inheritdoc />
        public string Layout { get; set; }

        /// <inheritdoc />
        public List<ILegality> Legalities { get; set; }

        /// <inheritdoc />
        public int? Life { get; set; }

        /// <inheritdoc />
        public string Loyalty { get; set; }

        /// <inheritdoc />
        public string ManaCost { get; set; }

        /// <inheritdoc />
        public string MultiverseId { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string[] Names { get; set; }

        /// <inheritdoc />
        public string Number { get; set; }

        /// <inheritdoc />
        public string OriginalText { get; set; }

        /// <inheritdoc />
        public string OriginalType { get; set; }

        /// <inheritdoc />
        public string Power { get; set; }

        /// <inheritdoc />
        public string[] Printings { get; set; }

        /// <inheritdoc />
        public string Rarity { get; set; }

        /// <inheritdoc />
        public string ReleaseDate { get; set; }

        /// <inheritdoc />
        public bool? Reserved { get; set; }

        /// <inheritdoc />
        public List<IRuling> Rulings { get; set; }

        /// <inheritdoc />
        public string Set { get; set; }

        /// <inheritdoc />
        public string SetName { get; set; }

        /// <inheritdoc />
        public string Source { get; set; }

        /// <inheritdoc />
        public bool? Starter { get; set; }

        /// <inheritdoc />
        public string[] SubTypes { get; set; }

        /// <inheritdoc />
        public string[] SuperTypes { get; set; }

        /// <inheritdoc />
        public string Text { get; set; }

        /// <inheritdoc />
        public bool? Timeshifted { get; set; }

        /// <inheritdoc />
        public string Toughness { get; set; }

        /// <inheritdoc />
        public string Type { get; set; }

        /// <inheritdoc />
        public string[] Types { get; set; }

        /// <inheritdoc />
        public string[] Variations { get; set; }

        /// <inheritdoc />
        public string Watermark { get; set; }
    }
}