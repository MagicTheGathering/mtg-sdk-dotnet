// <copyright file="IMtgApiServiceAdapter.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;
    using System.Threading.Tasks;
    using Dto;

    /// <summary>
    /// Used to make web service calls.
    /// </summary>
    public interface IMtgApiServiceAdapter
    {
        /// <summary>
        /// Do a Web Get for the given request Uri .
        /// </summary>
        /// <typeparam name="T">The type to serialize into.</typeparam>
        /// <param name="requestUri">The URL to call.</param>
        /// <returns>The serialized response.</returns>
        Task<T> WebGetAsync<T>(Uri requestUri) where T : MtgResponseBase;
    }
}