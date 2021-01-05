using MtgApiManager.Lib.Service;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class SetQueryParameterTests
    {
        [Fact]
        public void SetQueryParameter_BlockSet_Success()
        {
            // arrange
            const string BLOCK = "block1";
            var queryParams = new SetQueryParameter
            {
                // act
                Block = BLOCK
            };

            // assert
            Assert.Equal(BLOCK, queryParams.Block);
        }

        [Fact]
        public void SetQueryParameter_NameSet_Success()
        {
            // arrange
            const string NAME = "name1";
            var queryParams = new SetQueryParameter
            {
                // act
                Name = NAME
            };

            // assert
            Assert.Equal(NAME, queryParams.Name);
        }
    }
}