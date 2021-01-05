using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Flurl.Util;
using Moq;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class CardServiceTests
    {
        private readonly Mock<IHeaderManager> _mockHeaderManager;
        private readonly Mock<IModelMapper> _mockModelMapper;
        private readonly Mock<IRateLimit> _mockRateLimit;
        private readonly MockRepository _mockRepository;

        public CardServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockHeaderManager = _mockRepository.Create<IHeaderManager>();
            _mockModelMapper = _mockRepository.Create<IModelMapper>();
            _mockRateLimit = _mockRepository.Create<IRateLimit>();
        }

        [Fact]
        public async Task AllAsync_NullCardListDto_Failure()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(null);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.AllAsync();

            // assert
            Assert.False(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AllAsync_NullCards_SuccessWithEmptyList()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new RootCardListDto());

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.AllAsync();

            // assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AllAsync_Success()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardListDto()
            {
                Cards = new List<CardDto> { cardDto },
            };

            _mockModelMapper.Setup(x => x.MapCard(It.IsAny<CardDto>())).Returns(new Card() { Name = CARD_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.AllAsync();

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task FindAsync_ById_Success()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardDto()
            {
                Card = cardDto,
            };

            _mockModelMapper.Setup(x => x.MapCard(It.IsAny<CardDto>())).Returns(new Card() { Name = CARD_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.FindAsync("12345");

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task FindAsync_ByMultiverseId_Success()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardDto()
            {
                Card = cardDto,
            };

            _mockModelMapper.Setup(x => x.MapCard(It.IsAny<CardDto>())).Returns(new Card() { Name = CARD_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.FindAsync(12345);

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task FindAsync_Exception_ReturnsFailure()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardDto()
            {
                Card = cardDto,
            };

            _mockModelMapper.Setup(x => x.MapCard(It.IsAny<CardDto>())).Throws(new Exception());

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.FindAsync("12345");

            // assert
            Assert.False(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCardSubTypesAsync_Exception_Failure()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetCardSubTypesAsync();

            // assert
            Assert.False(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCardSubTypesAsync_Success()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var rootCardSubType = new RootCardSubTypeDto()
            {
                SubTypes = new List<string> { "type1", "type2" },
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardSubType);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetCardSubTypesAsync();

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCardTypesAsync_Exception_Failure()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetCardTypesAsync();

            // assert
            Assert.False(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCardTypesAsync_Success()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var rootCardType = new RootCardTypeDto()
            {
                Types = new List<string> { "type1", "type2" },
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardType);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetCardTypesAsync();

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFormatsAsync_Exception_Failure()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetFormatsAsync();

            // assert
            Assert.False(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFormatsAsync_Success()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var rootCardFormat = new RootCardFormatsDto()
            {
                Formats = new List<string> { "format1", "format2" },
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardFormat);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetFormatsAsync();

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetGetCardSuperTypesAsync_Exception_Failure()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetCardSuperTypesAsync();

            // assert
            Assert.False(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetGetCardSuperTypesAsync_Success()
        {
            // arrange
            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var rootCardSuperSubType = new RootCardSuperTypeDto()
            {
                SuperTypes = new List<string> { "type1", "type2" },
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardSuperSubType);

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            var result = await service.GetCardSuperTypesAsync();

            // assert
            Assert.True(result.IsSuccess);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Reset_Success()
        {
            const string CARD_NAME = "cardname1";

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardListDto()
            {
                Cards = new List<CardDto> { cardDto },
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            _mockModelMapper.Setup(x => x.MapCard(It.IsAny<CardDto>())).Returns(new Card());

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            service.Where(x => x.Name, "test");

            // act
            service.Reset();

            // assert
            await service.AllAsync();
            httpTest.ShouldHaveCalled("https://api.magicthegathering.io/v1/cards");
        }

        [Fact]
        public async Task Where_AddsQueryParameters_Success()
        {
            const string NAME = "name1";
            const string LANGUAGE = "English";

            _mockRateLimit.Setup(x => x.IsTurnedOn).Returns(false);

            _mockHeaderManager.Setup(x => x.Update(It.IsAny<IReadOnlyNameValueList<string>>()));
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.TotalCount)).Returns(2000);
            _mockHeaderManager.Setup(x => x.Get<int>(ResponseHeader.PageSize)).Returns(1000);

            var rootCardList = new RootCardListDto()
            {
                Cards = new List<CardDto> { new CardDto() },
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            _mockModelMapper.Setup(x => x.MapCard(It.IsAny<CardDto>())).Returns(new Card());

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            service
                .Where(x => x.Name, NAME)
                .Where(x => x.Language, LANGUAGE);

            // assert
            await service.AllAsync();
            httpTest
                .ShouldHaveCalled("https://api.magicthegathering.io/v1/cards*")
                .WithQueryParams("name", "language");
        }

        [Fact]
        public void Where_DefaultValue_Throws()
        {
            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => service.Where(x => x.Name, null));
        }

        [Fact]
        public void Where_NullProperty_Throws()
        {
            const string NAME = "name1";

            var service = new CardService(
                _mockHeaderManager.Object,
                _mockModelMapper.Object,
                ApiVersion.V1,
                _mockRateLimit.Object);

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => service.Where(_ => null, NAME));
        }
    }
}