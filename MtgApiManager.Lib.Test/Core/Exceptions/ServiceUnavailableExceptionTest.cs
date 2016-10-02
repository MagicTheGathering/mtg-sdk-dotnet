// <copyright file="ServiceUnavailableExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ServiceUnavailableException"/> class.
    /// </summary>
    [TestClass]
    public class ServiceUnavailableExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="ServiceUnavailableException.ServiceUnavailableException(string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            Assert.IsTrue(typeof(ServiceUnavailableException).IsSubclassOf(typeof(MtgExceptionBase)));

            ServiceUnavailableException exception = new ServiceUnavailableException("testing");
            Assert.AreEqual("MTG Api Error, testing", exception.Message);
        }
    }
}