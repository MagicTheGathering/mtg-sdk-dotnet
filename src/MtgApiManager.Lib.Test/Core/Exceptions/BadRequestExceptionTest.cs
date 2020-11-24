// <copyright file="BadRequestExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="BadRequestException"/> class.
    /// </summary>
    public class BadRequestExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="BadRequestException.BadRequestException(string)"/> constructor.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Assert.True(typeof(BadRequestException).IsSubclassOf(typeof(MtgExceptionBase)));

            BadRequestException exception = new BadRequestException("testing");
            Assert.Equal("MTG Api Error, testing", exception.Message);
        }
    }
}