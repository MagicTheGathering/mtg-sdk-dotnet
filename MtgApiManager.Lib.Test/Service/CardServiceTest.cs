// <copyright file="CardServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Lib.Core;
    using Lib.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Service;

    /// <summary>
    /// Tests the <see cref="CardService"/> class.
    /// </summary>
    [TestClass]
    public class CardServiceTest
    {
        /// <summary>
        /// Tests the constructors in the <see cref="CardService"/> class.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            CardService service;
            PrivateObject privateObject;

            service = new CardService();
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetField("_adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetField("_version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetField("_endpoint"));

            service = new CardService(new MtgApiServiceAdapter());
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetField("_adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetField("_version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetField("_endpoint"));

            service = new CardService(new MtgApiServiceAdapter(), ApiVersion.V1_0);
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetField("_adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetField("_version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetField("_endpoint"));
        }

        /// <summary>
        /// Tests the BuildUri method.
        /// </summary>
        [TestMethod]
        public void BuildUriTest()
        {
            CardService service = new CardService(); ;
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

            var url = privateObject.Invoke("BuildUri", new Type[] { typeof(NameValueCollection) } , new object[] { parameters }) as Uri;
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
        /// Tests the ParseHeaders method.
        /// </summary>
        [TestMethod]
        public void ParseHeadersTest()
        {
            CardService service = new CardService(); ;
            PrivateObject privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));

            try
            {
                // Test exception is thrown.
                privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { null });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("headers", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();

            // No headers.
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.IsNull(MtgApiController.Link);
            Assert.AreEqual(0, service.PageSize);
            Assert.AreEqual(0, service.Count);
            Assert.AreEqual(0, service.TotalCount);
            Assert.AreEqual(0, MtgApiController.RatelimitLimit);
            Assert.AreEqual(0, MtgApiController.RatelimitRemaining);

            response.Headers.Add("Link", "fakelink");
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.AreEqual("fakelink", MtgApiController.Link);

            response.Headers.Add("Page-Size", "2000");
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.AreEqual(2000, service.PageSize);

            response.Headers.Add("Count", "1000");
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.AreEqual(1000, service.Count);

            response.Headers.Add("Total-Count", "3000");
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.AreEqual(3000, service.TotalCount);

            response.Headers.Add("Ratelimit-Limit", "500");
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.AreEqual(500, MtgApiController.RatelimitLimit);

            response.Headers.Add("Ratelimit-Remaining", "250");
            privateObject.Invoke("ParseHeaders", new Type[] { typeof(HttpResponseHeaders) }, new object[] { response.Headers });
            Assert.AreEqual(250, MtgApiController.RatelimitRemaining);
        }
    }
}