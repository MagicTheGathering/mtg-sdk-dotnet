using System;
using MtgApiManager.Lib.Utility;
using Xunit;

namespace MtgApiManager.Lib.Test.Utility
{
    public class QueryUtilityTest
    {
        [Fact]
        public void GetQueryPropertyName_AttributeDoesNotExist_ReturnsNull()
        {
            // arrange
            // act
            var result = QueryUtility.GetQueryPropertyName<DtoTestObject>("Property1");

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void GetQueryPropertyName_PropertyDoesNotExist_ReturnsNull()
        {
            // arrange
            // act
            var result = QueryUtility.GetQueryPropertyName<DtoTestObject>("NotAPropertyName");

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void GetQueryPropertyName_PropertyNameNull_Throws()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => QueryUtility.GetQueryPropertyName<DtoTestObject>(null));
        }

        [Fact]
        public void GetQueryPropertyName_Success()
        {
            // arrange
            // act
            var result = QueryUtility.GetQueryPropertyName<DtoTestObject>("Property2");

            // assert
            Assert.Equal("jsonProperty2", result);
        }
    }
}