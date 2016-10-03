// <copyright file="LegalityTest.cs">
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
            Legality model;

            try
            {
                // Test exception is thrown.
                model = new Legality(null);
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

            var dto = new LegalityDto()
            {
                Format = "format1",
                LegalityName = "fake name"
            };

            model = new Legality(dto);

            Assert.AreEqual(dto.Format, model.Format);
            Assert.AreEqual(dto.LegalityName, model.LegalityName);
        }
    }
}