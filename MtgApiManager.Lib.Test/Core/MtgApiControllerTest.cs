// <copyright file="MtgApiControllerTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Lib.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="MtgApiController"/> class.
    /// </summary>
    [TestClass]
    public class MtgApiControllerTest
    {
        /// <summary>
        /// Tests the ParseHeaders method.
        /// </summary>
        [TestMethod]
        public void ParseHeadersTest()
        {
            try
            {
                // Test exception is thrown.
                MtgApiController.ParseHeaders(null);
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
            MtgApiController.ParseHeaders(response.Headers);
            Assert.IsNull(MtgApiController.Link);
            Assert.AreEqual(0, MtgApiController.PageSize);
            Assert.AreEqual(0, MtgApiController.Count);
            Assert.AreEqual(0, MtgApiController.TotalCount);
            Assert.AreEqual(0, MtgApiController.RatelimitLimit);
            Assert.AreEqual(0, MtgApiController.RatelimitRemaining);

            response.Headers.Add("Link", "fakelink");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.AreEqual("fakelink", MtgApiController.Link);

            response.Headers.Add("Page-Size", "2000");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.AreEqual(2000, MtgApiController.PageSize);

            response.Headers.Add("Count", "1000");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.AreEqual(1000, MtgApiController.Count);

            response.Headers.Add("Total-Count", "3000");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.AreEqual(3000, MtgApiController.TotalCount);

            response.Headers.Add("Ratelimit-Limit", "500");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.AreEqual(500, MtgApiController.RatelimitLimit);

            response.Headers.Add("Ratelimit-Remaining", "250");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.AreEqual(250, MtgApiController.RatelimitRemaining);

            var privateController = new PrivateType(typeof(MtgApiController));
            privateController.SetStaticFieldOrProperty("RatelimitLimit", 2000);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiController.HandleRateLimit"/> method.
        /// </summary>
        [TestMethod]
        public async Task HandleRateLimitTest()
        {
            // Test no delay.
            await MtgApiController.HandleRateLimit();

            // Test with delay.
            var privateController = new PrivateType(typeof(MtgApiController));
            privateController.SetStaticFieldOrProperty("RatelimitLimit", 2000);

            var limit = new RateLimit();
            var privateLimit = new PrivateObject(limit);
            privateLimit.SetFieldOrProperty("_webServiceCalls", new List<DateTime>()
            {
                DateTime.Now.AddSeconds(-1),
                DateTime.Now.AddSeconds(-2),
                DateTime.Now.AddSeconds(-3),
                DateTime.Now.AddSeconds(-4),
                DateTime.Now.AddSeconds(-5),
                DateTime.Now.AddSeconds(-8),
            });

            privateController.SetStaticFieldOrProperty("_apiRateLimit", limit);

            await MtgApiController.HandleRateLimit();
        }

        /// <summary>
        /// Make sure the rate limit is set back to 0 being that its static.
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            var privateController = new PrivateType(typeof(MtgApiController));
            privateController.SetStaticFieldOrProperty("RatelimitLimit", 0);
        }
    }
}