// <copyright file="CardTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Card"/> class.
    /// </summary>

    public class CardTest
    {
        /// <summary>
        /// Tests the <see cref="Card.MapCard(Lib.Dto.CardDto)"/> method.
        /// </summary>
        [Fact]
        public void MapCardTest()
        {
            Card model;

            try
            {
                // Test exception is thrown.
                model = new Card(null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("item", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
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

            Assert.Equal(dto.Artist, model.Artist);
            Assert.Equal(dto.Border, model.Border);
            Assert.Equal(dto.Cmc, model.Cmc);
            Assert.True(dto.Colors.SequenceEqual(model.Colors));
            Assert.Equal(dto.Flavor, model.Flavor);
            Assert.Equal(dto.ForeignNames.Count(), model.ForeignNames.Count);
            Assert.Equal(dto.Hand, model.Hand);
            Assert.Equal(dto.Id, model.Id);
            Assert.Equal(dto.ImageUrl, model.ImageUrl);
            Assert.Equal(dto.Layout, model.Layout);
            Assert.Equal(dto.Legalities.Count(), model.Legalities.Count);
            Assert.Equal(dto.Life, model.Life);
            Assert.Equal(dto.Loyalty, model.Loyalty);
            Assert.Equal(dto.ManaCost, model.ManaCost);
            Assert.Equal(dto.MultiverseId, model.MultiverseId);
            Assert.Equal(dto.Name, model.Name);
            Assert.True(dto.Name.SequenceEqual(model.Name));
            Assert.Equal(dto.Number, model.Number);
            Assert.Equal(dto.OriginalText, model.OriginalText);
            Assert.Equal(dto.OriginalType, model.OriginalType);
            Assert.Equal(dto.Power, model.Power);
            Assert.True(dto.Printings.SequenceEqual(model.Printings));
            Assert.Equal(dto.Rarity, model.Rarity);
            Assert.Equal(dto.ReleaseDate, model.ReleaseDate);
            Assert.Equal(dto.Number, model.Number);
            Assert.Equal(dto.Reserved, model.Reserved);
            Assert.Equal(dto.Rulings.Count(), model.Rulings.Count);
            Assert.Equal(dto.Set, model.Set);
            Assert.Equal(dto.SetName, model.SetName);
            Assert.Equal(dto.Source, model.Source);
            Assert.Equal(dto.Starter, model.Starter);
            Assert.Equal(dto.Reserved, model.Reserved);
            Assert.True(dto.SubTypes.SequenceEqual(model.SubTypes));
            Assert.True(dto.SuperTypes.SequenceEqual(model.SuperTypes));
            Assert.Equal(dto.Text, model.Text);
            Assert.Equal(dto.Timeshifted, model.Timeshifted);
            Assert.Equal(dto.Toughness, model.Toughness);
            Assert.Equal(dto.Type, model.Type);
            Assert.True(dto.Types.SequenceEqual(model.Types));
            Assert.True(dto.Variations.SequenceEqual(model.Variations));
            Assert.Equal(dto.Watermark, model.Watermark);

            dto = new CardDto()
            {
                ForeignNames = null,
                Legalities = null,
                Rulings = null,
            };

            model = new Card(dto);

            Assert.Null(dto.ForeignNames);
            Assert.Null(dto.Legalities);
            Assert.Null(dto.Rulings);
        }
    }
}