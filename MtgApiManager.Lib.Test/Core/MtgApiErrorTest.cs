// <copyright file="MtgApiErrorTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="MtgApiError"/> enumeration.
    /// </summary>
    [TestClass]
    public class MtgApiErrorTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiError.BadRequest"/> enumeration.
        /// </summary>
        [TestMethod]
        public void BadRequestTest()
        {
            Assert.AreEqual(400, (int)MtgApiError.BadRequest);

            var attribute = MtgApiError.BadRequest
                    .GetType()
                    .GetField(MtgApiError.BadRequest.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("Your request is not valid", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.Forbidden"/> enumeration.
        /// </summary>
        [TestMethod]
        public void ForbiddenTest()
        {
            Assert.AreEqual(403, (int)MtgApiError.Forbidden);

            var attribute = MtgApiError.Forbidden
                    .GetType()
                    .GetField(MtgApiError.Forbidden.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("You have exceeded the rate limit", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.InternalServerError"/> enumeration.
        /// </summary>
        [TestMethod]
        public void InternalServerErrorTest()
        {
            Assert.AreEqual(500, (int)MtgApiError.InternalServerError);

            var attribute = MtgApiError.InternalServerError
                    .GetType()
                    .GetField(MtgApiError.InternalServerError.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("We had a problem with our server. Try again later.", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.None"/> enumeration.
        /// </summary>
        [TestMethod]
        public void NoneTest()
        {
            Assert.AreEqual(0, (int)MtgApiError.None);

            var attribute = MtgApiError.None
                    .GetType()
                    .GetField(MtgApiError.None.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("none", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.NotFound"/> enumeration.
        /// </summary>
        [TestMethod]
        public void NotFoundTest()
        {
            Assert.AreEqual(404, (int)MtgApiError.NotFound);

            var attribute = MtgApiError.NotFound
                    .GetType()
                    .GetField(MtgApiError.NotFound.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("The specified resource could not be found", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="MtgApiError.ServiceUnavailable"/> enumeration.
        /// </summary>
        [TestMethod]
        public void ServiceUnavailableTest()
        {
            Assert.AreEqual(503, (int)MtgApiError.ServiceUnavailable);

            var attribute = MtgApiError.ServiceUnavailable
                    .GetType()
                    .GetField(MtgApiError.ServiceUnavailable.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("We’re temporarily off line for maintenance. Please try again later.", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }
    }
}