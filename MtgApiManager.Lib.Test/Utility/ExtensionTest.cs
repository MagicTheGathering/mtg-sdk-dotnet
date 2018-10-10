// <copyright file="ExtensionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using MtgApiManager.Lib.Utility;
    using System;
    using Xunit;

    /// <summary>
    /// Tests methods in the <see cref="Extension"/> class.
    /// </summary>

    public class ExtensionTest
    {
        /// <summary>
        /// Tests the <see cref="Extensions.GetDescription(Enum)"/> extension method.
        /// </summary>
        [Fact]
        public void GetDescriptionTest()
        {
            Enum testObject = null;

            try
            {
                // Test exception is thrown.
                testObject.GetDescription();
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("value", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            // Test when no attribute.
            testObject = EnumTestObject.NoDescription;
            Assert.Equal("NoDescription", testObject.GetDescription());

            // Test when different attribute.
            testObject = EnumTestObject.DifferentAttribute;
            Assert.Equal("DifferentAttribute", testObject.GetDescription());

            // Test when description is present.
            testObject = EnumTestObject.HasDescription;
            Assert.Equal("greatDescription", testObject.GetDescription());
        }
    }
}