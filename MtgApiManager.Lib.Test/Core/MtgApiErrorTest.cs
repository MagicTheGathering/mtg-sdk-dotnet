// <copyright file="MtgApiErrorTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;

    using MtgApiManager.Lib.Core;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="MtgApiError"/> enumeration.
    /// </summary>

    public class MtgApiErrorTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiError.BadRequest"/> enumeration.
        /// </summary>
        [Fact]
        public void BadRequestTest()
        {
            Assert.Equal(400, (int)MtgApiError.BadRequest);

            var attribute = MtgApiError.BadRequest
                    .GetType()
                    .GetField(MtgApiError.BadRequest.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("Your request is not valid", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.Forbidden"/> enumeration.
        /// </summary>
        [Fact]
        public void ForbiddenTest()
        {
            Assert.Equal(403, (int)MtgApiError.Forbidden);

            var attribute = MtgApiError.Forbidden
                    .GetType()
                    .GetField(MtgApiError.Forbidden.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("You have exceeded the rate limit", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.InternalServerError"/> enumeration.
        /// </summary>
        [Fact]
        public void InternalServerErrorTest()
        {
            Assert.Equal(500, (int)MtgApiError.InternalServerError);

            var attribute = MtgApiError.InternalServerError
                    .GetType()
                    .GetField(MtgApiError.InternalServerError.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("We had a problem with our server. Try again later.", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.None"/> enumeration.
        /// </summary>
        [Fact]
        public void NoneTest()
        {
            Assert.Equal(0, (int)MtgApiError.None);

            var attribute = MtgApiError.None
                    .GetType()
                    .GetField(MtgApiError.None.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("none", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.NotFound"/> enumeration.
        /// </summary>
        [Fact]
        public void NotFoundTest()
        {
            Assert.Equal(404, (int)MtgApiError.NotFound);

            var attribute = MtgApiError.NotFound
                    .GetType()
                    .GetField(MtgApiError.NotFound.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("The specified resource could not be found", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.ServiceUnavailable"/> enumeration.
        /// </summary>
        [Fact]
        public void ServiceUnavailableTest()
        {
            Assert.Equal(503, (int)MtgApiError.ServiceUnavailable);

            var attribute = MtgApiError.ServiceUnavailable
                    .GetType()
                    .GetField(MtgApiError.ServiceUnavailable.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("We’re temporarily off line for maintenance. Please try again later.", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }
    }
}