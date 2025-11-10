using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Flurl.Util;
using NSubstitute;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class ServiceBaseTests
    {
        private readonly IHeaderManager _mockHeaderManager;
        private readonly IModelMapper _mockModelMapper;
        private readonly IRateLimit _mockRateLimit;

        public ServiceBaseTests()
        {
            _mockHeaderManager = Substitute.For<IHeaderManager>();
            _mockModelMapper = Substitute.For<IModelMapper>();
            _mockRateLimit = Substitute.For<IRateLimit>();
        }

        [Fact]
        public async Task CallWebServiceGet_RateLimitOff_Success()
        {
            // arrange
            var URL = new Uri("http://fake/url");

            _mockRateLimit.IsTurnedOn.Returns(false);

            var card = new CardDto() { Id = "12345" };
            var rootCard = new RootCardDto()
            {
                Card = card,
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCard);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // act
            var result = await service.CallWebServiceGetTestMethod(URL);

            // assert
            Assert.Equal(card.Id, result.Card.Id);
        }

        [Fact]
        public async Task CallWebServiceGet_RateLimitOn_Success()
        {
            // arrange
            var URL = new Uri("http://fake/url");

            _mockRateLimit.IsTurnedOn.Returns(true);
            _mockRateLimit.Delay(2000, default).ReturnsForAnyArgs(1);
            _mockHeaderManager.Get<int>(ResponseHeader.RatelimitLimit).Returns(2000);

            var card = new CardDto() { Id = "12345" };
            var rootCard = new RootCardDto()
            {
                Card = card,
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCard);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // act
            var result = await service.CallWebServiceGetTestMethod(URL);

            // assert
            Assert.Equal(card.Id, result.Card.Id);
            await _mockRateLimit.Received(1).Delay(2000, default);
            _mockRateLimit.Received(1).AddApiCall();
        }

        [Fact]
        public void Constructor_Calls_ResetCurrentUrl()
        {
            // arrange
            // act
            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // assert
            Assert.NotNull(service.CurrentQueryUrlTestProp);
        }

        [Fact]
        public void GetPagingInfo_CountsCorrect()
        {
            // arrange
            const int TOTAL_COUNT = 100;
            const int PAGE_SIZE = 10;

            _mockHeaderManager.Get<int>(ResponseHeader.TotalCount).Returns(TOTAL_COUNT);
            _mockHeaderManager.Get<int>(ResponseHeader.PageSize).Returns(PAGE_SIZE);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // act
            var result = service.GetPagingInfoTestMethod();

            // assert
            Assert.Equal(TOTAL_COUNT, result.TotalCount);
            Assert.Equal(PAGE_SIZE, result.PageSize);
        }

        [Fact]
        public void GetPagingInfo_Success()
        {
            // arrange
            _mockHeaderManager.Get<int>(ResponseHeader.TotalCount).Returns(100);
            _mockHeaderManager.Get<int>(ResponseHeader.PageSize).Returns(10);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // act
            var result = service.GetPagingInfoTestMethod();

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ResetCurrentUrl_ResetsUrl_Success()
        {
            // arrange
            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            service.CurrentQueryUrlTestProp.AppendPathSegments("this", "is", "a", "test");

            // act
            service.ResetCurrentUrlTestMethod();

            // assert
            Assert.Equal(2, service.CurrentQueryUrlTestProp.PathSegments.Count);
        }

        [Theory]
        [ClassData(typeof(ErrorTestData))]
        public async Task CallWebServiceGet_ReturnsException_Throws(int id, string description)
        {
            // arrange
            var URL = new Uri("http://fake/url");

            _mockRateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.RespondWith("error", id);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // act
            // assert
            var result = await Assert.ThrowsAsync<MtgApiException>(
                () => service.CallWebServiceGetTestMethod(URL));
            Assert.EndsWith(description, result.Message);
        }

        [Fact]
        public async Task CallWebServiceGet_ExceptionStatusCodeUnknown_ThrowsBadRequest()
        {
            // arrange
            const string ERROR_MESSAGE = "something bad happened";

            var URL = new Uri("http://fake/url");

            _mockRateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.RespondWith(ERROR_MESSAGE, 410);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager,
                _mockModelMapper,
                _mockRateLimit);

            // act
            // assert
            var result = await Assert.ThrowsAsync<MtgApiException>(
                () => service.CallWebServiceGetTestMethod(URL));
            Assert.EndsWith(ERROR_MESSAGE, result.Message);
        }

        public class ErrorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() =>
                Enumeration.GetAll<MtgApiError>()
                .Where(x => x.Id != MtgApiError.None.Id)
                .Select(x => new object[] { x.Id, x.Description })
                .GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}