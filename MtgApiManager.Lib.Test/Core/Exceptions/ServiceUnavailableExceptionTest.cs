// <copyright file="ServiceUnavailableExceptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    using Lib.Core.Exceptions;
    using Xunit;


    /// <summary>
    /// Tests the <see cref="ServiceUnavailableException"/> class.
    /// </summary>

    public class ServiceUnavailableExceptionTest
    {
        /// <summary>
        /// Tests the <see cref="ServiceUnavailableException.ServiceUnavailableException(string)"/> constructor.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Assert.True(typeof(ServiceUnavailableException).IsSubclassOf(typeof(MtgExceptionBase)));

            ServiceUnavailableException exception = new ServiceUnavailableException("testing");
            Assert.Equal("MTG Api Error, testing", exception.Message);
        }
    }
}