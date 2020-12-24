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
        private readonly IMtgApiServiceAdapter _serviceAdapter;

        /// <summary>
        /// Initializes a new instance of the service provider.
        /// </summary>
        public MtgServiceProvider()
        {
            _rateLimit = new RateLimit(true);
            _headerManager = new HeaderManager();
            _serviceAdapter = new MtgApiServiceAdapter(_headerManager);
            _modelMapper = new ModelMapper();
            _apiVersion = ApiVersion.V1;
        }

        internal MtgServiceProvider(
            IMtgApiServiceAdapter serviceAdapter,
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            IRateLimit rateLimit)
        {
            _serviceAdapter = serviceAdapter;
            _headerManager = headerManager;
            _modelMapper = modelMapper;
            _rateLimit = rateLimit;
            _apiVersion = ApiVersion.V1;
        }

        /// <inheritdoc />
        public ICardService GetCardService()
        {
            return new CardService(
                _serviceAdapter,
                _headerManager,
                _modelMapper,
                _apiVersion,
                _rateLimit);
        }

        /// <inheritdoc />
        public ISetService GetSetService()
        {
            return new SetService(
                _serviceAdapter,
                _headerManager,
                _modelMapper,
                _apiVersion,
                _rateLimit);
        }
    }
}