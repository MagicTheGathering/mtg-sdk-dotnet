using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Utility;

namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Base class for a service.
    /// </summary>
    /// <typeparam name="TModel">The type of model used by the service.</typeparam>
    public abstract class ServiceBase<TModel>
        where TModel : class
    {
        private const string BASE_URL = "https://api.magicthegathering.io";

        private readonly bool _isRateLimitOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TModel}"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="modelMapper">Used to map entity objects to models.</param>
        /// <param name="version">The version of the API (currently only 1 version.)</param>
        /// <param name="endpoint">The end point of the service.</param>
        /// <param name="rateLimitOn">Turn the rate limit on or off.</param>
        protected ServiceBase(
            IMtgApiServiceAdapter serviceAdapter,
            IModelMapper modelMapper,
            ApiVersion version,
            ApiEndPoint endpoint,
            bool rateLimitOn)
        {
            Adapter = serviceAdapter;
            ModelMapper = modelMapper;
            Version = version;
            EndPoint = endpoint;
            _isRateLimitOn = rateLimitOn;
            WhereQueries = new Dictionary<string, string>();
        }

        protected IMtgApiServiceAdapter Adapter { get; }
        protected string BaseMtgUrl => BASE_URL;
        protected ApiEndPoint EndPoint { get; }

        protected IModelMapper ModelMapper { get; }

        protected ApiVersion Version { get; }

        /// <summary>
        /// Gets the list of where queries.
        /// </summary>
        protected Dictionary<string, string> WhereQueries { get; }

        /// <summary>
        /// Gets all the objects defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{T}"/> representing the result containing all the items.</returns>
        public abstract Task<Exceptional<List<TModel>>> AllAsync();

        /// <summary>
        /// Builds the URL to call the web service.
        /// </summary>
        /// <param name="parameters">The list of parameters to add to the query.</param>
        /// <returns>The URL to make the call with.</returns>
        protected Uri BuildUri(Dictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var urlBuilder = new UriBuilder(
                new Uri(
                    new Uri(BaseMtgUrl),
                    string.Concat(Version.GetDescription(), "/", EndPoint.GetDescription())));

            var paramList = parameters
                .Select(p => $"{p.Key}={p.Value}")
                .ToList();

            urlBuilder.Query = Uri.EscapeUriString(string.Join("&", paramList));

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
                throw new ArgumentNullException(nameof(parameterValue));
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
        protected Task<T> CallWebServiceGet<T>(Uri requestUri) where T : Dto.MtgResponseBase
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return CallWebServiceGetInternal<T>(requestUri);
        }

        protected async Task<T> CallWebServiceGetInternal<T>(Uri requestUri) where T : Dto.MtgResponseBase
        {
            // Makes sure that th rate limit is not reached.
            if (_isRateLimitOn)
                await MtgApiController.HandleRateLimit().ConfigureAwait(false);

            return await Adapter.WebGetAsync<T>(requestUri).ConfigureAwait(false);
        }
    }
}