// <copyright file="CardServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading.Tasks;
    using Lib.Core;
    using Lib.Core.Exceptions;
    using Lib.Dto;
    using Lib.Model;
    using Lib.Service;

    using Moq;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="CardService"/> class.
    /// </summary>

    public class CardServiceTest
    {
        /// <summary>
        /// Tests the <see cref="CardService.AllAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task AllAsyncTest()
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
                    ImageUrl = "http://fake/url",
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
                    Variations = new string[] { Guid.Empty.ToString() },
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
                    ImageUrl = "http://fake/url",
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
                    Variations = new string[] { Guid.Empty.ToString() },
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

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);
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
                    ImageUrl = "http://fake/url",
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
                    Variations = new string[] { Guid.Empty.ToString() },
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
                    ImageUrl = "http://fake/url",
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
                    Variations = new string[] { Guid.Empty.ToString() },
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

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);
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
        /// Tests the <see cref="CardService.FindAsync(int)"/> methods.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task FindAsyncTest()
        {
            var cardDto = new CardDto()
            {
                Artist = "artist1",
                Border = "border1",
                Cmc = 111,
                Colors = new string[] { "blue", "pink" },
                Flavor = "flavor1",
                ForeignNames = new ForeignNameDto[] { new ForeignNameDto() { Language = "english", MultiverseId = 222, Name = "name2" } },
                Hand = 222,
                Id = "12345",
                ImageUrl = "http://fake/url",
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
                Variations = new string[] { Guid.Empty.ToString() },
                Watermark = "watermark"
            };

            // Test the FindAsync method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardDto() { Card = cardDto });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.FindAsync(1);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);

            // Test the FindAsync method.
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardDto() { Card = cardDto });

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.Find(int)"/> method.
        /// </summary>
        [Fact]
        public void FindTest()
        {
            var cardDto = new CardDto()
            {
                Artist = "artist1",
                Border = "border1",
                Cmc = 111,
                Colors = new string[] { "blue", "pink" },
                Flavor = "flavor1",
                ForeignNames = new ForeignNameDto[] { new ForeignNameDto() { Language = "english", MultiverseId = 222, Name = "name2" } },
                Hand = 222,
                Id = "12345",
                ImageUrl = "http://fake/url",
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
                Variations = new string[] { Guid.Empty.ToString() },
                Watermark = "watermark"
            };

            // Test the Find by multi verse identifier method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardDto() { Card = cardDto });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.Find(1);
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.Find(1);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);

            // Test the Find by identifier method.
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardDto() { Card = cardDto });

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSubTypesAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task GetCardSubTypesAsyncTest()
        {
            var cardSubTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardSubTypeDto>(new Uri("https://api.magicthegathering.io/v1/subtypes")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardSubTypeDto() { SubTypes = cardSubTypes });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.GetCardSubTypesAsync();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSubTypes"/> method.
        /// </summary>
        [Fact]
        public void GetCardSubTypesTest()
        {
            var cardSubTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardSubTypeDto>(new Uri("https://api.magicthegathering.io/v1/subtypes")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardSubTypeDto() { SubTypes = cardSubTypes });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.GetCardSubTypes();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSuperTypesAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task GetCardSuperTypesAsyncTest()
        {
            var cardSuperTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardSuperTypeDto>(new Uri("https://api.magicthegathering.io/v1/supertypes")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardSuperTypeDto() { SuperTypes = cardSuperTypes });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.GetCardSuperTypesAsync();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSuperTypes"/> method.
        /// </summary>
        [Fact]
        public void GetCardSuperTypesTest()
        {
            var cardSuperTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardSuperTypeDto>(new Uri("https://api.magicthegathering.io/v1/supertypes")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardSuperTypeDto() { SuperTypes = cardSuperTypes });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.GetCardSuperTypes();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardTypesAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [Fact]
        public async Task GetCardTypesAsyncTest()
        {
            var cardTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardTypeDto>(new Uri("https://api.magicthegathering.io/v1/types")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardTypeDto() { Types = cardTypes });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = await service.GetCardTypesAsync();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardTypes"/> method.
        /// </summary>
        [Fact]
        public void GetCardTypesTest()
        {
            var cardTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardTypeDto>(new Uri("https://api.magicthegathering.io/v1/types")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardTypeDto() { Types = cardTypes });

            var service = new CardService(moqAdapter.Object, ApiVersion.V1_0, false);

            var result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("Value cannot be null.", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("bad request", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("forbidden", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("server error", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("not found", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.Equal("unavailable", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.False(result.IsSuccess);
            Assert.IsType<Exception>(result.Exception);

            result = service.GetCardTypes();
            Assert.True(result.IsSuccess);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.Where{U}(System.Linq.Expressions.Expression{Func{CardDto, U}}, string)"/> method.
        /// </summary>
        [Fact]
        public void WhereTest()
        {
            CardService service = new CardService();

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

            service = service.Where(x => x.Name, "test")
                            .Where(x => x.Page, 1)
                            .Where(x => x.PageSize, 250);
        }
    }
}