﻿// <copyright file="BoosterCustomConverterTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Dto
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Lib.Dto.Set;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Dto;
    using Newtonsoft.Json;

    /// <summary>
    /// Tests the data transfer objects.
    /// </summary>
    [TestClass]
    public class BoosterCustomConverterTest
    {
        /// <summary>
        /// Tests the <see cref="BoosterCustomConverter.CanConvert"/> method.
        /// </summary>
        [TestMethod]
        public void CanConvertTest()
        {
            BoosterCustomConverter converter = new BoosterCustomConverter();

            Assert.IsTrue(converter.CanConvert(typeof(int)));
        }

        /// <summary>
        /// Tests the <see cref="BoosterCustomConverter.ReadJson(JsonReader, System.Type, object, JsonSerializer)"/> method.
        /// </summary>
        [TestMethod]
        public void ReadJsonTest()
        {
            var json = File.ReadAllText("Dto\\BoosterCustomConverterData.json");

            var result = JsonConvert.DeserializeObject<RootSetDto>(json);

            Assert.IsNotNull(result.Set.Booster);
            Assert.AreEqual(16, result.Set.Booster.Count);

            JsonTextReader jsonReader = new JsonTextReader(new StringReader("[[\"rare\",\"mythic rare\"],\"uncommon\",\"uncommon\",\"uncommon\",\"common\",\"common\",\"common\",\"common\",\"common\",\"common\",\"common\",\"common\",\"common\",\"common\",\"land\",\"marketing\"]"));

            BoosterCustomConverter converter = new BoosterCustomConverter();
            var result2 = converter.ReadJson(jsonReader, typeof(List<List<string>>), null, null) as List<List<string>>;
            Assert.IsNotNull(result2);
            Assert.AreEqual(16, result2.Count);
        }

        /// <summary>
        /// Tests the <see cref="BoosterCustomConverter.WriteJson(JsonWriter, object, JsonSerializer)"/> method.
        /// </summary>
        [TestMethod]
        public void WriteJsonTest()
        {
            BoosterCustomConverter converter = new BoosterCustomConverter();

            try
            {
                // Test sending a null parameter.
                converter.WriteJson(null, null, null);
                Assert.Fail();
            }
            catch (NotImplementedException ex)
            {
                Assert.AreEqual("not implemented", ex.Message);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}