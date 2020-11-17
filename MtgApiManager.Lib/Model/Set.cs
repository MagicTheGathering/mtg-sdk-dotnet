using System.Collections.Generic;

namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a MTG Set.
    /// </summary>
    public class Set
    {
        /// <summary>
        /// Gets the block the set is in.
        /// </summary>
        public string Block { get; init; }

        /// <summary>
        /// Gets the booster contents for this set.
        /// </summary>
        public List<object> Booster { get; init; }

        /// <summary>
        /// Gets the type of border on the cards, either “white”, “black” or “silver”.
        /// </summary>
        public string Border { get; init; }

        /// <summary>
        /// Gets the code name of the set.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the type of set. One of: “core”, “expansion”, “reprint”, “box”, “un”, “from the vault”, “premium deck”, “duel deck”, “starter”, “commander”, “planechase”, “archenemy”, “promo”, “vanguard”, “masters”.
        /// </summary>
        public string Expansion { get; init; }

        /// <summary>
        /// Gets the code that Gatherer uses for the set. Only present if different than <see cref="Code"/>.
        /// </summary>
        public string GathererCode { get; init; }

        /// <summary>
        /// Gets the code that magiccards.info uses for the set. Only present if magiccards.info has this set.
        /// </summary>
        public string MagicCardsInfoCode { get; init; }

        /// <summary>
        /// Gets the name of the set.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets an old style code used by some Magic software. Only present if different than <see cref="GathererCode"/> and <see cref="Code"/>.
        /// </summary>
        public string OldCode { get; init; }

        /// <summary>
        /// Gets a value indicating whether the set was only released on line.
        /// </summary>
        public bool? OnlineOnly { get; init; }

        /// <summary>
        /// Gets when the set was released. For promo sets, the date the first card was released.
        /// </summary>
        public string ReleaseDate { get; init; }
    }
}