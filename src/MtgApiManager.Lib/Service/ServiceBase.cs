using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Core.Exceptions;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    internal abstract class ServiceBase<TModel>
        where TModel : class
    {
        protected IHeaderManager _headerManager;
        private const string BASE_URL = "https://api.magicthegathering.io";

        private readonly IRateLimit _rateLimit;

        protected ServiceBase(
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            ApiVersion version,
            ApiEndPoint endpoint,
            IRateLimit rateLimit)
        {
            _headerManager = headerManager;
            ModelMapper = modelMapper;
            Version = version;
            EndPoint = endpoint;
            _rateLimit = rateLimit;

            ResetCurrentUrl();
        }

        protected static string BaseMtgUrl => BASE_URL;

        protected Url CurrentQueryUrl { get; private set; }

        protected ApiEndPoint EndPoint { get; }

        protected IModelMapper ModelMapper { get; }

        protected ApiVersion Version { get; }

        public abstract Task<IOperationResult<List<TModel>>> AllAsync();

        protected Task<T> CallWebServiceGet<T>(Url requestUrl) where T : IMtgResponse
        {
            if (requestUrl == null)
            {
                throw new ArgumentNullException(nameof(requestUrl));
            }

            return CallWebServiceGetInternal<T>(requestUrl);
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

        private async Task<T> CallWebServiceGetInternal<T>(Url requestUrl) where T : IMtgResponse
        {
            // Makes sure that th rate limit is not reached.
            if (_rateLimit.IsTurnedOn)
            {
                var rateLimit = _headerManager.Get<int>(ResponseHeader.RatelimitLimit);
                await _rateLimit.Delay(rateLimit).ConfigureAwait(false);
                _rateLimit.AddApiCall();
            }

            IFlurlResponse response = null;
            try
            {
                response = await requestUrl.GetAsync().ConfigureAwait(false);
                _headerManager.Update(response.Headers);
                return await response.GetJsonAsync<T>().ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync().ConfigureAwait(false);

                if (response == null)
                {
                    throw new BadRequestException(error);
                }

                var mtgException = TranslateException(response.StatusCode);
                if (mtgException == null)
                {
                    throw new BadRequestException(error);
                }

                throw mtgException;
            }
        }

        private Exception TranslateException(int statusCode)
        {
            if (statusCode == MtgApiError.BadRequest.Id)
                return new MtgApiException<BadRequestException>(MtgApiError.BadRequest.Description);
            else if (statusCode == MtgApiError.Forbidden.Id)
                return new MtgApiException<ForbiddenException>(MtgApiError.Forbidden.Description);
            else if (statusCode == MtgApiError.InternalServerError.Id)
                return new MtgApiException<InternalServerErrorException>(MtgApiError.InternalServerError.Description);
            else if (statusCode == MtgApiError.NotFound.Id)
                return new MtgApiException<NotFoundException>(MtgApiError.NotFound.Description);
            else if (statusCode == MtgApiError.ServiceUnavailable.Id)
                return new MtgApiException<ServiceUnavailableException>(MtgApiError.ServiceUnavailable.Description);
            else
                return null;
        }
    }
}