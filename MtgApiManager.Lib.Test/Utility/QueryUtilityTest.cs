// <copyright file="QueryUtilityTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using System;
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Utility;

    /// <summary>
    /// Tests methods in the <see cref="QueryUtility"/> class.
    /// </summary>
    [TestClass]
    public class QueryUtilityTest
    {
        /// <summary>
        /// Tests the <see cref="QueryUtility.GetQueryPropertyName{T}(MemberExpression)"/> method.
        /// </summary>
        [TestMethod]
        public void GetQueryPropertyNameTest()
        {
            try
            {
                // Test exception is thrown.
                var result = QueryUtility.GetQueryPropertyName<CardDto>(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("propertyName", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            // Property doesn't exist.
            Assert.IsNull(QueryUtility.GetQueryPropertyName<DtoTestObject>("fakeProperty"));

            // Attribute doesn't exist.
            Assert.IsNull(QueryUtility.GetQueryPropertyName<DtoTestObject>("Property1"));

            // Property exists.
            Assert.AreEqual("jsonProperty2", QueryUtility.GetQueryPropertyName<DtoTestObject>("Property2"));
        }
    }
}