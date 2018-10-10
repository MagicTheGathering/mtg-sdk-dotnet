// <copyright file="ServiceBase.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading.Tasks;
    using System.Web;
    using Core;
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
        protected const string BaseMtgUrl = "https://api.magicthegathering.io";

        private readonly bool _isRateLimitOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TService, TModel}"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="version">The version of the API (currently only 1 version.)</param>
        /// <param name="endpoint">The end point of the service.</param>
        /// <param name="rateLimitOn">Turn the rate limit on or off.</param>
        public ServiceBase(IMtgApiServiceAdapter serviceAdapter, ApiVersion version, ApiEndPoint endpoint, bool rateLimitOn)
        {
            Adapter = serviceAdapter;
            Version = version;
            EndPoint = endpoint;
            _isRateLimitOn = rateLimitOn;
        }

        /// <summary>
        /// Gets the adapter used to interact with the MTG API.
        /// </summary>
        protected IMtgApiServiceAdapter Adapter { get; }

        /// <summary>
        /// Gets the end point of the service.
        /// </summary>
        protected ApiEndPoint EndPoint { get; }

        /// <summary>
        /// Gets the version of the MTG API.
        /// </summary>
        protected ApiVersion Version { get; }

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
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            var urlBuilder = new UriBuilder(
                new Uri(
                    new Uri(BaseMtgUrl),
                    string.Concat(Version.GetDescription(), "/", EndPoint.GetDescription())));

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
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException("parameterValue");
            }

            return new Uri(
                new Uri(BaseMtgUrl),
                string.Concat(Version.GetDescription(), "/", EndPoint.GetDescription(), "/", parameterValue));
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
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }

            // Makes sure that th rate limit is not reached.
            if (_isRateLimitOn)
                await MtgApiController.HandleRateLimit().ConfigureAwait(false);

            return await Adapter.WebGetAsync<T>(requestUri).ConfigureAwait(false);
        }
    }
}