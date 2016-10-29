// <copyright file="CardTypeService.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;
    using MtgApiManager.Lib.Core;
    using Utility;

    /// <summary>
    /// Object representing a MTG card type.
    /// </summary>
    public class CardTypeService : ServiceBase<CardTypeService, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardTypeService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        public CardTypeService(IMtgApiServiceAdapter serviceAdapter)
            : this(serviceAdapter, ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardTypeService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        public CardTypeService()
            : this(new MtgApiServiceAdapter(), ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardTypeService"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="version">The version of the API</param>
        public CardTypeService(IMtgApiServiceAdapter serviceAdapter, ApiVersion version)
            : base(serviceAdapter, version, ApiEndPoint.CardTypes)
        {
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/>..
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{String}}"/> representing the result containing all the items.</returns>
        public override Exceptional<List<string>> All()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", this.EndPoint.GetDescription()));
                var rootTypeList = this.CallWebServiceGet<RootCardTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.Types);
            }
            catch (AggregateException ex)
            {
                return Exceptional<List<string>>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/>..
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{String}}"/> representing the result containing all the items.</returns>
        public async override Task<Exceptional<List<string>>> AllAsync()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", this.EndPoint.GetDescription()));
                var rootTypeList = await this.CallWebServiceGet<RootCardTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.Types);
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }
    }
}