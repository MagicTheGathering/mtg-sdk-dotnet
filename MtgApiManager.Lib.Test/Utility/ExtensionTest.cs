// <copyright file="ExtensionTest.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using System;
    using Dto.Cards;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Utility;

    /// <summary>
    /// Tests methods in the <see cref="Extension"/> class.
    /// </summary>
    [TestClass]
    public class ExtensionTest
    {
        /// <summary>
        /// Tests the <see cref="Extensions.GetDescription(Enum)"/> extension method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDescriptionTest()
        {
            Enum testObject = null;

            // Test exception is thrown.
            testObject.GetDescription();

            // Test when no attribute.
            testObject = EnumTestObject.NoDescription;
            Assert.AreEqual("NoDescription", testObject.GetDescription());

            // Test when different attribute.
            testObject = EnumTestObject.DifferentAttribute;
            Assert.AreEqual("DifferentAttribute", testObject.GetDescription());

            // Test when description is present.
            testObject = EnumTestObject.HasDescription;
            Assert.AreEqual("greatDescription", testObject.GetDescription());
        }
    }
}
