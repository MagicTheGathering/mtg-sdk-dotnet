// <copyright file="Set.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model
{
    using System;
    using Dto;

    /// <summary>
    /// Object representing a MTG Set.
    /// </summary>
    public class Set
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Set"/> class.
        /// </summary>
        /// <param name="item">The set data transfer object to map to.</param>
        public Set(SetDto item)
        {
            this.MapSet(item);
        }

        /// <summary>
        /// Gets the block the set is in.
        /// </summary>
        public string Block
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the booster contents for this set.
        /// </summary>
        public object Booster
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of border on the cards, either “white”, “black” or “silver”.
        /// </summary>
        public string Border
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the code name of the set.
        /// </summary>
        public string Code
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of set. One of: “core”, “expansion”, “reprint”, “box”, “un”, “from the vault”, “premium deck”, “duel deck”, “starter”, “commander”, “planechase”, “archenemy”, “promo”, “vanguard”, “masters”.
        /// </summary>
        public string Expansion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the code that Gatherer uses for the set. Only present if different than <see cref="Code"/>.
        /// </summary>
        public string GathererCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the code that magiccards.info uses for the set. Only present if magiccards.info has this set.
        /// </summary>
        public string MagicCardsInfoCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the set.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an old style code used by some Magic software. Only present if different than <see cref="GathererCode"/> and <see cref="Code"/>.
        /// </summary>
        public string OldCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Get a value indicating whether the set was only released on line.
        /// </summary>
        public bool? OnlineOnly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets when the set was released. For promo sets, the date the first card was released.
        /// </summary>
        public DateTime? ReleaseDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Maps a single set DTO to the set model.
        /// </summary>
        /// <param name="item">The set DTO object.</param>
        private void MapSet(SetDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.Block = item.Block;
            this.Booster = item.Booster;
            this.Border = item.Border;
            this.Code = item.Code;
            this.Expansion = item.Expansion;
            this.GathererCode = item.GathererCode;
            this.MagicCardsInfoCode = item.MagicCardsInfoCode;
            this.Name = item.Name;
            this.OldCode = item.OldCode;
            this.OnlineOnly = item.OnlineOnly;
            this.ReleaseDate = item.ReleaseDate;
        }
    }
}