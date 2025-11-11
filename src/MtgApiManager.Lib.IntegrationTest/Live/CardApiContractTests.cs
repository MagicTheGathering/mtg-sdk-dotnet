using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Test.TestHelpers;
using Xunit;

namespace MtgApiManager.Lib.IntegrationTest.Live
{
    /// <summary>
    /// Integration tests to verify the Card API contract hasn't changed in breaking ways.
    /// These tests call the real MTG API and should be run on a schedule, not in CI.
    /// Set environment variable SKIP_LIVE_API_TESTS=true to skip these tests.
    /// </summary>
    [Trait("Category", "Integration")]
    [Trait("Category", "Live")]
    [Trait("Category", "ApiContract")]
    public class CardApiContractTests
    {
        private readonly ICardService _cardService;

        public CardApiContractTests()
        {
            var provider = ApiContractHelper.CreateServiceProvider();
            _cardService = provider.GetCardService();
        }

        [Fact]
        public async Task FindAsync_ByMultiverseId_ReturnsValidCardStructure()
        {
            // arrange - Using Black Lotus from Beta (multiverse ID: 3)
            const int MULTIVERSE_ID = 3;

            // act
            var result = await _cardService.FindAsync(MULTIVERSE_ID);

            // assert - Verify response structure
            var card = ApiContractHelper.AssertSuccess(result);

            // Check core properties exist
            ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
            ApiContractHelper.AssertNotNullOrEmpty(card.Id, nameof(card.Id));
            Assert.Equal("Black Lotus", card.Name);
        }

        [Fact]
        public async Task AllAsync_WithoutFilters_ReturnsValidCardList()
        {
            // act
            var result = await _cardService.AllAsync();

            // assert
            var cards = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(cards);
            Assert.NotEmpty(cards);

            // Verify first card has valid structure
            var firstCard = cards.First();
            ApiContractHelper.AssertNotNullOrEmpty(firstCard.Name, nameof(firstCard.Name));
            ApiContractHelper.AssertNotNullOrEmpty(firstCard.Id, nameof(firstCard.Id));
        }

        [Fact]
        public async Task WhereAsync_FilterBySet_ReturnsMatchingCards()
        {
            // arrange - Query for cards from Khans of Tarkir
            const string SET_CODE = "ktk";

            // act
            var result = await _cardService
                .Where(x => x.Set, SET_CODE)
                .AllAsync();

            // assert
            var cards = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(cards);
            Assert.NotEmpty(cards);

            // Verify all returned cards are from the requested set
            foreach (var card in cards)
            {
                Assert.Equal(SET_CODE, card.Set, ignoreCase: true);
            }
        }

        [Fact]
        public async Task WhereAsync_FilterByName_ReturnsMatchingCard()
        {
            // arrange
            const string CARD_NAME = "Lightning Bolt";

            // act
            var result = await _cardService
                .Where(x => x.Name, CARD_NAME)
                .AllAsync();

            // assert
            var cards = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(cards);
            Assert.NotEmpty(cards);

            // Verify returned cards have the requested name
            Assert.All(cards, card =>
                Assert.Equal(CARD_NAME, card.Name, ignoreCase: true));
        }

        [Fact]
        public async Task GetCardTypesAsync_ReturnsValidTypes()
        {
            // act
            var result = await _cardService.GetCardTypesAsync();

            // assert
            var types = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(types);
            Assert.NotEmpty(types);

            // Verify well-known card types exist
            Assert.Contains("Creature", types);
            Assert.Contains("Instant", types);
            Assert.Contains("Sorcery", types);
        }

        [Fact]
        public async Task GetCardSubTypesAsync_ReturnsValidSubtypes()
        {
            // act
            var result = await _cardService.GetCardSubTypesAsync();

            // assert
            var subtypes = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(subtypes);
            Assert.NotEmpty(subtypes);

            // Verify well-known subtypes exist
            Assert.Contains("Human", subtypes);
            Assert.Contains("Warrior", subtypes);
        }

        [Fact]
        public async Task GetCardSuperTypesAsync_ReturnsValidSupertypes()
        {
            // act
            var result = await _cardService.GetCardSuperTypesAsync();

            // assert
            var supertypes = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(supertypes);
            Assert.NotEmpty(supertypes);

            // Verify well-known supertypes exist
            Assert.Contains("Legendary", supertypes);
            Assert.Contains("Basic", supertypes);
        }

        [Fact]
        public async Task GetFormatsAsync_ReturnsValidFormats()
        {
            // act
            var result = await _cardService.GetFormatsAsync();

            // assert
            var formats = ApiContractHelper.AssertSuccess(result);

            Assert.NotNull(formats);
            Assert.NotEmpty(formats);

            // Verify well-known formats exist
            Assert.Contains("Standard", formats);
            Assert.Contains("Modern", formats);
        }
    }
}
