// <copyright file="ForbiddenExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ForbiddenException"/> class.
    /// </summary>
    [TestClass]
    public class ForbiddenExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="ForbiddenException.ForbiddenException"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            Assert.IsTrue(typeof(ForbiddenException).IsSubclassOf(typeof(MtgExceptionBase)));

            ForbiddenException exception = new ForbiddenException("testing");
            Assert.AreEqual("MTG Api Error, testing", exception.Message);
        }
    }
}