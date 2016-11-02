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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Tests the <see cref="CardService"/> class.
    /// </summary>
    [TestClass]
    public class CardServiceTest
    {
        /// <summary>
        /// Tests the <see cref="CardService.AllAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
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
                    Variations = new int[] { 1, 2, 3 },
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
                    Variations = new int[] { 1, 2, 3 },
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

            var service = new CardService(moqAdapter.Object);
            service = service.Where(x => x.Name, "name1");

            var result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.AllAsync();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.All"/> method.
        /// </summary>
        [TestMethod]
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
                    Variations = new int[] { 1, 2, 3 },
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
                    Variations = new int[] { 1, 2, 3 },
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

            var service = new CardService(moqAdapter.Object);
            service = service.Where(x => x.Name, "name1");

            var result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.All();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the constructors in the <see cref="CardService"/> class.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            CardService service;
            PrivateObject privateObject;

            service = new CardService();
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));

            service = new CardService(new MtgApiServiceAdapter());
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));

            service = new CardService(new MtgApiServiceAdapter(), ApiVersion.V1_0);
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));
        }

        /// <summary>
        /// Tests the <see cref="CardService.FindAsync(int)"/> methods.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
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
                Variations = new int[] { 1, 2, 3 },
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

            var service = new CardService(moqAdapter.Object);

            var result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.FindAsync(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.FindAsync(1);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);

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
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.FindAsync("123h4hfh4h6jgjk45jhbj");
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.Find(int)"/> method.
        /// </summary>
        [TestMethod]
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
                Variations = new int[] { 1, 2, 3 },
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

            var service = new CardService(moqAdapter.Object);

            var result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.Find(1);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.Find(1);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);

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
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.Find("123h4hfh4h6jgjk45jhbj");
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSubTypesAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
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

            var service = new CardService(moqAdapter.Object);

            var result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.GetCardSubTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.GetCardSubTypesAsync();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSubTypes"/> method.
        /// </summary>
        [TestMethod]
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

            var service = new CardService(moqAdapter.Object);

            var result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.GetCardSubTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.GetCardSubTypes();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSuperTypesAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
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

            var service = new CardService(moqAdapter.Object);

            var result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.GetCardSuperTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.GetCardSuperTypesAsync();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardSuperTypes"/> method.
        /// </summary>
        [TestMethod]
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

            var service = new CardService(moqAdapter.Object);

            var result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.GetCardSuperTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.GetCardSuperTypes();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardTypesAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
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

            var service = new CardService(moqAdapter.Object);

            var result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.GetCardTypesAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.GetCardTypesAsync();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.GetCardTypes"/> method.
        /// </summary>
        [TestMethod]
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

            var service = new CardService(moqAdapter.Object);

            var result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.GetCardTypes();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.GetCardTypes();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the MapCardsList method.
        /// </summary>
        [TestMethod]
        public void MapCardsListTest()
        {
            PrivateType privateObject = new PrivateType(typeof(CardService));

            try
            {
                // Test sending a null parameter.
                privateObject.InvokeStatic("MapCardsList", new object[] { null });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("cardListDto", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            // Test a null cards collection inside the RootCardListDto object.
            var cardListDto = new RootCardListDto();
            Assert.IsNull(privateObject.InvokeStatic("MapCardsList", new object[] { cardListDto }));

            cardListDto = new RootCardListDto()
            {
                Cards = new List<CardDto>()
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
                        Variations = new int[] { 1, 2, 3 },
                        Watermark = "watermark"
                    }
                }
            };

            var result = privateObject.InvokeStatic("MapCardsList", new object[] { cardListDto }) as List<Card>;
            Assert.AreEqual(1, result.Count);
        }

        /// <summary>
        /// Tests the <see cref="CardService.Where{U}(System.Linq.Expressions.Expression{Func{CardDto, U}}, string)"/> method.
        /// </summary>
        [TestMethod]
        public void WhereTest()
        {
            CardService service = new CardService();

            try
            {
                // Test sending a null parameter.
                service.Where<int>(null, 1);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("property", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                // Test sending a null parameter.
                service.Where(x => x.Name, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            service = service.Where(x => x.Name, "test")
                            .Where(x => x.Page, 1)
                            .Where(x => x.PageSize, 250);

            PrivateObject privateObject = new PrivateObject(service);
            var whereQuery = privateObject.GetFieldOrProperty("_whereQueries") as NameValueCollection;
            Assert.AreEqual(3, whereQuery.Count);
            Assert.AreEqual("name", whereQuery.AllKeys[0]);
            Assert.AreEqual("test", whereQuery["name"]);
        }
    }
}