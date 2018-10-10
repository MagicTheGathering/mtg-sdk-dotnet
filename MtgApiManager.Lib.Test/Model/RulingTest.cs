// <copyright file="RulingTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using System;
    using Xunit;

    /// <summary>
    /// Tests methods in the <see cref="Ruling"/> class.
    /// </summary>

    public class RulingTest
    {
        /// <summary>
        /// Tests the <see cref="Ruling.Ruling(RulingDto)"/> method.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Ruling model;

            try
            {
                // Test exception is thrown.
                model = new Ruling(null);
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

            var dto = new RulingDto()
            {
                Date = "2016, 10, 2",
                Text = "testing"
            };

            model = new Ruling(dto);

            Assert.Equal(dto.Date, model.Date);
            Assert.Equal(dto.Text, model.Text);
        }
    }
}