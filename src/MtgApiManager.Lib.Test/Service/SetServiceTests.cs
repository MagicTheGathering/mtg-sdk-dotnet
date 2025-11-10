using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Flurl.Util;
using NSubstitute;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Dto.Set;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class SetServiceTests
    {
        private readonly IHeaderManager _headerManager;
        private readonly IModelMapper _modelMapper;
        private readonly IRateLimit _rateLimit;

        public SetServiceTests()
        {
            _headerManager = Substitute.For<IHeaderManager>();
            _modelMapper = Substitute.For<IModelMapper>();
            _rateLimit = Substitute.For<IRateLimit>();
        }

        [Fact]
        public async Task AllAsync_NullSetListDto_Failure()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(null);

            var service = new SetService(
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
        public async Task AllAsync_NullSets_SuccessWithEmptyList()
        {
            // arrange
            _rateLimit.IsTurnedOn.Returns(false);
            _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new RootSetListDto());

            var service = new SetService(
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
            const string SET_NAME = "setname1";
            _rateLimit.IsTurnedOn.Returns(false);
            _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var setDto = new SetDto() { Name = SET_NAME };
            var rootSetList = new RootSetListDto()
            {
                Sets = [setDto],
            };

            _modelMapper.MapSet(Arg.Any<SetDto>()).Returns(new Set() { Name = SET_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootSetList);

            var service = new SetService(
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
        public async Task FindAsync_Exception_ReturnsFailure()
        {
            // arrange
            const string SET_NAME = "setname1";
            _rateLimit.IsTurnedOn.Returns(false);

            var setDto = new SetDto() { Name = SET_NAME };
            var rootSetList = new RootSetDto()
            {
                Set = setDto,
            };

            _modelMapper.MapSet(Arg.Any<SetDto>()).Returns(x => { throw new Exception(); });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootSetList);

            var service = new SetService(
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
        public async Task FindAsync_Success()
        {
            // arrange
            const string SET_NAME = "setname1";
            _rateLimit.IsTurnedOn.Returns(false);
            _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var setDto = new SetDto() { Name = SET_NAME };
            var rootSetList = new RootSetDto()
            {
                Set = setDto,
            };

            _modelMapper.MapSet(Arg.Any<SetDto>()).Returns(new Set() { Name = SET_NAME });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootSetList);

            var service = new SetService(
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
        public async Task GenerateBoosterAsync_Exception_ReturnsFailure()
        {
            // arrange
            const string CARD_NAME = "cardname1";
            _rateLimit.IsTurnedOn.Returns(false);

            var cardDto = new CardDto() { Name = CARD_NAME };
            var rootCardList = new RootCardListDto()
            {
                Cards = [cardDto],
            };

            _modelMapper.MapCard(Arg.Any<CardDto>()).Returns(x => { throw new Exception(); });

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootCardList);

            var service = new SetService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GenerateBoosterAsync("12345");

            // assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GenerateBoosterAsync_Success()
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

            var service = new SetService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            var result = await service.GenerateBoosterAsync("12345");

            // assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Reset_Success()
        {
            _rateLimit.IsTurnedOn.Returns(false);
            _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootSetList = new RootSetDto()
            {
                Set = new SetDto(),
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootSetList);

            _modelMapper.MapSet(Arg.Any<SetDto>()).Returns(new Set());

            var service = new SetService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            service.Where(x => x.Name, "test");

            // act
            service.Reset();

            // assert
            await service.AllAsync();
            httpTest.ShouldHaveCalled("https://api.magicthegathering.io/v1/sets");
        }

        [Fact]
        public async Task Where_AddsQueryParameters_Success()
        {
            const string NAME = "name1";
            const string BLOCK = "12345";

            _rateLimit.IsTurnedOn.Returns(false);
            _headerManager.Get<int>(ResponseHeader.TotalCount).Returns(2000);
            _headerManager.Get<int>(ResponseHeader.PageSize).Returns(1000);

            var rootSetList = new RootSetListDto()
            {
                Sets = [new SetDto()],
            };

            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(rootSetList);

            _modelMapper.MapSet(Arg.Any<SetDto>()).Returns(new Set());

            var service = new SetService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            service
                .Where(x => x.Name, NAME)
                .Where(x => x.Block, BLOCK);

            // assert
            await service.AllAsync();
            httpTest
                .ShouldHaveCalled("https://api.magicthegathering.io/v1/sets*")
                .WithQueryParams("name", "block");
        }

        [Fact]
        public void Where_DefaultValue_Throws()
        {
            var service = new SetService(
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

            var service = new SetService(
                _headerManager,
                _modelMapper,
                ApiVersion.V1,
                _rateLimit);

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => service.Where(_ => null, NAME));
        }
    }
}