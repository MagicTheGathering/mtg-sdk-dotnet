// <copyright file="ForeignNameTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using System;
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Model;

    /// <summary>
    /// Tests methods in the <see cref="ForeignName"/> class.
    /// </summary>
    [TestClass]
    public class ForeignNameTest
    {
        /// <summary>
        /// Tests the <see cref="ForeignName.ForeignName(ForeignNameDto)"/> method.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            ForeignName model;

            try
            {
                // Test exception is thrown.
                model = new ForeignName(null);
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

            var dto = new ForeignNameDto()
            {
                Language = "English",
                MultiverseId = 1,
                Name = "name1"
            };

            model = new ForeignName(dto);

            Assert.AreEqual(dto.Language, model.Language);
            Assert.AreEqual(dto.MultiverseId, model.MultiverseId);
            Assert.AreEqual(dto.Name, model.Name);
        }
    }
}