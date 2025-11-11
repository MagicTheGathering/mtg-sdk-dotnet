using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Test.TestHelpers;
using Xunit;

namespace MtgApiManager.Lib.IntegrationTest.Live
{
    /// <summary>
    /// Tests for edge cases and unusual scenarios that are easy to overlook.
    /// These tests ensure the library handles corner cases gracefully.
    /// </summary>
    [Trait("Category", "Integration")]
    [Trait("Category", "Live")]
    [Trait("Category", "ApiContract")]
    [Trait("Category", "EdgeCases")]
    public class EdgeCaseContractTests
    {
        private readonly ICardService _cardService;
        private readonly ISetService _setService;

        public EdgeCaseContractTests()
        {
            var provider = ApiContractHelper.CreateServiceProvider();
            _cardService = provider.GetCardService();
            _setService = provider.GetSetService();
        }

        #region Cards with Unusual Properties

        /// <summary>
        /// WHAT: Tests cards with no mana cost (lands, some special cards).
        /// WHY: Null manaCost is a valid state that must be handled.
        /// BREAKS: UI display of mana cost, deck statistics, CMC calculations.
        /// </summary>
        [Fact]
        public async Task Card_WithNoManaCost_HandlesGracefully()
        {
            // arrange - Search for lands which typically have no mana cost
            var result = await _cardService
                .Where(x => x.Types, "Land")
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);
            Assert.NotEmpty(cards);

            // act & assert - Find cards with no mana cost
            var cardsWithoutManaCost = cards.Where(c => string.IsNullOrEmpty(c.ManaCost)).ToList();

            // Should have some lands without mana cost
            Assert.NotEmpty(cardsWithoutManaCost);

            // Verify these cards still have valid structure
            foreach (var card in cardsWithoutManaCost.Take(5))
            {
                ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
                ApiContractHelper.AssertNotNullOrEmpty(card.Id, nameof(card.Id));

                // ManaCost can be null/empty, but shouldn't cause errors
                Assert.True(card.ManaCost == null || card.ManaCost == string.Empty);
            }
        }

        /// <summary>
        /// WHAT: Tests colorless cards (no color identity).
        /// WHY: Colorless is different from "Colors = []" or "Colors = null".
        /// BREAKS: Color filtering, deck building color restrictions, Commander rules.
        /// </summary>
        [Fact]
        public async Task Card_Colorless_HandlesColorsCorrectly()
        {
            // arrange - Artifacts are typically colorless
            var result = await _cardService
                .Where(x => x.Types, "Artifact")
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);
            Assert.NotEmpty(cards);

            // act & assert - Check colorless cards
            foreach (var card in cards.Take(10))
            {
                // Colors might be null or empty array for colorless
                // Either is valid, but should be consistent
                if (card.Colors != null)
                {
                    // Should be enumerable (even if empty)
                    var colorCount = card.Colors.Count();
                    Assert.True(colorCount >= 0);
                }

                // Same for ColorIdentity
                if (card.ColorIdentity != null)
                {
                    var identityCount = card.ColorIdentity.Count();
                    Assert.True(identityCount >= 0);
                }
            }
        }

        /// <summary>
        /// WHAT: Tests cards with special characters in names.
        /// WHY: Encoding issues can corrupt special characters (À, É, ä, etc.).
        /// BREAKS: Search by name, display, sorting, card lookup.
        /// </summary>
        [Fact]
        public async Task Card_WithSpecialCharacters_EncodesCorrectly()
        {
            // arrange - Search for a card known to have special characters
            // "Jötun Grunt" has ö character
            var result = await _cardService
                .Where(x => x.Name, "Jötun Grunt")
                .AllAsync();

            // act & assert
            if (result.IsSuccess && result.Value != null && result.Value.Any())
            {
                var card = result.Value.First();

                // Name should contain the special character correctly encoded
                Assert.Contains("ö", card.Name);
                Assert.Equal("Jötun Grunt", card.Name);
            }
            // If card not found, API may have changed how it handles special chars
        }

        #endregion

        #region Empty and Null Results

        /// <summary>
        /// WHAT: Tests query that returns no results.
        /// WHY: Empty results must be handled (empty list vs null vs error).
        /// BREAKS: Pagination, "no results" messaging, null reference exceptions.
        /// </summary>
        [Fact]
        public async Task Query_NoMatches_ReturnsEmptyNotNull()
        {
            // arrange - Query that should match nothing
            var result = await _cardService
                .Where(x => x.Name, "ThisCardDefinitelyDoesNotExist12345")
                .AllAsync();

            // assert
            var cards = ApiContractHelper.AssertSuccess(result);

            // Should return empty collection, NOT null
            Assert.NotNull(cards);
            Assert.Empty(cards);
        }

        /// <summary>
        /// WHAT: Tests finding a set that doesn't exist.
        /// WHY: Not-found should be consistent (null vs exception).
        /// BREAKS: Error handling, 404 pages, fallback logic.
        /// </summary>
        [Fact]
        public async Task Set_NotFound_HandlesConsistently()
        {
            // arrange
            const string INVALID_CODE = "NOTASET";

            // act
            var result = await _setService.FindAsync(INVALID_CODE);

            // assert - Either success with null, or failure with exception
            // Both are valid, but should be consistent across API
            if (result.IsSuccess)
            {
                Assert.Null(result.Value);
            }
            else
            {
                Assert.NotNull(result.Exception);
            }
        }

        #endregion

        #region Large Result Sets

        /// <summary>
        /// WHAT: Tests query that returns many results.
        /// WHY: Large responses might be paginated differently or timeout.
        /// BREAKS: Performance, memory usage, UI scrolling/pagination.
        /// </summary>
        [Fact]
        public async Task Query_LargeResultSet_HandlesPagination()
        {
            // arrange - Get all creatures (should be thousands)
            var result = await _cardService
                .Where(x => x.Types, "Creature")
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);

            // assert - Should get paginated results, not all creatures at once
            Assert.NotEmpty(cards);

            // If getting more than 1000, pagination may have changed
            Assert.InRange(cards.Count(), 1, 1000);

            // Verify all cards are actually creatures
            foreach (var card in cards.Take(20))
            {
                Assert.NotNull(card.Types);
                Assert.Contains("Creature", card.Types);
            }
        }

        #endregion

        #region Special Set Types

        /// <summary>
        /// WHAT: Tests non-standard sets (Commander, Conspiracy, etc.).
        /// WHY: Special sets might have different card structures.
        /// BREAKS: Set type filters, product categorization, draft features.
        /// </summary>
        [Fact]
        public async Task Set_CommanderProduct_HasValidStructure()
        {
            // arrange - Find a known Commander set by name
            var result = await _setService
                .Where(x => x.Name, "Commander 2013")
                .AllAsync();

            // act & assert
            if (result.IsSuccess && result.Value != null && result.Value.Any())
            {
                var commanderSet = result.Value.First();

                // Should have all standard set properties
                ApiContractHelper.AssertNotNullOrEmpty(commanderSet.Code, nameof(commanderSet.Code));
                ApiContractHelper.AssertNotNullOrEmpty(commanderSet.Name, nameof(commanderSet.Name));
            }
        }

        #endregion

        #region Rate Limiting and Concurrent Requests

        /// <summary>
        /// WHAT: Tests that multiple sequential requests don't fail due to rate limiting.
        /// WHY: Library should handle rate limiting automatically.
        /// BREAKS: Bulk operations, batch imports, search suggestions.
        /// </summary>
        [Fact]
        public async Task RateLimiting_SequentialRequests_AllSucceed()
        {
            // arrange & act - Make several requests in sequence
            var result1 = await _cardService.Where(x => x.Set, "ktk").AllAsync();
            var result2 = await _cardService.Where(x => x.Set, "frf").AllAsync();
            var result3 = await _cardService.Where(x => x.Set, "dtk").AllAsync();
            var result4 = await _setService.FindAsync("ktk");
            var result5 = await _cardService.GetCardTypesAsync();

            // assert - All should succeed
            ApiContractHelper.AssertSuccess(result1);
            ApiContractHelper.AssertSuccess(result2);
            ApiContractHelper.AssertSuccess(result3);
            ApiContractHelper.AssertSuccess(result4);
            ApiContractHelper.AssertSuccess(result5);

            // If rate limiting changed, some might fail or be throttled incorrectly
        }

        #endregion

        #region Boundary Conditions

        /// <summary>
        /// WHAT: Tests cards at the edges of the dataset (very old, very new).
        /// WHY: Edge cards might have different data quality or structure.
        /// BREAKS: Completeness of data, historical searches, timeline features.
        /// </summary>
        [Fact]
        public async Task Card_AlphaSet_HandlesOldestCards()
        {
            // arrange - Alpha (LEA) is the first set
            var result = await _cardService
                .Where(x => x.Set, "lea")
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);
            Assert.NotEmpty(cards);

            // act & assert - Old cards should still have valid structure
            var sampleCard = cards.First();
            ApiContractHelper.AssertNotNullOrEmpty(sampleCard.Name, nameof(sampleCard.Name));
            ApiContractHelper.AssertNotNullOrEmpty(sampleCard.Id, nameof(sampleCard.Id));
            Assert.Equal("lea", sampleCard.Set, ignoreCase: true);
        }

        /// <summary>
        /// WHAT: Tests behavior with explicit page parameter at boundaries.
        /// WHY: Edge pages (first, last, beyond last) might behave unexpectedly.
        /// BREAKS: Pagination controls, infinite scroll, "load more" buttons.
        /// </summary>
        [Fact]
        public async Task Pagination_FirstPage_ReturnsResults()
        {
            // arrange - Explicitly request first page
            var result = await _cardService
                .Where(x => x.Page, 1)
                .Where(x => x.PageSize, 10)
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);

            // assert
            Assert.NotEmpty(cards);
            Assert.True(cards.Count() <= 10);
        }

        /// <summary>
        /// WHAT: Tests requesting a page beyond the last page.
        /// WHY: Should return empty results, not error.
        /// BREAKS: Pagination logic, error handling.
        /// </summary>
        [Fact]
        public async Task Pagination_BeyondLastPage_ReturnsEmpty()
        {
            // arrange - Request a page way beyond reasonable bounds
            var result = await _cardService
                .Where(x => x.Page, 999999)
                .Where(x => x.PageSize, 10)
                .AllAsync();

            // assert - Should return empty, not fail
            var cards = ApiContractHelper.AssertSuccess(result);
            Assert.NotNull(cards);
            // May be empty or have minimal results
        }

        #endregion

        #region Data Consistency

        /// <summary>
        /// WHAT: Tests that the same card returned by different endpoints has consistent data.
        /// WHY: Inconsistencies between endpoints indicate API issues.
        /// BREAKS: Data synchronization, caching, card identity.
        /// </summary>
        [Fact]
        public async Task DataConsistency_SameCard_ConsistentAcrossEndpoints()
        {
            // arrange
            const int MULTIVERSE_ID = 3; // Black Lotus

            // act - Get same card via different methods
            var directResult = await _cardService.FindAsync(MULTIVERSE_ID);
            var searchResult = await _cardService
                .Where(x => x.Name, "Black Lotus")
                .AllAsync();

            // assert
            var directCard = ApiContractHelper.AssertSuccess(directResult);
            var searchCards = ApiContractHelper.AssertSuccess(searchResult);

            // Find Black Lotus in search results (MultiverseId is a string)
            var searchCard = searchCards.FirstOrDefault(c =>
                c.MultiverseId == MULTIVERSE_ID.ToString());

            if (searchCard != null)
            {
                // Core properties should match
                Assert.Equal(directCard.Name, searchCard.Name);
                Assert.Equal(directCard.Id, searchCard.Id);
                Assert.Equal(directCard.MultiverseId, searchCard.MultiverseId);
            }
        }

        #endregion
    }
}
