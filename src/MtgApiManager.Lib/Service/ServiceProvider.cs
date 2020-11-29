using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    /// <inheritdoc />
    public class MtgServiceProvider : IMtgServiceProvider
    {
        private readonly ApiVersion _apiVersion;
        private readonly IMtgApiServiceAdapter _serviceAdapter;
        private readonly IModelMapper _modelMapper;
        private readonly bool _rateLimitOn;
        private ICardService _cardService;
        private ISetService _setService;

        /// <summary>
        /// Initializes a new instance of the service provider.
        /// </summary>
        public MtgServiceProvider()
            : this(new MtgApiServiceAdapter(), new ModelMapper())
        {
        }

        internal MtgServiceProvider(
            IMtgApiServiceAdapter serviceAdapter,
            IModelMapper modelMapper,
            bool rateLimitOn = true)
        {
            _serviceAdapter = serviceAdapter;
            _modelMapper = modelMapper;
            _rateLimitOn = rateLimitOn;
            _apiVersion = ApiVersion.V1_0;
        }

        /// <inheritdoc />
        public ICardService GetCardService()
        {
            return _cardService ??= new CardService(_serviceAdapter, _modelMapper, _apiVersion, _rateLimitOn);
        }

        /// <inheritdoc />
        public ISetService GetSetService()
        {
            return _setService ??= new SetService(_serviceAdapter, _modelMapper, _apiVersion, _rateLimitOn);
        }
    }

}