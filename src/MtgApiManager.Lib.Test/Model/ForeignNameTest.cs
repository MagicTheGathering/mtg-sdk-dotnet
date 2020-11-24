namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using Xunit;

    /// <summary>
    /// Tests methods in the <see cref="ForeignName"/> class.
    /// </summary>
    public class ForeignNameTest
    {
        private readonly ModelMapper _modelMapper = new ModelMapper();

        /// <summary>
        /// Tests the <see cref="ForeignName.ForeignName(ForeignNameDto)"/> method.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            var dto = new ForeignNameDto()
            {
                Language = "English",
                MultiverseId = 1,
                Name = "name1"
            };

            var model = _modelMapper.MapForeignName(dto);

            Assert.Equal(dto.Language, model.Language);
            Assert.Equal(dto.MultiverseId, model.MultiverseId);
            Assert.Equal(dto.Name, model.Name);
        }
    }
}