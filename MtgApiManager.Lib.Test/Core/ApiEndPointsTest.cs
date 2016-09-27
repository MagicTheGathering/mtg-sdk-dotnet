// <copyright file="ApiEndPointsTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="ApiEndPoint"/> enumeration.
    /// </summary>
    [TestClass]
    public class ApiEndPointsTest
    {
        /// <summary>
        /// Tests the <see cref="ApiEndPoint.Cards"/> enumeration.
        /// </summary>
        [TestMethod]
        public void CardsTest()
        {
            Assert.AreEqual(1, (int)ApiEndPoint.Cards);

            var attribute = ApiEndPoint.Cards
                    .GetType()
                    .GetField(ApiEndPoint.Cards.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("cards", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.CardSubTypes"/> enumeration.
        /// </summary>
        [TestMethod]
        public void CardSubTypesTest()
        {
            Assert.AreEqual(5, (int)ApiEndPoint.CardSubTypes);

            var attribute = ApiEndPoint.CardSubTypes
                    .GetType()
                    .GetField(ApiEndPoint.CardSubTypes.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("subtypes", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.CardSuperTypes"/> enumeration.
        /// </summary>
        [TestMethod]
        public void CardSuperTypesTest()
        {
            Assert.AreEqual(4, (int)ApiEndPoint.CardSuperTypes);

            var attribute = ApiEndPoint.CardSuperTypes
                    .GetType()
                    .GetField(ApiEndPoint.CardSuperTypes.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("supertypes", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.CardTypes"/> enumeration.
        /// </summary>
        [TestMethod]
        public void CardTypesTest()
        {
            Assert.AreEqual(3, (int)ApiEndPoint.CardTypes);

            var attribute = ApiEndPoint.CardTypes
                    .GetType()
                    .GetField(ApiEndPoint.CardTypes.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("types", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.None"/> enumeration.
        /// </summary>
        [TestMethod]
        public void NoneTest()
        {
            Assert.AreEqual(0, (int)ApiEndPoint.None);

            var attribute = ApiEndPoint.None
                    .GetType()
                    .GetField(ApiEndPoint.None.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("none", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }

        /// <summary>
        /// Tests the <see cref="ApiEndPoint.Sets"/> enumeration.
        /// </summary>
        [TestMethod]
        public void SetsTest()
        {
            Assert.AreEqual(2, (int)ApiEndPoint.Sets);

            var attribute = ApiEndPoint.Sets
                    .GetType()
                    .GetField(ApiEndPoint.Sets.ToString())
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                    .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("sets", ((System.ComponentModel.DescriptionAttribute)attribute).Description);
        }
    }
}