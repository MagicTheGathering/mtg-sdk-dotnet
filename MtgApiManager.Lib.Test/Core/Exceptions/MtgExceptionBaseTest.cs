// <copyright file="MtgExceptionBaseTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="MtgExceptionBase"/> class.
    /// </summary>
    [TestClass]
    public class MtgExceptionBaseTest
    {
        /// <summary>
        /// Tests the <see cref="MtgExceptionBase.MtgExceptionBase(string)"/> constructor.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            MtgExceptionBase exception = new MtgExceptionBase("testing");
            Assert.AreEqual("MTG Api Error, testing", exception.Message);
        }
    }
}