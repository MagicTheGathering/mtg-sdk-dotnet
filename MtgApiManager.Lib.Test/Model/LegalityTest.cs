// <copyright file="LegalityTest.cs">
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
    /// Tests methods in the <see cref="Legality"/> class.
    /// </summary>

    public class LegalityTest
    {
        /// <summary>
        /// Tests the <see cref="Legality.Legality(LegalityDto)"/> method.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            Legality model;

            try
            {
                // Test exception is thrown.
                model = new Legality(null);
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

            var dto = new LegalityDto()
            {
                Format = "format1",
                LegalityName = "fake name"
            };

            model = new Legality(dto);

            Assert.Equal(dto.Format, model.Format);
            Assert.Equal(dto.LegalityName, model.LegalityName);
        }
    }
}