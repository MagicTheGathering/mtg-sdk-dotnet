// <copyright file="SetTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using System;
    using System.Collections.Generic;
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Model;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Tests the <see cref="Set"/> class.
    /// </summary>
    [TestClass]
    public class SetTest
    {
        /// <summary>
        /// Tests the <see cref="Set.MapSet(SetDto)"/> method.
        /// </summary>
        [TestMethod]
        public void MapSetTest()
        {
            Set model;

            try
            {
                // Test exception is thrown.
                model = new Set(null);
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

            Assert.AreEqual(dto.Block, model.Block);
            Assert.AreEqual(dto.Border, model.Border);
            Assert.AreEqual(dto.Code, model.Code);
            Assert.AreEqual(dto.Expansion, model.Expansion);
            Assert.AreEqual(dto.GathererCode, model.GathererCode);
            Assert.AreEqual(dto.MagicCardsInfoCode, model.MagicCardsInfoCode);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.OldCode, model.OldCode);
            Assert.AreEqual(dto.OnlineOnly, model.OnlineOnly);
            Assert.AreEqual(dto.ReleaseDate, model.ReleaseDate);
        }

        /// <summary>
        /// Tests the CreateBoosterArray method.
        /// </summary>
        [TestMethod]
        public void CreateBoosterArrayTest()
        {
            var privateSet = new PrivateType(typeof(Set));

            var stringResult = privateSet.InvokeStatic("CreateBoosterArray", new object[] { new JValue("booster1") }) as string;
            Assert.AreEqual("booster1", stringResult);

            var booster = new JArray()
            {
                new JValue("booster2"),
                new JValue("booster3"),
                new JArray()
                {
                    new JValue("booster4"),
                    new JArray()
                    {
                        new JValue("booster5"),
                        new JValue("booster6")
                    }
                }
            };

            var arrayResult = privateSet.InvokeStatic("CreateBoosterArray", new object[] { booster }) as List<object>;
            Assert.AreEqual(3, arrayResult.Count);
            Assert.AreEqual(2, ((List<object>)arrayResult[2]).Count);
            Assert.AreEqual(2, ((List<object>)((List<object>)arrayResult[2])[1]).Count);
        }
    }
}