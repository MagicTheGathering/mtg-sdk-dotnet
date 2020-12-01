namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using Xunit;

    /// <summary>
    /// Tests methods in the <see cref="Ruling"/> class.
    /// </summary>

    public class RulingTest
    {
        private readonly ModelMapper _modelMapper = new ModelMapper();

        /// <summary>
        /// Tests the <see cref="Ruling.Ruling(RulingDto)"/> method.
        /// </summary>
        [Fact]
        public void ContructorTest()
        {
            var dto = new RulingDto()
            {
                Date = "2016, 10, 2",
                Text = "testing"
            };

            var model = _modelMapper.MapRuling(dto);

            Assert.Equal(dto.Date, model.Date);
            Assert.Equal(dto.Text, model.Text);
        }
    }
}