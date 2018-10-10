// <copyright file="WebUtilityTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using Lib.Core;
    using Lib.Dto;
    using System;
    using Xunit;


    /// <summary>
    /// Tests the functionality of the <see cref="MtgApiServiceAdapter"/> class.
    /// </summary>

    public class MtgApiServiceAdapterTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiServiceAdapter.WebGetAsync(Uri)"/> method.
        /// </summary>
        [Fact]
        public void WebGetAsyncTest()
        {
            MtgApiServiceAdapter adapter = new MtgApiServiceAdapter();

            try
            {
                // Test exception is thrown.
                var result = adapter.WebGetAsync<RootCardDto>(null).Result;
                Assert.True(false);
            }
            catch (AggregateException ex)
            {
                Assert.Equal("requestUri", ((ArgumentNullException)ex.Flatten().InnerException).ParamName);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}