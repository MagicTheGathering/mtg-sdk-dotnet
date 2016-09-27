// <copyright file="ApiVersionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="ApiVersion"/> enumeration.
    /// </summary>
    [TestClass]
    public class ApiVersionTest
    {
        /// <summary>
        /// Tests the <see cref="ApiVersion.None"/> enumeration.
        /// </summary>
        [TestMethod]
        public void NoneTest()
        {
            Assert.AreEqual(0, (int)ApiVersion.None);

            var attribute = ApiVersion.None
                    .GetType()
                    .GetField(ApiVersion.None.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("none", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiVersion.V1_0"/> enumeration.
        /// </summary>
        [TestMethod]
        public void V1Test()
        {
            Assert.AreEqual(1, (int)ApiVersion.V1_0);

            var attribute = ApiVersion.V1_0
                    .GetType()
                    .GetField(ApiVersion.V1_0.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("v1", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }
    }
}