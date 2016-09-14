// <copyright file="ApiEndPoints.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System.ComponentModel;

    /// <summary>
    /// The mtg api endpoints.
    /// </summary>
    public enum ApiEndPoints
    {
        /// <summary>
        /// No end point.
        /// </summary>
        [Description("None")]
        None = 0,

        /// <summary>
        /// The endpoint which handles the cards.
        /// </summary>
        [Description("cards")]
        Cards = 0,

        /// <summary>
        /// The endpoint which handles the sets.
        /// </summary>
        [Description("sets")]
        Sets = 1,

        /// <summary>
        /// The endpoint which handles the card types.
        /// </summary>
        [Description("types")]
        CardTypes = 2,
    }
}