// <copyright file="MtgApiServiceAdapter.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using Dto;
    using Exceptions;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Utility;

    /// <summary>
    /// Used to make web service calls, and can easily be mocked for testing.
    /// </summary>
    public class MtgApiServiceAdapter : IMtgApiServiceAdapter
    {
        /// <summary>
        /// Do a Web Get for the given request Uri .
        /// </summary>
        /// <typeparam name="T">The type to serialize into.</typeparam>
        /// <param name="requestUri">The URL to call.</param>
        /// <returns>The serialized response.</returns>
        public async Task<T> WebGetAsync<T>(Uri requestUri)
            where T : MtgResponseBase
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(requestUri).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        MtgApiController.ParseHeaders(response.Headers);
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    }
                    else
                    {
                        switch ((int)response.StatusCode)
                        {
                            case (int)MtgApiError.BadRequest:
                                throw new MtgApiException<BadRequestException>(MtgApiError.BadRequest.GetDescription());
                            case (int)MtgApiError.Forbidden:
                                throw new MtgApiException<ForbiddenException>(MtgApiError.Forbidden.GetDescription());
                            case (int)MtgApiError.InternalServerError:
                                throw new MtgApiException<InternalServerErrorException>(MtgApiError.InternalServerError.GetDescription());
                            case (int)MtgApiError.NotFound:
                                throw new MtgApiException<NotFoundException>(MtgApiError.NotFound.GetDescription());
                            case (int)MtgApiError.ServiceUnavailable:
                                throw new MtgApiException<ServiceUnavailableException>(MtgApiError.ServiceUnavailable.GetDescription());
                            default:
                                response.EnsureSuccessStatusCode();
                                return null;
                        }
                    }
                }
            }
        }
    }
}