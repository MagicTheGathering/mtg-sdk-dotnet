// <copyright file="MtgApiControllerTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;
    using System.Net.Http;
    using Xunit;


    /// <summary>
    /// Tests the <see cref="MtgApiController"/> class.
    /// </summary>

    public class MtgApiControllerTest
    {
        /// <summary>
        /// Tests the ParseHeaders method.
        /// </summary>
        [Fact]
        public void ParseHeadersTest()
        {
            try
            {
                // Test exception is thrown.
                MtgApiController.ParseHeaders(null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("headers", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            HttpResponseMessage response = new HttpResponseMessage();

            // No headers.
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Null(MtgApiController.Link);
            Assert.Equal(0, MtgApiController.PageSize);
            Assert.Equal(0, MtgApiController.Count);
            Assert.Equal(0, MtgApiController.TotalCount);
            Assert.Equal(0, MtgApiController.RatelimitLimit);
            Assert.Equal(0, MtgApiController.RatelimitRemaining);

            response.Headers.Add("Link", "fakelink");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Equal("fakelink", MtgApiController.Link);

            response.Headers.Add("Page-Size", "2000");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Equal(2000, MtgApiController.PageSize);

            response.Headers.Add("Count", "1000");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Equal(1000, MtgApiController.Count);

            response.Headers.Add("Total-Count", "3000");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Equal(3000, MtgApiController.TotalCount);

            response.Headers.Add("Ratelimit-Limit", "500");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Equal(500, MtgApiController.RatelimitLimit);

            response.Headers.Add("Ratelimit-Remaining", "250");
            MtgApiController.ParseHeaders(response.Headers);
            Assert.Equal(250, MtgApiController.RatelimitRemaining);
        }
    }
}