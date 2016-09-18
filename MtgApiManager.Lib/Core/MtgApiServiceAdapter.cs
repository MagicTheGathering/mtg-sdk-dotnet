// <copyright file="WebService.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Utility;

    /// <summary>
    /// Used to make web service calls.
    /// </summary>
    public class MtgApiServiceAdapter : IMtgApiServiceAdapter
    {
        /// <summary>
        /// The mutable list of custom headers.
        /// </summary>
        private Dictionary<string, string> mutableHeaders;

        /// <summary>
        /// The custom mtg headers.
        /// </summary>
        public ReadOnlyDictionary<string, string> Headers
        {
            get
            {
                return new ReadOnlyDictionary<string, string>(this.mutableHeaders);
            }
        }

        /// <summary>
        /// Do a Web Get for the given request Uri .
        /// </summary>
        /// <param name="requestUri">The URL to call.</param>
        /// <returns>The response string.</returns>
        public async Task<T> WebGetAsync<T>(Uri requestUri)
            where T : Dto.MtgResponseBase
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(requestUri))
            {
                // Throw an exception if the call didn't succeed.
                response.EnsureSuccessStatusCode();

                // Get the custom mtg headers.
                this.mutableHeaders = this.ParseHeaders(response.Headers);

                // De serialize the response.
                var jsonString = await response.Content.ReadAsStringAsync();
                T obj = SerializationUtility.DeserializeDataContractJson<T>(jsonString);

                return obj;
            }
        }

        /// <summary>
        /// Gets all the related headers from the response.
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private Dictionary<string, string> ParseHeaders(HttpResponseHeaders headers)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            IEnumerable<string> resultHeaders = null;

            if (headers.TryGetValues("Link", out resultHeaders))
            {
                result.Add("Link", resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Page-Size", out resultHeaders))
            {
                result.Add("Page-Size", resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Count", out resultHeaders))
            {
                result.Add("Count", resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Total-Count", out resultHeaders))
            {
                result.Add("Total-Count", resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Ratelimit-Limit", out resultHeaders))
            {
                result.Add("Ratelimit-Limit", resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Ratelimit-Remaining", out resultHeaders))
            {
                result.Add("Ratelimit-Remaining", resultHeaders.FirstOrDefault());
            }

            return result;
        }
    }
}