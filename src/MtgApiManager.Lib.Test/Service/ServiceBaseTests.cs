using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Flurl.Util;
using Moq;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class ServiceBaseTests
    {
        private readonly Mock<IHeaderManager> _mockHeaderManager;
        private readonly Mock<IModelMapper> _mockModelMapper;
        private readonly Mock<IRateLimit> _mockRateLimit;
        private readonly MockRepository _mockRepository;

        public ServiceBaseTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockHeaderManager = _mockRepository.Create<IHeaderManager>();
            _mockModelMapper = _mockRepository.Create<IModelMapper>();
            _mockRateLimit = _mockRepository.Create<IRateLimit>();
        }

        [Fact]
        public async Task CallWebServiceGet_RateLimitOff_Success()
        {
            // arrange
            var URL = new Uri("http://fake/url");

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            var card = new CardDto() { Id = "12345" };
            var rootCard = new RootCardDto()
            {
                Card = card,
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCard);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));

            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

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

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(true);
            _mockRateLimit.Setup(x => x.Delay(2000, default)).ReturnsAsync(1);
            _mockRateLimit.Setup(x => x.AddApiCall());

            var card = new CardDto() { Id = "12345" };
            var rootCard = new RootCardDto()
            {
                Card = card,
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCard);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.RatelimitLimit)).Returns(2000);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

            // act
            var result = await service.CallWebServiceGetTestMethod(URL);

            // assert
            Assert.Equal(card.Id, result.Card.Id);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void Constructor_Calls_ResetCurrentUrl()
        {
            // arrange
            // act
            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

            // assert
            Assert.NotNull(service.CurrentQueryUrlTestProp);
        }

        [Fact]
        public void GetPagingInfo_CountsCorrect()
        {
            // arrange
            const int TOTAL_COUNT = 100;
            const int PAGE_SIZE = 10;

            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(TOTAL_COUNT);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(PAGE_SIZE);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

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
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(100);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(10);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

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
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

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

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            using var httpTest = new HttpTest();
            httpTest.RespondWith("error", id);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

            // act
            // assert
            var result = await Assert.ThrowsAsync<MtgApiException>(                
                () => service.CallWebServiceGetTestMethod(URL));
            Assert.EndsWith(description, result.Message);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CallWebServiceGet_ExceptionStatusCodeUnknown_ThrowsBadRequest()
        {
            // arrange
            const string ERROR_MESSAGE = "something bad happened";

            var URL = new Uri("http://fake/url");

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            using var httpTest = new HttpTest();
            httpTest.RespondWith(ERROR_MESSAGE, 410);

            var service = new ServiceBaseTestObject(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

            // act
            // assert
            var result = await Assert.ThrowsAsync<MtgApiException>(
                () => service.CallWebServiceGetTestMethod(URL));
            Assert.EndsWith(ERROR_MESSAGE, result.Message);
            _mockRepository.VerifyAll();
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