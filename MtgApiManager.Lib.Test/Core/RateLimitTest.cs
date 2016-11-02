// <copyright file="RateLimitTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="RateLimit"/> class.
    /// </summary>
    [TestClass]
    public class RateLimitTest
    {
        /// <summary>
        /// Tests the <see cref="RateLimit.AddApiCall"/> method.
        /// </summary>
        [TestMethod]
        public void AddApiCallTest()
        {
            var limit = new RateLimit();
            var privateObject = new PrivateObject(limit);

            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();

            Assert.AreEqual(3, (privateObject.GetFieldOrProperty("_webServiceCalls") as List<DateTime>).Count);
        }

        /// <summary>
        /// Tests the <see cref="RateLimit.RateLimit"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructerTest()
        {
            var limit = new RateLimit();
            var privateObject = new PrivateObject(limit);
            Assert.IsNotNull(privateObject.GetFieldOrProperty("_webServiceCalls"));
        }

        /// <summary>
        /// Tests the <see cref="RateLimit.GetDelay(int)"/> method.
        /// </summary>
        [TestMethod]
        public void GetDelayTest()
        {
            var limit = new RateLimit();

            // Test passing 0 for the request per hour.
            Assert.AreEqual(0, limit.GetDelay(0));

            // No web calls yet.
            Assert.AreEqual(0, limit.GetDelay(2000));

            // Test being under the limit.
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Assert.AreEqual(0, limit.GetDelay(2000));

            // Test being over the limit.
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Assert.AreNotEqual(0, limit.GetDelay(2000));
        }
    }
}