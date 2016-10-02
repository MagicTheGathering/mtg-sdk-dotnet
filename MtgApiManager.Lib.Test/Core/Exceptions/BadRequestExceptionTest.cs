// <copyright file="BadRequestExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="BadRequestException"/> class.
    /// </summary>
    [TestClass]
    public class BadRequestExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="BadRequestException.BadRequestException(string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            Assert.IsTrue(typeof(BadRequestException).IsSubclassOf(typeof(MtgExceptionBase)));

            BadRequestException exception = new BadRequestException("testing");
            Assert.AreEqual("MTG Api Error, testing", exception.Message);
        }
    }
}