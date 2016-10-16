// <copyright file="WebUtilityTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;
    using Lib.Core;
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the functionality of the <see cref="MtgApiServiceAdapter"/> class.
    /// </summary>
    [TestClass]
    public class MtgApiServiceAdapterTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiServiceAdapter.WebGetAsync(Uri)"/> method.
        /// </summary>
        [TestMethod]
        public void WebGetAsyncTest()
        {
            MtgApiServiceAdapter adapter = new MtgApiServiceAdapter();

            try
            {
                // Test exception is thrown.
                var result = adapter.WebGetAsync<RootCardDto>(null).Result;
                Assert.Fail();
            }
            catch (AggregateException ex)
            {
                Assert.AreEqual("requestUri", ((ArgumentNullException)ex.Flatten().InnerException).ParamName);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}