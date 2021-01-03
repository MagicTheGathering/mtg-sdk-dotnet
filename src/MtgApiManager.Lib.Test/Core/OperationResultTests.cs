using System;
using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class OperationResultTests
    {
        [Fact]
        public void Failure_Exception_NotNull()
        {
            // arrange
            var operationResult = OperationResult<string>.Failure(new Exception());

            // act
            var result = operationResult.Exception;

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Failure_IsSuccess_False()
        {
            // arrange
            var operationResult = OperationResult<string>.Failure(new Exception());

            // act
            var result = operationResult.IsSuccess;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Failure_PagingInfo_Null()
        {
            // arrange
            var operationResult = OperationResult<string>.Failure(new Exception());

            // act
            var result = operationResult.PagingInfo;

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Failure_Value_Null()
        {
            // arrange
            var operationResult = OperationResult<string>.Failure(new Exception());

            // act
            var result = operationResult.Value;

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Success_Exception_Null()
        {
            // arrange
            var pagingInfo = PagingInfo.Create(100, 50);
            var operationResult = OperationResult<string>.Success("value", pagingInfo);

            // act
            var result = operationResult.Exception;

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Success_IsSuccess_True()
        {
            // arrange
            var pagingInfo = PagingInfo.Create(100, 50);
            var operationResult = OperationResult<string>.Success("value", pagingInfo);

            // act
            var result = operationResult.IsSuccess;

            // assert
            Assert.True(result);
        }

        [Fact]
        public void Success_PagingInfo_NotNull()
        {
            // arrange
            var pagingInfo = PagingInfo.Create(100, 50);
            var operationResult = OperationResult<string>.Success("value", pagingInfo);

            // act
            var result = operationResult.PagingInfo;

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Success_Value_NotNull()
        {
            // arrange
            var pagingInfo = PagingInfo.Create(100, 50);
            var operationResult = OperationResult<string>.Success("value", pagingInfo);

            // act
            var result = operationResult.Value;

            // assert
            Assert.NotEmpty(result);
        }
    }
}