namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Lib.Dto;
    using Lib.Model;
    using Lib.Service;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Object used to test the abstract <see cref="ServiceBase{TService, TModel}"/> class.
    /// </summary>
    public class ServiceBaseObjectService : ServiceBase<CardService, Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBaseObjectService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        public ServiceBaseObjectService()
            : base(new MtgApiServiceAdapter(), new ModelMapper(), ApiVersion.V1_0, ApiEndPoint.Cards, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBaseObjectService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        /// <param name="adapter">The adapter to use.</param>
        public ServiceBaseObjectService(IMtgApiServiceAdapter adapter)
            : base(adapter, new ModelMapper(), ApiVersion.V1_0, ApiEndPoint.Cards, false)
        {
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{Card}}"/> representing the result containing all the items.</returns>
        public override Task<Exceptional<List<Card>>> AllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to help with the testing of <see cref="ServiceBase{TService, TModel}.CallWebServiceGet{T}(Uri)"/>.
        /// </summary>
        /// <param name="fakeUri"></param>
        public Task<RootCardDto> CallWebServiceGetTestMethod(Uri fakeUri)
        {
            return CallWebServiceGet<RootCardDto>(fakeUri);
        }
    }
}