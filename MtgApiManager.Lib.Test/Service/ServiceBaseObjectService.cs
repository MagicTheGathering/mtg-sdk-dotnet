// <copyright file="ServiceBaseObjectService.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
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
            : base(new MtgApiServiceAdapter(), ApiVersion.V1_0, ApiEndPoint.Cards)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBaseObjectService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        /// <param name="adapter">The adapter to use.</param>
        public ServiceBaseObjectService(IMtgApiServiceAdapter adapter)
            : base(adapter, ApiVersion.V1_0, ApiEndPoint.Cards)
        {
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{TModel}}"/> representing the result containing all the items.</returns>
        public override Exceptional<List<Card>> All()
        {
            throw new NotImplementedException();
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
        public RootCardDto CallWebServiceGetTestMethod(Uri fakeUri)
        {
            return this.CallWebServiceGet<RootCardDto>(fakeUri).Result;
        }
    }
}