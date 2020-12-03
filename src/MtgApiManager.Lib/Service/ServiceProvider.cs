using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    /// <inheritdoc />
    public class MtgServiceProvider : IMtgServiceProvider
    {
        private readonly ApiVersion _apiVersion;
        private readonly IModelMapper _modelMapper;
        private readonly bool _rateLimitOn;
        private readonly IMtgApiServiceAdapter _serviceAdapter;

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
            _apiVersion = ApiVersion.V1;
        }

        /// <inheritdoc />
        public ICardService GetCardService()
        {
            return new CardService(_serviceAdapter, _modelMapper, _apiVersion, _rateLimitOn);
        }

        /// <inheritdoc />
        public ISetService GetSetService()
        {
            return new SetService(_serviceAdapter, _modelMapper, _apiVersion, _rateLimitOn);
        }
    }
}