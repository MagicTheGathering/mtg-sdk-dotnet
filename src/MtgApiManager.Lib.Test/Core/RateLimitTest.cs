// <copyright file="RateLimitTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using MtgApiManager.Lib.Core;
    using System.Threading;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="RateLimit"/> class.
    /// </summary>

    public class RateLimitTest
    {

        /// <summary>
        /// Tests the <see cref="RateLimit.GetDelay(int)"/> method.
        /// </summary>
        [Fact]
        public void GetDelayTest()
        {
            var limit = new RateLimit();

            // Test passing 0 for the request per hour.
            Assert.Equal(0, limit.GetDelay(0));

            // No web calls yet.
            Assert.Equal(0, limit.GetDelay(2000));

            // Test being under the limit.
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Assert.Equal(0, limit.GetDelay(2000));

            // Test being over the limit.
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Thread.Sleep(500);
            limit.AddApiCall();
            Assert.NotEqual(0, limit.GetDelay(2000));
        }
    }
}