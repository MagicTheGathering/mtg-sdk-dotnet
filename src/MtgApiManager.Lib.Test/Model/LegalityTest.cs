namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using Xunit;

    /// <summary>
    /// Tests methods in the <see cref="Legality"/> class.
    /// </summary>

    public class LegalityTest
    {
        private readonly ModelMapper _modelMapper = new ModelMapper();

        /// <summary>
        /// Tests the <see cref="Legality.Legality(LegalityDto)"/> method.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            var dto = new LegalityDto()
            {
                Format = "format1",
                Legality = "fake name"
            };

            var model = _modelMapper.MapLegality(dto);

            Assert.Equal(dto.Format, model.Format);
            Assert.Equal(dto.Legality, model.LegalityName);
        }
    }
}