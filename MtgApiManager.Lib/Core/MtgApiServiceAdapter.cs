// <copyright file="MtgApiServiceAdapter.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Used to make web service calls, and can easily be mocked for testing.
    /// </summary>
    public class MtgApiServiceAdapter : IMtgApiServiceAdapter
    {
        /// <summary>
        /// Do a Web Get for the given request Uri .
        /// </summary>
        /// <param name="requestUri">The URL to call.</param>
        /// <returns>The response string.</returns>
        public async Task<HttpResponseMessage> WebGetAsync(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }

            using (var client = new HttpClient())
            {
                return await client.GetAsync(requestUri);
            }                
        }
    }
}