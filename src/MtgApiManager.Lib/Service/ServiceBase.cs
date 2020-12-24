using System;
using System.Collections.Generic;
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
        protected IHeaderManager _headerManager;
        private const string BASE_URL = "https://api.magicthegathering.io";

        private readonly IRateLimit _rateLimit;

        protected ServiceBase(
            IMtgApiServiceAdapter serviceAdapter,
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            ApiVersion version,
            ApiEndPoint endpoint,
            IRateLimit rateLimit)
        {
            Adapter = serviceAdapter;
            _headerManager = headerManager;
            ModelMapper = modelMapper;
            Version = version;
            EndPoint = endpoint;
            _rateLimit = rateLimit;

            ResetCurrentUrl();
        }

        protected static string BaseMtgUrl => BASE_URL;

        protected IMtgApiServiceAdapter Adapter { get; }

        protected Url CurrentQueryUrl { get; private set; }

        protected ApiEndPoint EndPoint { get; }

        protected IModelMapper ModelMapper { get; }

        protected ApiVersion Version { get; }

        public abstract Task<IOperationResult<List<TModel>>> AllAsync();

        protected Task<T> CallWebServiceGet<T>(Uri requestUri) where T : IMtgResponse
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return CallWebServiceGetInternal<T>(requestUri);
        }

        protected PagingInfo GetPagingInfo()
        {
            var totalCount = _headerManager.Get<int>(ResponseHeader.TotalCount);
            var pageSize = _headerManager.Get<int>(ResponseHeader.PageSize);

            return PagingInfo.Create(totalCount, pageSize);
        }

        protected void ResetCurrentUrl()
        {
            CurrentQueryUrl = BASE_URL.AppendPathSegments(Version.Name, EndPoint.Name);
        }

        private async Task<T> CallWebServiceGetInternal<T>(Uri requestUri) where T : IMtgResponse
        {
            // Makes sure that th rate limit is not reached.
            if (_rateLimit.IsTurnedOn)
            {
                var rateLimit = _headerManager.Get<int>(ResponseHeader.RatelimitLimit);
                await _rateLimit.Delay(rateLimit).ConfigureAwait(false);

                var result = await Adapter.WebGetAsync<T>(requestUri).ConfigureAwait(false);
                _rateLimit.AddApiCall();

                return result;
            }

            return await Adapter.WebGetAsync<T>(requestUri).ConfigureAwait(false);
        }
    }
}