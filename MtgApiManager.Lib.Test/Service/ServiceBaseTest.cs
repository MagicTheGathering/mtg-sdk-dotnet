// <copyright file="ServiceBaseTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Lib.Core;
    using Lib.Dto;
    using Lib.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MtgApiManager.Lib.Service;

    /// <summary>
    /// Tests the <see cref="ServiceBase{TService, TModel}"/> class.
    /// </summary>
    [TestClass]
    public class ServiceBaseTest
    {
        /// <summary>
        /// Tests the constructor <see cref="ServiceBase{TService, TModel}.ServiceBase(IMtgApiServiceAdapter, ApiVersion, ApiEndPoint)"/>.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            ServiceBaseObjectService service;
            PrivateObject privateObject;

            service = new ServiceBaseObjectService();
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));
        }

        /// <summary>
        /// Tests the BuildUri method.
        /// </summary>
        [TestMethod]
        public void BuildUriTest()
        {
            ServiceBaseObjectService service = new ServiceBaseObjectService();
            PrivateObject privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));

            // Test passing in a NameValueCollection.
            try
            {
                // Test exception is thrown.
                privateObject.Invoke("BuildUri", new Type[] { typeof(NameValueCollection) }, new object[] { null });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("parameters", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            NameValueCollection parameters = new NameValueCollection();
            parameters["name"] = "fakename";
            parameters["address"] = "123fakestreet";
            parameters["province"] = "fakeprovince";

            var url = privateObject.Invoke("BuildUri", new Type[] { typeof(NameValueCollection) }, new object[] { parameters }) as Uri;
            Assert.AreEqual(new Uri("https://api.magicthegathering.io/v1/cards?name=fakename&address=123fakestreet&province=fakeprovince"), url);

            // Test passing in a parameter value.
            try
            {
                // Test exception is thrown.
                privateObject.Invoke("BuildUri", new Type[] { typeof(string) }, new object[] { null });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("parameterValue", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            url = privateObject.Invoke("BuildUri", new Type[] { typeof(string) }, new object[] { "100" }) as Uri;
            Assert.AreEqual(new Uri("https://api.magicthegathering.io/v1/cards/100"), url);
        }

        /// <summary>
        /// Tests the CallWebServiceGet method.
        /// </summary>
        [TestMethod]
        public void CallWebServiceGetTest()
        {
            ServiceBaseObjectService service = new ServiceBaseObjectService();            

            try
            {
                // Test exception is thrown.
                service.CallWebServiceGetTestMethod(null);
                Assert.Fail();
            }
            catch (AggregateException ex)
            {
                Assert.AreEqual("requestUri", ((ArgumentNullException)ex.Flatten().InnerException).ParamName);
            }
            catch(Exception)
            {
                Assert.Fail();
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
