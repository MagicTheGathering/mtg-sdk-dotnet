using System;
using System.Threading.Tasks;
using Flurl;
using Moq;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class ServiceBaseTest
    {
        private readonly Mock<IHeaderManager> _mockHeaderManager;
        private readonly Mock<IModelMapper> _mockModelMapper;
        private readonly Mock<IMtgApiServiceAdapter> _mockMtgApiServiceAdapter;
        private readonly Mock<IRateLimit> _mockRateLimit;
        private readonly MockRepository _mockRepository;

        public ServiceBaseTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockMtgApiServiceAdapter = _mockRepository.Create<IMtgApiServiceAdapter>();
            _mockHeaderManager = _mockRepository.Create<IHeaderManager>();
            _mockModelMapper = _mockRepository.Create<IModelMapper>();
            _mockRateLimit = _mockRepository.Create<IRateLimit>();
        }

        [Fact]
        public async Task CallWebServiceGet_RateLimitOff_Success()
        {
            // arrange
            var URL = new Uri("http://fake/url");

            var card = new CardDto() { Id = "12345" };
            var rootCard = new RootCardDto()
            {
                Card = card,
            };

            _mockMtgApiServiceAdapter.Setup(x => x.WebGetAsync<RootCardDto>(URL)).ReturnsAsync(rootCard);

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            var service = new ServiceBaseTestObject(
                _mockMtgApiServiceAdapter.Object,
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

            var card = new CardDto() { Id = "12345" };
            var rootCard = new RootCardDto()
            {
                Card = card,
            };

            _mockMtgApiServiceAdapter.Setup(x => x.WebGetAsync<RootCardDto>(URL)).ReturnsAsync(rootCard);

            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.RatelimitLimit)).Returns(2000);

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(true);
            _mockRateLimit.Setup(x => x.Delay(2000)).ReturnsAsync(1);
            _mockRateLimit.Setup(x => x.AddApiCall());

            var service = new ServiceBaseTestObject(
                _mockMtgApiServiceAdapter.Object,
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
        public void Constructor_calls_ResetCurrentUrl()
        {
            // arrange
            // act
            var service = new ServiceBaseTestObject(
                _mockMtgApiServiceAdapter.Object,
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
                _mockMtgApiServiceAdapter.Object,
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
        public void GetPagingInfo_Sucess()
        {
            // arrange
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(100);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(10);

            var service = new ServiceBaseTestObject(
                _mockMtgApiServiceAdapter.Object,
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
                _mockMtgApiServiceAdapter.Object,
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                _mockRateLimit.Object);

            service.CurrentQueryUrlTestProp.AppendPathSegments("this", "is", "a", "test");

            // act
            service.ResetCurrentUrlTestMethod();

            // assert
            Assert.Equal(2, service.CurrentQueryUrlTestProp.PathSegments.Count);
        }
    }
}