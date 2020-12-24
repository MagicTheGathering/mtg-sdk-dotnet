namespace MtgApiManager.Lib.Test.Core
{
    using Lib.Core;
    using Lib.Dto;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Tests the functionality of the <see cref="MtgApiServiceAdapter"/> class.
    /// </summary>

    public class MtgApiServiceAdapterTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiServiceAdapter.WebGetAsync(Uri)"/> method.
        /// </summary>
        [Fact]
        public async Task WebGetAsyncTest()
        {
            var moqHeaderManager = new Mock<IHeaderManager>();
            MtgApiServiceAdapter adapter = new MtgApiServiceAdapter(moqHeaderManager.Object);

            // Test exception is thrown.
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => adapter.WebGetAsync<RootCardDto>(null));
            Assert.Equal("requestUri", exception.ParamName);
        }
    }
}