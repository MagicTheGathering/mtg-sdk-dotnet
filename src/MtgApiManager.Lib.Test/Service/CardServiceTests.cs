using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Flurl.Util;
using NSubstitute;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class CardServiceTests
    {
        private readonly IHeaderManager _headerManager;
        private readonly IModelMapper _modelMapper;
        private readonly IRateLimit _rateLimit;

        public CardServiceTests()
        {
            _headerManager = Substitute.For<IHeaderManager>();
            _modelMapper = Substitute.For<IModelMapper>();
            _rateLimit = Substitute.For<IRateLimit>();
        }

        [Fact]
        public async Task AllAsync_NullCardListDto_Failure()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

            
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(null);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.AllAsync();

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task AllAsync_NullCards_SuccessWithEmptyList()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new RootCardListDto());

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.AllAsync();

            // assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task AllAsync_Success()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardListDto()
            {
                Cards = [cardDto],
            };

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card() { Name = CARD_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.AllAsync();

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AllAsync_NoMock_EqualPageSize()
        {
            const int PAGE_SIZE = 50;
            var serviceProvider = new MtgServiceProvider();
            var service = serviceProvider.GetCardService();

            var result = await service
                .Where(x => x.Page, 1)
                .Where(x => x.PageSize, PAGE_SIZE)
                .AllAsync();

            Assert.Equal(PAGE_SIZE, result.PagingInfo.PageSize);
        }

        [Fact]
        public async Task FindAsync_ById_Success()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardDto()
            {
                Card = cardDto,
            };

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card() { Name = CARD_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.FindAsync("12345");

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task FindAsync_ByMultiverseId_Success()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardDto()
            {
                Card = cardDto,
            };

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card() { Name = CARD_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.FindAsync(12345);

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task FindAsync_Exception_ReturnsFailure()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _rateLimit.IsTurnedOn.Returns(false);

            
            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardDto()
            {
                Card = cardDto,
            };

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(x => { throw new Exception(); });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.FindAsync("12345");

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetCardSubTypesAsync_Exception_Failure()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetCardSubTypesAsync();

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetCardSubTypesAsync_Success()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardSubType = new RootCardSubTypeDto()
            {
                SubTypes = ["type1", "type2"],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardSubType);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetCardSubTypesAsync();

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetCardTypesAsync_Exception_Failure()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetCardTypesAsync();

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetCardTypesAsync_Success()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardType = new RootCardTypeDto()
            {
                Types = ["type1", "type2"],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardType);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetCardTypesAsync();

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetFormatsAsync_Exception_Failure()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetFormatsAsync();

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetFormatsAsync_Success()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardFormat = new RootCardFormatsDto()
            {
                Formats = ["format1", "format2"],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardFormat);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetFormatsAsync();

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetGetCardSuperTypesAsync_Exception_Failure()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetCardSuperTypesAsync();

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetGetCardSuperTypesAsync_Success()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardSuperSubType = new RootCardSuperTypeDto()
            {
                SuperTypes = ["type1", "type2"],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardSuperSubType);

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GetCardSuperTypesAsync();

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Reset_Success()
        {
            const string CARD_NAME = "cardname1";

            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardListDto()
            {
                Cards = [cardDto],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card());

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

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

            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardList = new RootCardListDto()
            {
                Cards = [new CardDto()],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card());

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

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
        public async Task Where_AddsIdParameter_Success()
        {
            const string MULTIPLE_IDS = 
                "896a92b1-ed29-5daf-bf89-2502224f8f11|516dd7a4-fad8-5eed-bcdb-c05088762303";
            
            const string ID_PARAM = "id";

            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardList = new RootCardListDto()
            {
                Cards = [new CardDto()],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card());

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            await service
                .Where(x => x.Id, MULTIPLE_IDS)
                .AllAsync();

            // assert
            httpTest
                .ShouldHaveCalled("https://api.magicthegathering.io/v1/cards*")
                .WithQueryParams(ID_PARAM);
        }

        [Fact]
        public async Task Where_AddMultiverseIdParameter_Success()
        {
            const string MULTIPLE_MULTIS = "3|4";
            const string MULTI_PARAM = "multiverseid";

            _rateLimit.IsTurnedOn.Returns(false);

                        _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootCardList = new RootCardListDto()
            {
                Cards = [new CardDto()],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(new Card());

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            await service
                .Where(x => x.MultiverseId, MULTIPLE_MULTIS)
                .AllAsync();

            // assert
            httpTest
                .ShouldHaveCalled("https://api.magicthegathering.io/v1/cards*")
                .WithQueryParams(MULTI_PARAM);
        }

        [Fact]
        public void Where_DefaultValue_Throws()
        {
            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => service.Where(x => x.Name, null));
        }

        [Fact]
        public void Where_NullProperty_Throws()
        {
            const string NAME = "name1";

            var service = new CardService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => service.Where(_ => null, NAME));
        }

        [Fact]
        public async Task Where_AddMultiverseIdNoMock_Success()
        {
            const string MULTIPLE_MULTIS = "3|4|5|6";

            var serviceProvider = new MtgServiceProvider();
            var service = serviceProvider.GetCardService();

            var result = await service
                .Where(x => x.MultiverseId, MULTIPLE_MULTIS)
                .AllAsync();

            Assert.True(result.IsSuccess);
            Assert.Equal(4, result.Value.Count);
        }
    }
}