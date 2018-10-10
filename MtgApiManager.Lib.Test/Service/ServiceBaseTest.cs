// <copyright file="ServiceBaseTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using Lib.Core;
    using Lib.Dto;
    using Moq;
    using MtgApiManager.Lib.Service;
    using System;
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
        public void CallWebServiceGetTest()
        {
            ServiceBaseObjectService service = new ServiceBaseObjectService();            

            try
            {
                // Test exception is thrown.
                service.CallWebServiceGetTestMethod(null);
                Assert.True(false);
            }
            catch (AggregateException ex)
            {
                Assert.Equal("requestUri", ((ArgumentNullException)ex.Flatten().InnerException).ParamName);
            }
            catch(Exception)
            {
                Assert.True(false);
            }

            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardDto>(It.IsAny<Uri>()))
                .ReturnsAsync(new RootCardDto());

            service = new ServiceBaseObjectService(moqAdapter.Object);

            var result = service.CallWebServiceGetTestMethod(new Uri("http://fake/url"));
        }
    }
}
