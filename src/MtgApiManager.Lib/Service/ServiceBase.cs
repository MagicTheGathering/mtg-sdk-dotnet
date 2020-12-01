using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Utility;

namespace MtgApiManager.Lib.Service
{
    internal abstract class ServiceBase<TModel>
        where TModel : class
    {
        private const string BASE_URL = "https://api.magicthegathering.io";

        private readonly ApiEndPoint _endPoint;
        private readonly bool _isRateLimitOn;

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
            _endPoint = endpoint;
            _isRateLimitOn = rateLimitOn;
            WhereQueries = new Dictionary<string, string>();
        }

        protected IMtgApiServiceAdapter Adapter { get; }

        protected static string BaseMtgUrl => BASE_URL;

        protected IModelMapper ModelMapper { get; }

        protected ApiVersion Version { get; }

        protected Dictionary<string, string> WhereQueries { get; }

        public abstract Task<Exceptional<List<TModel>>> AllAsync();

        protected Uri BuildUri(Dictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var urlBuilder = new UriBuilder(
                new Uri(
                    new Uri(BaseMtgUrl),
                    string.Concat(Version.GetDescription(), "/", _endPoint.GetDescription())));

            var paramList = parameters
                .Select(p => $"{p.Key}={p.Value}")
                .ToList();

            urlBuilder.Query = Uri.EscapeUriString(string.Join("&", paramList));

            return urlBuilder.Uri;
        }

        protected Uri BuildUri(string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException(nameof(parameterValue));
            }

            return new Uri(
                new Uri(BaseMtgUrl),
                string.Concat(Version.GetDescription(), "/", _endPoint.GetDescription(), "/", parameterValue));
        }

        protected Task<T> CallWebServiceGet<T>(Uri requestUri) where T : IMtgResponse
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return CallWebServiceGetInternal<T>(requestUri);
        }

        protected async Task<T> CallWebServiceGetInternal<T>(Uri requestUri) where T : IMtgResponse
        {
            // Makes sure that th rate limit is not reached.
            if (_isRateLimitOn)
            {
                await MtgApiController.HandleRateLimit().ConfigureAwait(false);
            }

            return await Adapter.WebGetAsync<T>(requestUri).ConfigureAwait(false);
        }
    }
}