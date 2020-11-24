// <copyright file="InternalServerErrorExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Xunit;


    /// <summary>
    /// Tests the <see cref="InternalServerErrorException"/> class.
    /// </summary>

    public class InternalServerErrorExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="InternalServerErrorException.InternalServerErrorException(string)"/> constructor.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Assert.True(typeof(InternalServerErrorException).IsSubclassOf(typeof(MtgExceptionBase)));

            InternalServerErrorException exception = new InternalServerErrorException("testing");
            Assert.Equal("MTG Api Error, testing", exception.Message);
        }
    }
}