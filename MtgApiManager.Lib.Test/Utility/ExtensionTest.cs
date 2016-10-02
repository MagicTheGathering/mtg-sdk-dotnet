// <copyright file="ExtensionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using System;
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
        public void GetDescriptionTest()
        {
            Enum testObject = null;

            try
            {
                // Test exception is thrown.
                testObject.GetDescription();
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

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