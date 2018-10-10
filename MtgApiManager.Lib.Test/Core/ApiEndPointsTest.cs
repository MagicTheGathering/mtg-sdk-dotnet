// <copyright file="ApiEndPointsTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;

    using MtgApiManager.Lib.Core;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ApiEndPoint"/> enumeration.
    /// </summary>

    public class ApiEndPointsTest
    {
        /// <summary>
        /// Tests the <see cref="ApiEndPoint.Cards"/> enumeration.
        /// </summary>
        [Fact]
        public void CardsTest()
        {
            Assert.Equal(1, (int)ApiEndPoint.Cards);

            var attribute = ApiEndPoint.Cards
                    .GetType()
                    .GetField(ApiEndPoint.Cards.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("cards", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.CardSubTypes"/> enumeration.
        /// </summary>
        [Fact]
        public void CardSubTypesTest()
        {
            Assert.Equal(5, (int)ApiEndPoint.CardSubTypes);

            var attribute = ApiEndPoint.CardSubTypes
                    .GetType()
                    .GetField(ApiEndPoint.CardSubTypes.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("subtypes", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.CardSuperTypes"/> enumeration.
        /// </summary>
        [Fact]
        public void CardSuperTypesTest()
        {
            Assert.Equal(4, (int)ApiEndPoint.CardSuperTypes);

            var attribute = ApiEndPoint.CardSuperTypes
                    .GetType()
                    .GetField(ApiEndPoint.CardSuperTypes.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("supertypes", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.CardTypes"/> enumeration.
        /// </summary>
        [Fact]
        public void CardTypesTest()
        {
            Assert.Equal(3, (int)ApiEndPoint.CardTypes);

            var attribute = ApiEndPoint.CardTypes
                    .GetType()
                    .GetField(ApiEndPoint.CardTypes.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("types", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.None"/> enumeration.
        /// </summary>
        [Fact]
        public void NoneTest()
        {
            Assert.Equal(0, (int)ApiEndPoint.None);

            var attribute = ApiEndPoint.None
                    .GetType()
                    .GetField(ApiEndPoint.None.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("none", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.Sets"/> enumeration.
        /// </summary>
        [Fact]
        public void SetsTest()
        {
            Assert.Equal(2, (int)ApiEndPoint.Sets);

            var attribute = ApiEndPoint.Sets
                    .GetType()
                    .GetField(ApiEndPoint.Sets.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("sets", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }
    }
}