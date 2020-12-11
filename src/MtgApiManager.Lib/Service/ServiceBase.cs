using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using Flurl;

namespace MtgApiManager.Lib.Service
{
    internal abstract class ServiceBase<TModel>
        where TModel : class
    {
        private const string BASE_URL = "https://api.magicthegathering.io";

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
            EndPoint = endpoint;
            _isRateLimitOn = rateLimitOn;

            ResetCurrentUrl();
        }

        protected static string BaseMtgUrl => BASE_URL;
        protected IMtgApiServiceAdapter Adapter { get; }
        protected Url CurrentQueryUrl { get; private set; }

        protected ApiEndPoint EndPoint { get; }

        protected IModelMapper ModelMapper { get; }

        protected ApiVersion Version { get; }

        public abstract Task<Exceptional<List<TModel>>> AllAsync();

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

        protected void ResetCurrentUrl()
        {
            CurrentQueryUrl = BASE_URL.AppendPathSegments(Version.Name, EndPoint.Name);
        }
    }
}