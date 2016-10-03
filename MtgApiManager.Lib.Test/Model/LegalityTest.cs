// <copyright file="LegalityTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Model;

    /// <summary>
    /// Tests methods in the <see cref="Legality"/> class.
    /// </summary>
    [TestClass]
    public class LegalityTest
    {
        /// <summary>
        /// Tests the <see cref="Legality.Legality(LegalityDto)"/> method.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            var dto = new LegalityDto()
            {
                Format = "format1",
                LegalityName = "fake name"
            };

            var model = new Legality(dto);

            Assert.AreEqual(dto.Format, model.Format);
            Assert.AreEqual(dto.LegalityName, model.LegalityName);
        }
    }
}