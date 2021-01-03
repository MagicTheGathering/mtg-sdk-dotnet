using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class PagingInfoTests
    {
        [Fact]
        public void Create_Success()
        {
            // arrange
            const int TOTAL_COUNT = 100;
            const int PAGE_SIZE = 50;

            // act
            var pagingInfo = PagingInfo.Create(TOTAL_COUNT, PAGE_SIZE);

            // assert
            Assert.Equal(TOTAL_COUNT, pagingInfo.TotalCount);
            Assert.Equal(PAGE_SIZE, pagingInfo.PageSize);
        }

        [Fact]
        public void TotalPages_NoRemainder_Correct()
        {
            // arrange
            var pagingInfo = PagingInfo.Create(100, 50);

            // act
            var result = pagingInfo.TotalPages;

            // assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void TotalPages_WithRemainder_Correct()
        {
            // arrange
            var pagingInfo = PagingInfo.Create(500, 30);

            // act
            var result = pagingInfo.TotalPages;

            // assert
            Assert.Equal(17, result);
        }
    }
}