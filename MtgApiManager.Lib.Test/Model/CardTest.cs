// <copyright file="CardTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using System;
    using System.Linq;
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Model;

    /// <summary>
    /// Tests the <see cref="Card"/> class.
    /// </summary>
    [TestClass]
    public class CardTest
    {
        /// <summary>
        /// Tests the <see cref="Card.MapCard(Lib.Dto.CardDto)"/> method.
        /// </summary>
        [TestMethod]
        public void MapCardTest()
        {
            Card model;

            try
            {
                // Test exception is thrown.
                model = new Card(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("item", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            var dto = new CardDto()
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
                Rulings = new RulingDto[] { new RulingDto() { Date = "(2010, 2, 2", Text = "text2" } },
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

            model = new Card(dto);

            Assert.AreEqual(dto.Artist, model.Artist);
            Assert.AreEqual(dto.Border, model.Border);
            Assert.AreEqual(dto.Cmc, model.Cmc);
            Assert.IsTrue(dto.Colors.SequenceEqual(model.Colors));
            Assert.AreEqual(dto.Flavor, model.Flavor);
            Assert.AreEqual(dto.ForeignNames.Count(), model.ForeignNames.Count);
            Assert.AreEqual(dto.Hand, model.Hand);
            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.ImageUrl, model.ImageUrl);
            Assert.AreEqual(dto.Layout, model.Layout);
            Assert.AreEqual(dto.Legalities.Count(), model.Legalities.Count);
            Assert.AreEqual(dto.Life, model.Life);
            Assert.AreEqual(dto.Loyalty, model.Loyalty);
            Assert.AreEqual(dto.ManaCost, model.ManaCost);
            Assert.AreEqual(dto.MultiverseId, model.MultiverseId);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.IsTrue(dto.Name.SequenceEqual(model.Name));
            Assert.AreEqual(dto.Number, model.Number);
            Assert.AreEqual(dto.OriginalText, model.OriginalText);
            Assert.AreEqual(dto.OriginalType, model.OriginalType);
            Assert.AreEqual(dto.Power, model.Power);
            Assert.IsTrue(dto.Printings.SequenceEqual(model.Printings));
            Assert.AreEqual(dto.Rarity, model.Rarity);
            Assert.AreEqual(dto.ReleaseDate, model.ReleaseDate);
            Assert.AreEqual(dto.Number, model.Number);
            Assert.AreEqual(dto.Reserved, model.Reserved);
            Assert.AreEqual(dto.Rulings.Count(), model.Rulings.Count);
            Assert.AreEqual(dto.Set, model.Set);
            Assert.AreEqual(dto.SetName, model.SetName);
            Assert.AreEqual(dto.Source, model.Source);
            Assert.AreEqual(dto.Starter, model.Starter);
            Assert.AreEqual(dto.Reserved, model.Reserved);
            Assert.IsTrue(dto.SubTypes.SequenceEqual(model.SubTypes));
            Assert.IsTrue(dto.SuperTypes.SequenceEqual(model.SuperTypes));
            Assert.AreEqual(dto.Text, model.Text);
            Assert.AreEqual(dto.Timeshifted, model.Timeshifted);
            Assert.AreEqual(dto.Toughness, model.Toughness);
            Assert.AreEqual(dto.Type, model.Type);
            Assert.IsTrue(dto.Types.SequenceEqual(model.Types));
            Assert.IsTrue(dto.Variations.SequenceEqual(model.Variations));
            Assert.AreEqual(dto.Watermark, model.Watermark);

            dto = new CardDto()
            {
                ForeignNames = null,
                Legalities = null,
                Rulings = null,
            };

            model = new Card(dto);

            Assert.IsNull(dto.ForeignNames);
            Assert.IsNull(dto.Legalities);
            Assert.IsNull(dto.Rulings);
        }
    }
}