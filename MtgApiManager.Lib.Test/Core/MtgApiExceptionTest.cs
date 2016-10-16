// <copyright file="MtgApiExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="MtgApiException{T}"/> class.
    /// </summary>
    [TestClass]
    public class MtgApiExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiException{T}.MtgApiException(string)"/>
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            var exception = new MtgApiException<BadRequestException>("this is a test");
            Assert.AreEqual("this is a test", exception.Message);
        }
    }
}