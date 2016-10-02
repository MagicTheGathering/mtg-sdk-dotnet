// <copyright file="NotFoundExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="NotFoundException"/> class.
    /// </summary>
    [TestClass]
    public class NotFoundExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="NotFoundException.NotFoundException(string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            Assert.IsTrue(typeof(NotFoundException).IsSubclassOf(typeof(MtgExceptionBase)));

            NotFoundException exception = new NotFoundException("testing");
            Assert.AreEqual("MTG Api Error, testing", exception.Message);
        }
    }
}