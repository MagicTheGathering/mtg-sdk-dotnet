using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace MtgApiManager.Lib.Test.Service
{
    internal class ServiceBaseTestObject : ServiceBase<Card>
    {
        public ServiceBaseTestObject(
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            IRateLimit rateLimit)
            : base(headerManager, modelMapper, ApiVersion.V1, ApiEndPoint.Cards, rateLimit)
        {
        }

        public Url CurrentQueryUrlTestProp => CurrentQueryUrl;

        public override Task<IOperationResult<List<Card>>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RootCardDto> CallWebServiceGetTestMethod(Uri fakeUri)
        {
            return CallWebServiceGet<RootCardDto>(fakeUri);
        }

        public PagingInfo GetPagingInfoTestMethod()
        {
            return GetPagingInfo();
        }

        public void ResetCurrentUrlTestMethod()
        {
            ResetCurrentUrl();
        }
    }
}