using Flurl.Http;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    /// <inheritdoc />
    public class MtgServiceProvider : IMtgServiceProvider
    {
        private readonly ApiVersion _apiVersion;
        private readonly IHeaderManager _headerManager;
        private readonly IModelMapper _modelMapper;
        private readonly IRateLimit _rateLimit;

        /// <summary>
        /// Initializes a new instance of the service provider.
        /// </summary>
        public MtgServiceProvider()
            : this(new HeaderManager(), new ModelMapper(), new RateLimit(true))
        {
        }

        internal MtgServiceProvider(
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            IRateLimit rateLimit)
        {
            _headerManager = headerManager;
            _modelMapper = modelMapper;
            _rateLimit = rateLimit;
            _apiVersion = ApiVersion.V1;

            FlurlHttp.Configure(settings =>
            {
                settings.JsonSerializer = new SystemTextJsonSerializer();
            });
        }

        /// <inheritdoc />
        public ICardService GetCardService()
        {
            return new CardService(
                _headerManager,
                _modelMapper,
                _apiVersion,
                _rateLimit);
        }

        /// <inheritdoc />
        public ISetService GetSetService()
        {
            return new SetService(
                _headerManager,
                _modelMapper,
                _apiVersion,
                _rateLimit);
        }
    }
}