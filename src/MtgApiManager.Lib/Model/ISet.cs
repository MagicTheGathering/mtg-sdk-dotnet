using System.Collections.Generic;

namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a MTG Set.
    /// </summary>
    public interface ISet
    {
        /// <summary>
        /// Gets the block the set is in.
        /// </summary>
        string Block { get; }

        /// <summary>
        /// Gets the booster contents for this set.
        /// </summary>
        List<object> Booster { get; }

        /// <summary>
        /// Gets the type of border on the cards, either “white”, “black” or “silver”.
        /// </summary>
        string Border { get; }

        /// <summary>
        /// Gets the code name of the set.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets the type of set. One of: “core”, “expansion”, “reprint”, “box”, “un”, “from the vault”, “premium deck”, “duel deck”, “starter”, “commander”, “planechase”, “archenemy”, “promo”, “vanguard”, “masters”.
        /// </summary>
        string Expansion { get; }

        /// <summary>
        /// Gets the code that Gatherer uses for the set. Only present if different than <see cref="Code"/>.
        /// </summary>
        string GathererCode { get; }

        /// <summary>
        /// Gets the code that magiccards.info uses for the set. Only present if magiccards.info has this set.
        /// </summary>
        string MagicCardsInfoCode { get; }

        /// <summary>
        /// Gets the name of the set.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an old style code used by some Magic software. Only present if different than <see cref="GathererCode"/> and <see cref="Code"/>.
        /// </summary>
        string OldCode { get; }

        /// <summary>
        /// Gets a value indicating whether the set was only released on line.
        /// </summary>
        bool? OnlineOnly { get; }

        /// <summary>
        /// Gets when the set was released. For promo sets, the date the first card was released.
        /// </summary>
        string ReleaseDate { get; }

        /// <summary>
        /// Gets the set type.
        /// </summary>
        string Type { get; }
    }
}