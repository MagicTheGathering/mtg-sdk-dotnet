using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core.Exceptions;
using MtgApiManager.Lib.Dto;

namespace MtgApiManager.Lib.Core
{
    /// <summary>
    /// Used to make web service calls, and can easily be mocked for testing.
    /// </summary>
    internal class MtgApiServiceAdapter : IMtgApiServiceAdapter
    {
        private readonly IHeaderManager _headerManager;

        public MtgApiServiceAdapter(IHeaderManager headerManager)
        {
            _headerManager = headerManager;
        }

        /// <summary>
        /// Do a Web Get for the given request Uri .
        /// </summary>
        /// <typeparam name="T">The type to serialize into.</typeparam>
        /// <param name="requestUri">The URL to call.</param>
        /// <returns>The serialized response.</returns>
        public Task<T> WebGetAsync<T>(Uri requestUri) where T : IMtgResponse
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return WebGetAsyncInternal<T>(requestUri);
        }

        private async Task<T> WebGetAsyncInternal<T>(Uri requestUri) where T : IMtgResponse
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(requestUri).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        _headerManager.Update(response.Headers);
                        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        return await JsonSerializer.DeserializeAsync<T>(stream).ConfigureAwait(false);
                    }
                    else
                    {
                        var statusValue = (int)response.StatusCode;

                        if (statusValue == MtgApiError.BadRequest.Id)
                            throw new MtgApiException<BadRequestException>(MtgApiError.BadRequest.Description);
                        else if (statusValue == MtgApiError.Forbidden.Id)
                            throw new MtgApiException<ForbiddenException>(MtgApiError.Forbidden.Description);
                        else if (statusValue == MtgApiError.InternalServerError.Id)
                            throw new MtgApiException<InternalServerErrorException>(MtgApiError.InternalServerError.Description);
                        else if (statusValue == MtgApiError.NotFound.Id)
                            throw new MtgApiException<NotFoundException>(MtgApiError.NotFound.Description);
                        else if (statusValue == MtgApiError.ServiceUnavailable.Id)
                            throw new MtgApiException<ServiceUnavailableException>(MtgApiError.ServiceUnavailable.Description);

                        response.EnsureSuccessStatusCode();
                        return default;
                    }
                }
            }
        }
    }
}