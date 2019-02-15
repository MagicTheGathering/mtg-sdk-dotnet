// <copyright file="SetServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using Lib.Core;
    using Lib.Core.Exceptions;
    using Lib.Dto;
    using Lib.Dto.Set;
    using Lib.Model;
    using Lib.Service;
    using Moq;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="SetService"/> class.
    /// </summary>

    public class SetServiceTest
    {
        /// <summary>
        /// Tests the <see cref="SetService.AllAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task AllAsyncTest()
        {
            var sets = new List<SetDto>()
            {
                new SetDto()
                {
                    Block = "block1",
                    Booster = new object[2]
                    {
                        new JValue("booster1"),
                        new JArray()
                        {
                            new JValue("booster2"),
                            new JValue("booster3"),
                            new JArray()
                            {
                                new JValue("booster4"),
                                new JValue("booster5")
                            }
                        }
                    },
                    Border = "border1",
                    Code = "code1",
                    Expansion = "expansion1",
                    GathererCode = "gathererCode1",
                    MagicCardsInfoCode = "magicCardsInfoCode1",
                    Name = "name1",
                    OldCode = "oldCode1",
                    OnlineOnly = true,
                    ReleaseDate = "2016, 1, 1"
                },
                new SetDto()
                {
                    Block = "block2",
                    Booster = new object[2]
                    {
                        new JValue("booster1"),
                        new JArray()
                        {
                            new JValue("booster2"),
                            new JValue("booster3"),
                            new JArray()
                            {
                                new JValue("booster4"),
                                new JValue("booster5")
                            }
                        }
                    },
                    Border = "border2",
                    Code = "code2",
                    Expansion = "expansion2",
                    GathererCode = "gathererCode2",
                    MagicCardsInfoCode = "magicCardsInfoCode2",
                    Name = "name2",
                    OldCode = "oldCode2",
                    OnlineOnly = true,
                    ReleaseDate = "2016, 2, 2"
                }
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetListDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetListDto() { Sets = sets });

            var service = new SetService(moqAdapter.Object, ApiVersion.V1_0, false);
            service = service.Where(x => x.Name, "name1");

            var result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.AllAsync();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.AllAsync();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.All"/> method.
        /// </summary>
        [Fact]
        public void AllTest()
        {
            var sets = new List<SetDto>()
            {
                new SetDto()
                {
                    Block = "block1",
                    Booster = new object[2]
                    {
                        new JValue("booster1"),
                        new JArray()
                        {
                            new JValue("booster2"),
                            new JValue("booster3"),
                            new JArray()
                            {
                                new JValue("booster4"),
                                new JValue("booster5")
                            }
                        }
                    },
                    Border = "border1",
                    Code = "code1",
                    Expansion = "expansion1",
                    GathererCode = "gathererCode1",
                    MagicCardsInfoCode = "magicCardsInfoCode1",
                    Name = "name1",
                    OldCode = "oldCode1",
                    OnlineOnly = true,
                    ReleaseDate = "2016, 1, 1"
                },
                new SetDto()
                {
                    Block = "block2",
                    Booster = new object[2]
                    {
                        new JValue("booster1"),
                        new JArray()
                        {
                            new JValue("booster1"),
                            new JValue("booster2")
                        }
                    },
                    Border = "border2",
                    Code = "code2",
                    Expansion = "expansion2",
                    GathererCode = "gathererCode2",
                    MagicCardsInfoCode = "magicCardsInfoCode2",
                    Name = "name2",
                    OldCode = "oldCode2",
                    OnlineOnly = true,
                    ReleaseDate = "2016, 2, 2"
                }
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetListDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetListDto() { Sets = sets });

            var service = new SetService(moqAdapter.Object, ApiVersion.V1_0, false);
            service = service.Where(x => x.Name, "name1");

            var result = service.All();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.All();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.All();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.All();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.All();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.All();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.All();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.All();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="SetService.FindAsync(int)"/> methods.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task FindAsyncTest()
        {
            var setDto = new SetDto()
            {
                Block = "block1",
                Booster = new object[2]
                {
                    new JValue("booster1"),
                    new JArray()
                    {
                        new JValue("booster2"),
                        new JValue("booster3"),
                        new JArray()
                        {
                            new JValue("booster4"),
                            new JValue("booster5")
                        }
                    }
                },
                Border = "border1",
                Code = "code1",
                Expansion = "expansion1",
                GathererCode = "gathererCode1",
                MagicCardsInfoCode = "magicCardsInfoCode1",
                Name = "name1",
                OldCode = "oldCode1",
                OnlineOnly = true,
                ReleaseDate = "2016, 1, 1"
            };

            // Test the FindAsync method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetDto() { Set = setDto });

            var service = new SetService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.FindAsync("code1");
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="SetService.Find(int)"/> method.
        /// </summary>
        [Fact]
        public void FindTest()
        {
            var setDto = new SetDto()
            {
                Block = "block1",
                Booster = new object[2]
                {
                    new JValue("booster1"),
                    new JArray()
                    {
                        new JValue("booster2"),
                        new JValue("booster3"),
                        new JArray()
                        {
                            new JValue("booster4"),
                            new JValue("booster5")
                        }
                    }
                },
                Border = "border1",
                Code = "code1",
                Expansion = "expansion1",
                GathererCode = "gathererCode1",
                MagicCardsInfoCode = "magicCardsInfoCode1",
                Name = "name1",
                OldCode = "oldCode1",
                OnlineOnly = true,
                ReleaseDate = "2016, 1, 1"
            };

            // Test the FindAsync method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetDto() { Set = setDto });

            var service = new SetService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.Find("code1");
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.Find("code1");
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="SetService.GenerateBoosterAsync(string)"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task GenerateBoosterAsyncTest()
        {
            var cards = new List<CardDto>()
            {
                new CardDto()
                {
                    Artist = "artist1",
                    Border = "border1",
                    Cmc = 111,
                    Colors = new string[] { "blue", "pink" },
                    Flavor = "flavor1",
                    ForeignNames = new ForeignNameDto[] { new ForeignNameDto() { Language = "english", MultiverseId = 222, Name = "name2" } },
                    Hand = 222,
                    Id = "12345",
                    ImageUrl = new System.Uri("http://fake/url"),
                    Layout = "layout1",
                    Legalities = new LegalityDto[] { new LegalityDto() { Format = "format2", LegalityName = "legality name 2" } },
                    Life = 333,
                    Loyalty = "loyalty",
                    ManaCost = "500",
                    MultiverseId = 444,
                    Name = "name1",
                    Names = new string[] { "name2", "name3" },
                    Number = "600",
                    OriginalText = "original text",
                    OriginalType = "original type",
                    Power = "9000",
                    Printings = new string[] { "printing1", "printing2" },
                    Rarity = "rare",
                    ReleaseDate = "2010, 1, 1",
                    Reserved = true,
                    Rulings = new RulingDto[] { new RulingDto() { Date = "2010, 2, 2", Text = "text2" } },
                    Set = "set1",
                    SetName = "set name 1",
                    Source = "source",
                    Starter = true,
                    SubTypes = new string[] { "subtype1", "subtype2" },
                    SuperTypes = new string[] { "supertype1", "supertype2" },
                    Text = "text3",
                    Timeshifted = false,
                    Toughness = "tough",
                    Type = "type2",
                    Types = new string[] { "type1", "type2" },
                    Variations = new [] { Guid.Empty.ToString() },
                    Watermark = "watermark"
                },
                new CardDto()
                {
                    Artist = "artist1",
                    Border = "border1",
                    Cmc = 111,
                    Colors = new string[] { "blue", "pink" },
                    Flavor = "flavor1",
                    ForeignNames = new ForeignNameDto[] { new ForeignNameDto() { Language = "english", MultiverseId = 222, Name = "name2" } },
                    Hand = 222,
                    Id = "12345",
                    ImageUrl = new System.Uri("http://fake/url"),
                    Layout = "layout1",
                    Legalities = new LegalityDto[] { new LegalityDto() { Format = "format2", LegalityName = "legality name 2" } },
                    Life = 333,
                    Loyalty = "loyalty",
                    ManaCost = "500",
                    MultiverseId = 444,
                    Name = "name1",
                    Names = new string[] { "name2", "name3" },
                    Number = "600",
                    OriginalText = "original text",
                    OriginalType = "original type",
                    Power = "9000",
                    Printings = new string[] { "printing1", "printing2" },
                    Rarity = "rare",
                    ReleaseDate = "2010, 1, 1",
                    Reserved = true,
                    Rulings = new RulingDto[] { new RulingDto() { Date = "2010, 2, 2", Text = "text2" } },
                    Set = "set1",
                    SetName = "set name 1",
                    Source = "source",
                    Starter = true,
                    SubTypes = new string[] { "subtype1", "subtype2" },
                    SuperTypes = new string[] { "supertype1", "supertype2" },
                    Text = "text3",
                    Timeshifted = false,
                    Toughness = "tough",
                    Type = "type2",
                    Types = new string[] { "type1", "type2" },
                    Variations = new [] { Guid.Empty.ToString() },
                    Watermark = "watermark"
                }
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardListDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardListDto() { Cards = cards });

            var service = new SetService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.GenerateBoosterAsync("ktk");
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="SetService.GenerateBooster(string)"/> method.
        /// </summary>
        [Fact]
        public void GenerateBoosterTest()
        {
            var cards = new List<CardDto>()
            {
                new CardDto()
                {
                    Artist = "artist1",
                    Border = "border1",
                    Cmc = 111,
                    Colors = new string[] { "blue", "pink" },
                    Flavor = "flavor1",
                    ForeignNames = new ForeignNameDto[] { new ForeignNameDto() { Language = "english", MultiverseId = 222, Name = "name2" } },
                    Hand = 222,
                    Id = "12345",
                    ImageUrl = new System.Uri("http://fake/url"),
                    Layout = "layout1",
                    Legalities = new LegalityDto[] { new LegalityDto() { Format = "format2", LegalityName = "legality name 2" } },
                    Life = 333,
                    Loyalty = "loyalty",
                    ManaCost = "500",
                    MultiverseId = 444,
                    Name = "name1",
                    Names = new string[] { "name2", "name3" },
                    Number = "600",
                    OriginalText = "original text",
                    OriginalType = "original type",
                    Power = "9000",
                    Printings = new string[] { "printing1", "printing2" },
                    Rarity = "rare",
                    ReleaseDate = "2010, 1, 1",
                    Reserved = true,
                    Rulings = new RulingDto[] { new RulingDto() { Date = "2010, 2, 2", Text = "text2" } },
                    Set = "set1",
                    SetName = "set name 1",
                    Source = "source",
                    Starter = true,
                    SubTypes = new string[] { "subtype1", "subtype2" },
                    SuperTypes = new string[] { "supertype1", "supertype2" },
                    Text = "text3",
                    Timeshifted = false,
                    Toughness = "tough",
                    Type = "type2",
                    Types = new string[] { "type1", "type2" },
                    Variations = new [] { Guid.Empty.ToString() },
                    Watermark = "watermark"
                },
                new CardDto()
                {
                    Artist = "artist1",
                    Border = "border1",
                    Cmc = 111,
                    Colors = new string[] { "blue", "pink" },
                    Flavor = "flavor1",
                    ForeignNames = new ForeignNameDto[] { new ForeignNameDto() { Language = "english", MultiverseId = 222, Name = "name2" } },
                    Hand = 222,
                    Id = "12345",
                    ImageUrl = new System.Uri("http://fake/url"),
                    Layout = "layout1",
                    Legalities = new LegalityDto[] { new LegalityDto() { Format = "format2", LegalityName = "legality name 2" } },
                    Life = 333,
                    Loyalty = "loyalty",
                    ManaCost = "500",
                    MultiverseId = 444,
                    Name = "name1",
                    Names = new string[] { "name2", "name3" },
                    Number = "600",
                    OriginalText = "original text",
                    OriginalType = "original type",
                    Power = "9000",
                    Printings = new string[] { "printing1", "printing2" },
                    Rarity = "rare",
                    ReleaseDate = "2010, 1, 1",
                    Reserved = true,
                    Rulings = new RulingDto[] { new RulingDto() { Date = "2010, 2, 2", Text = "text2" } },
                    Set = "set1",
                    SetName = "set name 1",
                    Source = "source",
                    Starter = true,
                    SubTypes = new string[] { "subtype1", "subtype2" },
                    SuperTypes = new string[] { "supertype1", "supertype2" },
                    Text = "text3",
                    Timeshifted = false,
                    Toughness = "tough",
                    Type = "type2",
                    Types = new string[] { "type1", "type2" },
                    Variations = new [] { Guid.Empty.ToString() },
                    Watermark = "watermark"
                }
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardListDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardListDto() { Cards = cards });

            var service = new SetService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.GenerateBooster("ktk");
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.GenerateBooster("ktk");
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="SetService.Where{U}(System.Linq.Expressions.Expression{Func{SetQueryParameter, U}}, string)"/>> method.
        /// </summary>
        [Fact]
        public void WhereTest()
        {
            SetService service = new SetService();

            try
            {
                // Test sending a null parameter.
                service.Where<int>(null, 1);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("property", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            try
            {
                // Test sending a null parameter.
                service.Where(x => x.Name, null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("value", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}