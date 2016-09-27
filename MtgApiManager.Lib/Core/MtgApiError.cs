// <copyright file="MtgApiError.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System.ComponentModel;

    /// <summary>
    /// Represents the API errors.
    /// </summary>
    public enum MtgApiError
    {
        /// <summary>
        /// No error.
        /// </summary>
        [Description("none")]
        None = 0,

        /// <summary>
        /// The request is not valid
        /// </summary>
        [Description("Your request is not valid")]
        BadRequest = 400,

        /// <summary>
        /// The rate limit has been exceeded.
        /// </summary>
        [Description("You have exceeded the rate limit")]
        Forbidden = 403,

        /// <summary>
        /// The specified resource could not be found
        /// </summary>
        [Description("The specified resource could not be found")]
        NotFound = 404,

        /// <summary>
        /// Issue with the server.
        /// </summary>
        [Description("We had a problem with our server. Try again later.")]
        InternalServerError = 500,

        /// <summary>
        /// The service is temporarily unavailable.
        /// </summary>
        [Description("We’re temporarily off line for maintenance. Please try again later.")]
        ServiceUnavailable = 503
    }
}