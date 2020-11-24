// <copyright file="QueryUtilityTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using Lib.Dto;
    using MtgApiManager.Lib.Utility;
    using System;
    using Xunit;

    /// <summary>
    /// Tests methods in the <see cref="QueryUtility"/> class.
    /// </summary>
    public class QueryUtilityTest
    {
        /// <summary>
        /// Tests the <see cref="QueryUtility.GetQueryPropertyName{T}(MemberExpression)"/> method.
        /// </summary>
        [Fact]
        public void GetQueryPropertyNameTest()
        {
            try
            {
                // Test exception is thrown.
                var result = QueryUtility.GetQueryPropertyName<CardDto>(null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("propertyName", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            // Property doesn't exist.
            Assert.Null(QueryUtility.GetQueryPropertyName<DtoTestObject>("fakeProperty"));

            // Attribute doesn't exist.
            Assert.Null(QueryUtility.GetQueryPropertyName<DtoTestObject>("Property1"));

            // Property exists.
            Assert.Equal("jsonProperty2", QueryUtility.GetQueryPropertyName<DtoTestObject>("Property2"));
        }
    }
}