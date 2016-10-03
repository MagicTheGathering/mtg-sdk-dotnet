// <copyright file="RulingTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Model;

    /// <summary>
    /// Tests methods in the <see cref="Ruling"/> class.
    /// </summary>
    [TestClass]
    public class RulingTest
    {
        /// <summary>
        /// Tests the <see cref="Ruling.Ruling(RulingDto)"/> method.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            var dto = new RulingDto()
            {
                Date = new System.DateTime(2016, 10, 2),
                Text = "testing"
            };

            var model = new Ruling(dto);

            Assert.AreEqual(dto.Date, model.Date);
            Assert.AreEqual(dto.Text, model.Text);
        }
    }
}