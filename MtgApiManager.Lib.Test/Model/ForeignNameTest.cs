// <copyright file="ForeignNameTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
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
            var dto = new ForeignNameDto()
            {
                Language = "English",
                MultiverseId = 1,
                Name = "name1"
            };

            var model = new ForeignName(dto);

            Assert.AreEqual(dto.Language, model.Language);
            Assert.AreEqual(dto.MultiverseId, model.MultiverseId);
            Assert.AreEqual(dto.Name, model.Name);
        }
    }
}