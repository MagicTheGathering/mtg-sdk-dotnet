// <copyright file="MtgApiExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using Lib.Core.Exceptions;

    using MtgApiManager.Lib.Core;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="MtgApiException{T}"/> class.
    /// </summary>

    public class MtgApiExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiException{T}.MtgApiException(string)"/>
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            var exception = new MtgApiException<BadRequestException>("this is a test");
            Assert.Equal("this is a test", exception.Message);
        }
    }
}