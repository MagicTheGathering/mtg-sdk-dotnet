// <copyright file="ApiEndPoint.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System.ComponentModel;

    /// <summary>
    /// The MTG API endpoints.
    /// </summary>
    public enum ApiEndPoint
    {
        /// <summary>
        /// No end point.
        /// </summary>
        [Description("none")]
        None = 0,

        /// <summary>
        /// The endpoint which handles the cards.
        /// </summary>
        [Description("cards")]
        Cards = 1,

        /// <summary>
        /// The endpoint which handles the sets.
        /// </summary>
        [Description("sets")]
        Sets = 2,

        /// <summary>
        /// The endpoint which handles the card types.
        /// </summary>
        [Description("types")]
        CardTypes = 3,

        /// <summary>
        /// The endpoint which handles the card super types.
        /// </summary>
        [Description("supertypes")]
        CardSuperTypes = 4,

        /// <summary>
        /// The endpoint which handles the card sub types.
        /// </summary>
        [Description("subtypes")]
        CardSubTypes = 5,
    }
}