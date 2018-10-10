// <copyright file="SetTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Set"/> class.
    /// </summary>

    public class SetTest
    {
        /// <summary>
        /// Tests the <see cref="Set.MapSet(SetDto)"/> method.
        /// </summary>
        [Fact]
        public void MapSetTest()
        {
            Set model;

            try
            {
                // Test exception is thrown.
                model = new Set(null);
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

            var dto = new SetDto()
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
                MagicCardsInfoCode = "magicCardsInfoCode",
                Name = "name1",
                OldCode = "oldCode1",
                OnlineOnly = true,
                ReleaseDate = "2016, 1, 1"
            };

            model = new Set(dto);

            Assert.Equal(dto.Block, model.Block);
            Assert.Equal(dto.Border, model.Border);
            Assert.Equal(dto.Code, model.Code);
            Assert.Equal(dto.Expansion, model.Expansion);
            Assert.Equal(dto.GathererCode, model.GathererCode);
            Assert.Equal(dto.MagicCardsInfoCode, model.MagicCardsInfoCode);
            Assert.Equal(dto.Name, model.Name);
            Assert.Equal(dto.OldCode, model.OldCode);
            Assert.Equal(dto.OnlineOnly, model.OnlineOnly);
            Assert.Equal(dto.ReleaseDate, model.ReleaseDate);
        }
    }
}