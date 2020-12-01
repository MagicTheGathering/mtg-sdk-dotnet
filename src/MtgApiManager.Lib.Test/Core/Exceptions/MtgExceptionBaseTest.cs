// <copyright file="MtgExceptionBaseTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Xunit;


    /// <summary>
    /// Tests the <see cref="MtgExceptionBase"/> class.
    /// </summary>

    public class MtgExceptionBaseTest
    {
        /// <summary>
        /// Tests the <see cref="MtgExceptionBase.MtgExceptionBase(string)"/> constructor.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            MtgExceptionBase exception = new MtgExceptionBase("testing");
            Assert.Equal("MTG Api Error, testing", exception.Message);
        }
    }
}