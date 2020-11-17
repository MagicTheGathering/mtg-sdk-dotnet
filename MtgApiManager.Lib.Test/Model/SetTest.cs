namespace MtgApiManager.Lib.Test.Model
{
    using Lib.Dto;
    using MtgApiManager.Lib.Model;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Set"/> class.
    /// </summary>

    public class SetTest
    {
        private readonly ModelMapper _modelMapper = new ModelMapper();

        /// <summary>
        /// Tests the <see cref="Set.MapSet(SetDto)"/> method.
        /// </summary>
        [Fact]
        public void MapSetTest()
        {
            var dto = new SetDto()
            {
                Block = "block1",
                Border = "border1",
                Code = "code1",
                Expansion = "expansion1",
                GathererCode = "gathererCode1",
                MagicCardsInfoCode = "magicCardsInfoCode",
                Name = "name1",
                OldCode = "oldCode1",
                OnlineOnly = true,
                ReleaseDate = "2016, 1, 1"
            };

            var model = _modelMapper.MapSet(dto);

            Assert.Equal(dto.Block, model.Block);
            Assert.Equal(dto.Border, model.Border);
            Assert.Equal(dto.Code, model.Code);
            Assert.Equal(dto.Expansion, model.Expansion);
            Assert.Equal(dto.GathererCode, model.GathererCode);
            Assert.Equal(dto.MagicCardsInfoCode, model.MagicCardsInfoCode);
            Assert.Equal(dto.Name, model.Name);
            Assert.Equal(dto.OldCode, model.OldCode);
            Assert.Equal(dto.OnlineOnly, model.OnlineOnly);
            Assert.Equal(dto.ReleaseDate, model.ReleaseDate);
        }
    }
}