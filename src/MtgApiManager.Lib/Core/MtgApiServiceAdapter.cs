namespace MtgApiManager.Lib.Core
{
    using Dto;
    using Exceptions;
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Utility;

    /// <summary>
    /// Used to make web service calls, and can easily be mocked for testing.
    /// </summary>
    internal class MtgApiServiceAdapter : IMtgApiServiceAdapter
    {
        public static async Task<T> WebGetAsyncInternal<T>(Uri requestUri) where T : IMtgResponse
        {
            using var client = new HttpClient();
            using var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                MtgApiController.ParseHeaders(response.Headers);
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                return await JsonSerializer.DeserializeAsync<T>(stream).ConfigureAwait(false);
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
                        return default;
                }
            }
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
    }
}