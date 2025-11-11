using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Test.TestHelpers;
using Xunit;

namespace MtgApiManager.Lib.IntegrationTest.Live
{
    /// <summary>
    /// Integration tests to verify the API behavior and rate limiting work correctly.
    /// These tests ensure that the library handles the API correctly under various conditions.
    /// </summary>
    [Trait("Category", "Integration")]
    [Trait("Category", "Live")]
    [Trait("Category", "ApiContract")]
    public class ApiHeadersTests
    {
        private readonly ICardService _cardService;
        private readonly ISetService _setService;

        public ApiHeadersTests()
        {
            var provider = ApiContractHelper.CreateServiceProvider();
            _cardService = provider.GetCardService();
            _setService = provider.GetSetService();
        }

        [Fact]
        public async Task CardService_AllAsync_ReturnsValidData()
        {
            // act
            var result = await _cardService.AllAsync();

            // assert
            var cards = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(cards);
            Assert.NotEmpty(cards);
        }

        [Fact]
        public async Task SetService_AllAsync_ReturnsValidData()
        {
            // act
            var result = await _setService.AllAsync();

            // assert
            var sets = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(sets);
            Assert.NotEmpty(sets);
        }

        [Fact]
        public async Task CardService_RateLimiting_MultipleRequestsSucceed()
        {
            // arrange - Make multiple rapid requests to test rate limiting

            // act - Make 3 requests in quick succession
            var result1 = await _cardService.Where(x => x.Set, "ktk").AllAsync();
            var result2 = await _cardService.Where(x => x.Set, "frf").AllAsync();
            var result3 = await _cardService.Where(x => x.Set, "dtk").AllAsync();

            // assert - All requests should succeed (rate limiting should handle throttling)
            ApiContractHelper.AssertSuccess(result1);
            ApiContractHelper.AssertSuccess(result2);
            ApiContractHelper.AssertSuccess(result3);
        }

        [Fact]
        public async Task SetService_RateLimiting_MultipleRequestsSucceed()
        {
            // arrange - Make multiple rapid requests to test rate limiting

            // act - Make 3 requests in quick succession
            var result1 = await _setService.Where(x => x.Block, "Khans of Tarkir").AllAsync();
            var result2 = await _setService.Where(x => x.Block, "Battle for Zendikar").AllAsync();
            var result3 = await _setService.Where(x => x.Block, "Shadows over Innistrad").AllAsync();

            // assert - All requests should succeed (rate limiting should handle throttling)
            ApiContractHelper.AssertSuccess(result1);
            ApiContractHelper.AssertSuccess(result2);
            ApiContractHelper.AssertSuccess(result3);
        }
    }
}
