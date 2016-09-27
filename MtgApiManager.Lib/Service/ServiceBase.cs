// <copyright file="ServiceBase.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;
    using Core;
    using Core.Exceptions;
    using Newtonsoft.Json;
    using Utility;

    /// <summary>
    /// Base class for a service.
    /// </summary>
    /// <typeparam name="TService">The type of service.</typeparam>
    /// <typeparam name="TModel">The type of model used by the service.</typeparam>
    public abstract class ServiceBase<TService, TModel>
        where TService : class
        where TModel : class
    {
        /// <summary>
        /// The base URL to the MTG API.
        /// </summary>
        private const string BaseMtgUrl = "https://api.magicthegathering.io";

        /// <summary>
        /// The adapter used to interact with the MTG API.
        /// </summary>
        private IMtgApiServiceAdapter _adapter = null;

        /// <summary>
        /// The end point for the service.
        /// </summary>
        private ApiEndPoint _endpoint;

        /// <summary>
        /// The version of the API.
        /// </summary>
        private ApiVersion _version;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TService, TModel}"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="version">The version of the API (currently only 1 version.)</param>
        /// <param name="endpoint">The end point of the service.</param>
        public ServiceBase(IMtgApiServiceAdapter serviceAdapter, ApiVersion version, ApiEndPoint endpoint)
        {
            this._adapter = serviceAdapter;
            this._version = version;
            this._endpoint = endpoint;
        }

        /// <summary>
        /// Gets the number of elements returned.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the page size for the request.
        /// </summary>
        public int PageSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total number of elements (across all pages).
        /// </summary>
        public int TotalCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the adapter used to interact with the MTG API.
        /// </summary>
        protected IMtgApiServiceAdapter Adapter
        {
            get
            {
                return this._adapter;
            }
        }

        /// <summary>
        /// Gets the end point of the service.
        /// </summary>
        protected ApiEndPoint EndPoint
        {
            get
            {
                return this._endpoint;
            }
        }

        /// <summary>
        /// Gets the version of the MTG API.
        /// </summary>
        protected ApiVersion Version
        {
            get
            {
                return this._version;
            }
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{TModel}}"/> representing the result containing all the items.</returns>
        public abstract Exceptional<List<TModel>> All();

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{TModel}}"/> representing the result containing all the items.</returns>
        public abstract Task<Exceptional<List<TModel>>> AllAsync();

        /// <summary>
        /// Builds the URL to call the web service.
        /// </summary>
        /// <param name="parameters">The list of parameters to add to the query.</param>
        /// <returns>The URL to make the call with.</returns>
        protected Uri BuildUri(NameValueCollection parameters)
        {
            var urlBuilder = new UriBuilder(
                new Uri(
                    new Uri(BaseMtgUrl),
                    string.Concat(this._version.GetDescription(), "/", this._endpoint.GetDescription())));

            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query.Add(parameters);
            urlBuilder.Query = query.ToString();

            return urlBuilder.Uri;
        }

        /// <summary>
        /// Builds the URL to call the web service.
        /// </summary>
        /// <param name="parameterValue">The parameter value to add.</param>
        /// <returns>The URL to make the call with.</returns>
        protected Uri BuildUri(string parameterValue)
        {
            return new Uri(
                new Uri(BaseMtgUrl),
                string.Concat(this._version.GetDescription(), "/", this._endpoint.GetDescription(), "/", parameterValue));
        }

        /// <summary>
        /// Makes a GET call to the web service.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize the response into.</typeparam>
        /// <param name="requestUri">The URL to call.</param>
        /// <returns>The response of the GET serialized into the specified object.</returns>
        protected async Task<T> CallWebServiceGet<T>(Uri requestUri)
            where T : Dto.MtgResponseBase
        {
            var response = await this._adapter.WebGetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                // Get the custom mtg headers.
                this.ParseHeaders(response.Headers);

                // De serialize the response.
                T obj = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

                return obj;
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

        /// <summary>
        /// Gets all the related headers from the response.
        /// </summary>
        /// <param name="headers">The header to parse.</param>
        private void ParseHeaders(HttpResponseHeaders headers)
        {
            IEnumerable<string> resultHeaders = null;

            if (headers.TryGetValues("Link", out resultHeaders))
            {
                MtgApiController.Link = resultHeaders.FirstOrDefault();
            }

            if (headers.TryGetValues("Page-Size", out resultHeaders))
            {
                this.PageSize = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Count", out resultHeaders))
            {
                this.Count = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Total-Count", out resultHeaders))
            {
                this.TotalCount = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Ratelimit-Limit", out resultHeaders))
            {
                MtgApiController.RatelimitLimit = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Ratelimit-Remaining", out resultHeaders))
            {
                MtgApiController.RatelimitRemaining = int.Parse(resultHeaders.FirstOrDefault());
            }
        }
    }
}