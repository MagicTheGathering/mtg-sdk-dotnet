// <copyright file="Ruling.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model
{
    using System;
    using Dto;

    /// <summary>
    /// Object representing a ruling for a card.
    /// </summary>
    public class Ruling
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ruling"/> class.
        /// </summary>
        /// <param name="item">The legality data transfer object to map.</param>
        public Ruling(RulingDto item)
        {
            MapRuling(item);
        }

        /// <summary>
        /// Gets or sets the date of the ruling.
        /// </summary>
        public string Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text of the ruling.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Maps a single <see cref="LegalityDto"/> to a <see cref="Ruling"/> model.
        /// </summary>
        /// <param name="item">The foreign name data transfer object to map.</param>
        private void MapRuling(RulingDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Date = item.Date;
            Text = item.Text;
        }
    }
}