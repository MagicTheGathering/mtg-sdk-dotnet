using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Test.TestHelpers;
using Xunit;

namespace MtgApiManager.Lib.IntegrationTest.Live
{
    /// <summary>
    /// Integration tests to verify the Set API contract hasn't changed in breaking ways.
    /// These tests call the real MTG API and should be run on a schedule, not in CI.
    /// Set environment variable SKIP_LIVE_API_TESTS=true to skip these tests.
    /// </summary>
    [Trait("Category", "Integration")]
    [Trait("Category", "Live")]
    [Trait("Category", "ApiContract")]
    public class SetApiContractTests
    {
        private readonly ISetService _setService;

        public SetApiContractTests()
        {
            var provider = ApiContractHelper.CreateServiceProvider();
            _setService = provider.GetSetService();
        }

        [Fact]
        public async Task FindAsync_KnownSet_ReturnsValidSetStructure()
        {
            // arrange - Using Khans of Tarkir
            const string SET_CODE = "ktk";

            // act
            var result = await _setService.FindAsync(SET_CODE);

            // assert
            var set = ApiContractHelper.AssertSuccess(result);

            // Check core properties exist
            ApiContractHelper.AssertNotNullOrEmpty(set.Code, nameof(set.Code));
            ApiContractHelper.AssertNotNullOrEmpty(set.Name, nameof(set.Name));

            Assert.Equal(SET_CODE, set.Code, ignoreCase: true);
            Assert.Equal("Khans of Tarkir", set.Name);
        }

        [Fact]
        public async Task AllAsync_WithoutFilters_ReturnsValidSetList()
        {
            // act
            var result = await _setService.AllAsync();

            // assert
            var sets = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(sets);
            Assert.NotEmpty(sets);

            // Verify first set has valid structure
            var firstSet = sets.First();
            ApiContractHelper.AssertNotNullOrEmpty(firstSet.Code, nameof(firstSet.Code));
            ApiContractHelper.AssertNotNullOrEmpty(firstSet.Name, nameof(firstSet.Name));
        }

        [Fact]
        public async Task WhereAsync_FilterByName_ReturnsMatchingSet()
        {
            // arrange
            const string SET_NAME = "Khans of Tarkir";

            // act
            var result = await _setService
                .Where(x => x.Name, SET_NAME)
                .AllAsync();

            // assert
            var sets = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(sets);
            Assert.NotEmpty(sets);

            // Verify the returned set has the requested name
            var set = sets.First();
            Assert.Equal(SET_NAME, set.Name);
        }

        [Fact]
        public async Task WhereAsync_FilterByBlock_ReturnsMatchingSets()
        {
            // arrange
            const string BLOCK_NAME = "Khans of Tarkir";

            // act
            var result = await _setService
                .Where(x => x.Block, BLOCK_NAME)
                .AllAsync();

            // assert
            var sets = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(sets);
            Assert.NotEmpty(sets);

            // Verify all returned sets are from the requested block
            foreach (var set in sets)
            {
                Assert.Equal(BLOCK_NAME, set.Block);
            }
        }

        [Fact]
        public async Task GenerateBoosterAsync_KnownSet_ReturnsValidCards()
        {
            // arrange
            const string SET_CODE = "ktk";

            // act
            var result = await _setService.GenerateBoosterAsync(SET_CODE);

            // assert
            var cards = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(cards);
            Assert.NotEmpty(cards);

            // A typical booster has around 15 cards
            Assert.InRange(cards.Count(), 1, 20);

            // Verify the cards have valid structure
            foreach (var card in cards)
            {
                ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
                ApiContractHelper.AssertNotNullOrEmpty(card.Id, nameof(card.Id));
            }

            // All cards should be from the requested set
            foreach (var card in cards)
            {
                Assert.Equal(SET_CODE, card.Set, ignoreCase: true);
            }
        }
    }
}
