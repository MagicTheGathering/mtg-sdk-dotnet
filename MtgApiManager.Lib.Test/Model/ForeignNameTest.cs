// <copyright file="ForeignNameTest.cs">
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
    /// Tests methods in the <see cref="ForeignName"/> class.
    /// </summary>

    public class ForeignNameTest
    {
        /// <summary>
        /// Tests the <see cref="ForeignName.ForeignName(ForeignNameDto)"/> method.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            ForeignName model;

            try
            {
                // Test exception is thrown.
                model = new ForeignName(null);
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

            var dto = new ForeignNameDto()
            {
                Language = "English",
                MultiverseId = 1,
                Name = "name1"
            };

            model = new ForeignName(dto);

            Assert.Equal(dto.Language, model.Language);
            Assert.Equal(dto.MultiverseId, model.MultiverseId);
            Assert.Equal(dto.Name, model.Name);
        }
    }
}