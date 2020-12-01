// <copyright file="ApiVersionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;

    using MtgApiManager.Lib.Core;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ApiVersion"/> enumeration.
    /// </summary>

    public class ApiVersionTest
    {
        /// <summary>
        /// Tests the <see cref="ApiVersion.None"/> enumeration.
        /// </summary>
        [Fact]
        public void NoneTest()
        {
            Assert.Equal(0, (int)ApiVersion.None);

            var attribute = ApiVersion.None
                    .GetType()
                    .GetField(ApiVersion.None.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("none", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiVersion.V1_0"/> enumeration.
        /// </summary>
        [Fact]
        public void V1Test()
        {
            Assert.Equal(1, (int)ApiVersion.V1_0);

            var attribute = ApiVersion.V1_0
                    .GetType()
                    .GetField(ApiVersion.V1_0.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("v1", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }
    }
}