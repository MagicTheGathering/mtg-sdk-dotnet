// <copyright file="ForeignName.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model.Card
{
    /// <summary>
    /// Object representing a foreign name for an mtg card.
    /// </summary>
    public class ForeignName
    {
        /// <summary>
        /// Get or sets the language it was printed in.
        /// </summary>
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the multiverse identifier of the card for the foreign name.
        /// </summary>
        public int MultiverseId
        {
            get;
            set;
        }

        /// <summary>
        /// Get or sets the name of the card in the foreign language.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}