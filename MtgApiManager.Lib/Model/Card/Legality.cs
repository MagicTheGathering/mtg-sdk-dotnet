// <copyright file="Legality.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model.Card
{
    /// <summary>
    /// Object representing the legality of a card.
    /// </summary>
    public class Legality
    {
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
    }
}