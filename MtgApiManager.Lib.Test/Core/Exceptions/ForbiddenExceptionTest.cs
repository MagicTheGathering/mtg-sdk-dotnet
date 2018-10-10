// <copyright file="ForbiddenExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Xunit;


    /// <summary>
    /// Tests the <see cref="ForbiddenException"/> class.
    /// </summary>

    public class ForbiddenExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="ForbiddenException.ForbiddenException"/> constructor.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Assert.True(typeof(ForbiddenException).IsSubclassOf(typeof(MtgExceptionBase)));

            ForbiddenException exception = new ForbiddenException("testing");
            Assert.Equal("MTG Api Error, testing", exception.Message);
        }
    }
}