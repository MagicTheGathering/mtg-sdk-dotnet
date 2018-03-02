// <copyright file="Legality.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model
{
    using System;
    using MtgApiManager.Lib.Dto;

    /// <summary>
    /// Object representing the legality of a card.
    /// </summary>
    public class Legality
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Legality"/> class.
        /// </summary>
        /// <param name="item">The legality data transfer object to map.</param>
        public Legality(LegalityDto item)
        {
            MapLegality(item);
        }

        /// <summary>
        /// Gets or sets the format of the legality.
        /// </summary>
        public string Format
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the legality.
        /// </summary>
        public string LegalityName
        {
            get;
            set;
        }

        /// <summary>
        /// Maps a single <see cref="LegalityDto"/> to a <see cref="Legality"/> model.
        /// </summary>
        /// <param name="item">The foreign name data transfer object to map.</param>
        private void MapLegality(LegalityDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Format = item.Format;
            LegalityName = item.LegalityName;
        }
    }
}