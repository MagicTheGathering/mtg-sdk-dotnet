// <copyright file="Set.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model
{
    using System;
    using System.Collections.Generic;
    using Dto;
    using Newtonsoft.Json.Linq;

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
            MapSet(item);
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
        public List<object> Booster
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
        /// Gets a value indicating whether the set was only released on line.
        /// </summary>
        public bool? OnlineOnly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets when the set was released. For promo sets, the date the first card was released.
        /// </summary>
        public string ReleaseDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Handles parsing the booster property by recursively looking for arrays or values.
        /// </summary>
        /// <param name="item">The parent item.</param>
        /// <returns>A object representing the booster member.</returns>
        private static object CreateBoosterArray(object item)
        {
            if (item is JValue value)
            {
                return value.Value.ToString();
            }
            else
            {
                var array = item as JArray;
                var subList = new List<object>();

                foreach (var arrayItem in array)
                {
                    subList.Add(Set.CreateBoosterArray(arrayItem));
                }

                return subList;
            }
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

            Block = item.Block;

            if (item.Booster != null)
            {
                Booster = new List<object>();
                foreach (var booster in item.Booster)
                {
                    if (booster is string)
                    {
                        Booster.Add(booster);
                    }
                    else
                    {
                        Booster.Add(Set.CreateBoosterArray(booster));
                    }
                }
            }

            Border = item.Border;
            Code = item.Code;
            Expansion = item.Expansion;
            GathererCode = item.GathererCode;
            MagicCardsInfoCode = item.MagicCardsInfoCode;
            Name = item.Name;
            OldCode = item.OldCode;
            OnlineOnly = item.OnlineOnly;
            ReleaseDate = item.ReleaseDate;
        }
    }
}