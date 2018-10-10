// <copyright file="NotFoundExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Xunit;


    /// <summary>
    /// Tests the <see cref="NotFoundException"/> class.
    /// </summary>

    public class NotFoundExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="NotFoundException.NotFoundException(string)"/> constructor.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Assert.True(typeof(NotFoundException).IsSubclassOf(typeof(MtgExceptionBase)));

            NotFoundException exception = new NotFoundException("testing");
            Assert.Equal("MTG Api Error, testing", exception.Message);
        }
    }
}