// <copyright file="InternalServerErrorExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="InternalServerErrorException"/> class.
    /// </summary>
    [TestClass]
    public class InternalServerErrorExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="InternalServerErrorException.InternalServerErrorException(string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            Assert.IsTrue(typeof(InternalServerErrorException).IsSubclassOf(typeof(MtgExceptionBase)));

            InternalServerErrorException exception = new InternalServerErrorException("testing");
            Assert.AreEqual("MTG Api Error, testing", exception.Message);
        }
    }
}