namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Threading.Tasks;
    using Lib.Core;
    using Lib.Dto;
    using Moq;
    using MtgApiManager.Lib.Service;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ServiceBase{TService, TModel}"/> class.
    /// </summary>

    public class ServiceBaseTest
    {
        /// <summary>
        /// Tests the CallWebServiceGet method.
        /// </summary>
        [Fact]
        public async Task CallWebServiceGetTest()
        {
            ServiceBaseObjectService service = new();

            // Test exception is thrown.
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.CallWebServiceGetTestMethod(null));
            Assert.Equal("requestUri", exception.ParamName);

            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardDto>(It.IsAny<Uri>()))
                .ReturnsAsync(new RootCardDto());

            service = new ServiceBaseObjectService(moqAdapter.Object);

            await service.CallWebServiceGetTestMethod(new Uri("http://fake/url"));
        }
    }
}