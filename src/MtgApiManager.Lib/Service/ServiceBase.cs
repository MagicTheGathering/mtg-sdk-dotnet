using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    internal abstract class ServiceBase<TModel>
        where TModel : class
    {
        private const string BASE_URL = "https://api.magicthegathering.io";

        private readonly IHeaderManager _headerManager;
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

        protected async Task<T> CallWebServiceGet<T>(Url requestUrl, CancellationToken cancellationToken) where T : IMtgResponse
        {
            try
            {
                // Makes sure that the rate limit is not reached.
                if (_rateLimit.IsTurnedOn)
                {
                    var rateLimit = _headerManager.Get<int>(ResponseHeader.RatelimitLimit);

                    await _rateLimit.Delay(rateLimit, cancellationToken).ConfigureAwait(false);

                    _rateLimit.AddApiCall();
                }

                using (var response = await requestUrl.GetAsync(HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    _headerManager.Update(response.Headers);

                    return await response.GetJsonAsync<T>().ConfigureAwait(false);
                }

            }
            catch (OperationCanceledException ex)
            {
                throw new MtgApiException(ex.Message);
            }
            catch (FlurlHttpException ex)
            {
                var message = TranslateExceptionMessage(ex.StatusCode.GetValueOrDefault());
                if (message == null)
                {
                    var error = await ex.GetResponseStringAsync().ConfigureAwait(false);
                    throw new MtgApiException(error);
                }

                throw new MtgApiException(message);
            }
        }

        protected PagingInfo GetPagingInfo()
        {
            var totalCount = _headerManager.Get<int>(ResponseHeader.TotalCount);
            var pageSize = _headerManager.Get<int>(ResponseHeader.PageSize);

            return PagingInfo.Create(totalCount, pageSize);
        }

        protected void ResetCurrentUrl() => CurrentQueryUrl = BASE_URL.AppendPathSegments(Version.Name, EndPoint.Name);

        private string TranslateExceptionMessage(int statusCode)
        {
            if (statusCode == MtgApiError.BadRequest.Id)
                return MtgApiError.BadRequest.Description;
            else if (statusCode == MtgApiError.Forbidden.Id)
                return MtgApiError.Forbidden.Description;
            else if (statusCode == MtgApiError.InternalServerError.Id)
                return MtgApiError.InternalServerError.Description;
            else if (statusCode == MtgApiError.NotFound.Id)
                return MtgApiError.NotFound.Description;
            else if (statusCode == MtgApiError.ServiceUnavailable.Id)
                return MtgApiError.ServiceUnavailable.Description;
            else
                return null;
        }
    }
}