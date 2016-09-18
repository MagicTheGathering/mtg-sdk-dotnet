// <copyright file="Ruling.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model.Card
{
    using System;

    /// <summary>
    /// Object representing a ruling for a card.
    /// </summary>
    public class Ruling
    {
        /// <summary>
        /// Gets or sets the date of the ruling.
        /// </summary>
        public DateTime? Date
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
    }
}