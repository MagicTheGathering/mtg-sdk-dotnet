// <copyright file="ForeignName.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model
{
    using System;
    using MtgApiManager.Lib.Dto;

    /// <summary>
    /// Object representing a foreign name for an MTG card.
    /// </summary>
    public class ForeignName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignName"/> class.
        /// </summary>
        /// <param name="item">The foreign name data transfer object to map.</param>
        public ForeignName(ForeignNameDto item)
        {
            this.MapForeignName(item);
        }

        /// <summary>
        /// Gets the language it was printed in.
        /// </summary>
        public string Language
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the multiverse identifier of the card for the foreign name.
        /// </summary>
        public int MultiverseId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the card in the foreign language.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Maps a single <see cref="ForeignNameDto"/> to a <see cref="ForeignName"/> model.
        /// </summary>
        /// <param name="item">The foreign name data transfer object to map.</param>
        private void MapForeignName(ForeignNameDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.Language = item.Language;
            this.MultiverseId = item.MultiverseId;
            this.Name = item.Name;
        }
    }
}