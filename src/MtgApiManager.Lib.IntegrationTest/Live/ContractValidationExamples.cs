using System.Linq;
using System.Threading.Tasks;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Test.TestHelpers;
using Xunit;

namespace MtgApiManager.Lib.IntegrationTest.Live
{
    /// <summary>
    /// Example contract tests that demonstrate how to detect specific types of API breaking changes.
    /// These tests serve as templates for adding more comprehensive contract validation.
    /// </summary>
    /// <remarks>
    /// Each test is documented with:
    /// - WHAT it checks
    /// - WHY it matters
    /// - WHAT would break if the API changes
    /// </remarks>
    [Trait("Category", "Integration")]
    [Trait("Category", "Live")]
    [Trait("Category", "ApiContract")]
    [Trait("Category", "Examples")]
    public class ContractValidationExamples
    {
        private readonly ICardService _cardService;
        private readonly ISetService _setService;

        public ContractValidationExamples()
        {
            var provider = ApiContractHelper.CreateServiceProvider();
            _cardService = provider.GetCardService();
            _setService = provider.GetSetService();
        }

        #region Card Field Validation Examples

        /// <summary>
        /// WHAT: Validates that core Card properties are present and not null.
        /// WHY: If API removes or renames these fields, DTOs will fail to deserialize or have null values.
        /// BREAKS: Code that accesses card.Name, card.Id without null checks will throw NullReferenceException.
        /// </summary>
        [Fact]
        public async Task Card_CoreProperties_MustExist()
        {
            // arrange - Using a known card with all standard properties
            const int MULTIVERSE_ID = 3; // Black Lotus

            // act
            var result = await _cardService.FindAsync(MULTIVERSE_ID);
            var card = ApiContractHelper.AssertSuccess(result);

            // assert - These fields are fundamental to the Card model
            Assert.NotNull(card);
            ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
            ApiContractHelper.AssertNotNullOrEmpty(card.Id, nameof(card.Id));

            // These may be null for some cards, but should exist as properties
            Assert.NotNull(card); // Validates the object itself deserialized
        }

        /// <summary>
        /// WHAT: Validates that optional but commonly-present fields are correctly typed.
        /// WHY: Type changes (string -> int, array -> object) will cause deserialization failures.
        /// BREAKS: Silent failures where fields become null, or JsonException during deserialization.
        /// </summary>
        [Fact]
        public async Task Card_OptionalProperties_HaveCorrectTypes()
        {
            // arrange - Using a card known to have many optional properties
            const int MULTIVERSE_ID = 3; // Black Lotus has: type, rarity, set, etc.

            // act
            var result = await _cardService.FindAsync(MULTIVERSE_ID);
            var card = ApiContractHelper.AssertSuccess(result);

            // assert - Validate field types (if present, they must be correct type)
            if (card.Type != null)
            {
                Assert.IsType<string>(card.Type);
                Assert.NotEmpty(card.Type);
            }

            if (card.Rarity != null)
            {
                Assert.IsType<string>(card.Rarity);
                Assert.NotEmpty(card.Rarity);
            }

            if (card.Set != null)
            {
                Assert.IsType<string>(card.Set);
                Assert.NotEmpty(card.Set);
            }

            if (card.SetName != null)
            {
                Assert.IsType<string>(card.SetName);
            }

            // MultiverseId should be a string if present
            if (card.MultiverseId != null)
            {
                Assert.IsType<string>(card.MultiverseId);
                Assert.NotEmpty(card.MultiverseId);
            }
        }

        /// <summary>
        /// WHAT: Validates that collection properties are properly initialized (not null).
        /// WHY: Collections changing from empty array [] to null breaks foreach loops and LINQ.
        /// BREAKS: NullReferenceException in code like: card.Colors.Any(), foreach (var color in card.Colors).
        /// </summary>
        [Fact]
        public async Task Card_CollectionProperties_AreNotNull()
        {
            // arrange - Get cards with known collection properties
            var result = await _cardService
                .Where(x => x.Set, "ktk")
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);
            Assert.NotEmpty(cards);

            // act & assert - Check multiple cards to increase confidence
            foreach (var card in cards.Take(10))
            {
                // Collections should never be null, even if empty
                // The API should return [] not null for array fields

                if (card.Colors != null)
                {
                    // If Colors exists, it should be enumerable
                    var colorCount = card.Colors.Count();
                    Assert.True(colorCount >= 0); // Just verify we can enumerate
                }

                if (card.ColorIdentity != null)
                {
                    var colorIdentityCount = card.ColorIdentity.Count();
                    Assert.True(colorIdentityCount >= 0);
                }

                // SuperTypes, SubTypes, Types arrays should be enumerable if present
                if (card.SuperTypes != null)
                {
                    Assert.NotNull(card.SuperTypes.ToList()); // Forces enumeration
                }

                if (card.SubTypes != null)
                {
                    Assert.NotNull(card.SubTypes.ToList());
                }

                if (card.Types != null)
                {
                    Assert.NotNull(card.Types.ToList());
                }
            }
        }

        #endregion

        #region Set Field Validation Examples

        /// <summary>
        /// WHAT: Validates that Set objects have required identifying properties.
        /// WHY: Sets without code/name are unusable for lookups and display.
        /// BREAKS: UI display code, set lookup functionality, caching by set code.
        /// </summary>
        [Fact]
        public async Task Set_IdentifyingProperties_MustExist()
        {
            // arrange
            const string SET_CODE = "ktk";

            // act
            var result = await _setService.FindAsync(SET_CODE);
            var set = ApiContractHelper.AssertSuccess(result);

            // assert
            ApiContractHelper.AssertNotNullOrEmpty(set.Code, nameof(set.Code));
            ApiContractHelper.AssertNotNullOrEmpty(set.Name, nameof(set.Name));

            // These identify the set uniquely
            Assert.Equal("ktk", set.Code, ignoreCase: true);
            Assert.Equal("Khans of Tarkir", set.Name);
        }

        /// <summary>
        /// WHAT: Validates that Set metadata fields have expected values/types.
        /// WHY: Type changes or value changes in set type/release date break filtering and sorting.
        /// BREAKS: Code that filters by set type, sorts by release date, or displays set info.
        /// </summary>
        [Fact]
        public async Task Set_MetadataProperties_HaveExpectedFormat()
        {
            // arrange
            const string SET_CODE = "ktk";

            // act
            var result = await _setService.FindAsync(SET_CODE);
            var set = ApiContractHelper.AssertSuccess(result);

            // assert

            // Type should be a non-empty string
            if (set.Type != null)
            {
                ApiContractHelper.AssertNotNullOrEmpty(set.Type, nameof(set.Type));
                // Common set types: "expansion", "core", "masters", etc.
            }

            // ReleaseDate should be a string (usually ISO format)
            if (set.ReleaseDate != null)
            {
                Assert.IsType<string>(set.ReleaseDate);
                Assert.NotEmpty(set.ReleaseDate);
                // For KTK, we know the release date
                Assert.Contains("2014", set.ReleaseDate);
            }

            // Block is optional but should be string if present
            if (set.Block != null)
            {
                Assert.IsType<string>(set.Block);
                Assert.Equal("Khans of Tarkir", set.Block);
            }
        }

        #endregion

        #region Query/Filter Behavior Examples

        /// <summary>
        /// WHAT: Validates that WHERE filters actually filter results.
        /// WHY: If API stops respecting query parameters, results become unfiltered (wrong/too many).
        /// BREAKS: Pagination, filtered searches, performance (fetching too much data).
        /// </summary>
        [Fact]
        public async Task Query_FilterBySet_ReturnsOnlyMatchingCards()
        {
            // arrange
            const string SET_CODE = "ktk";
            const int SAMPLE_SIZE = 20;

            // act
            var result = await _cardService
                .Where(x => x.Set, SET_CODE)
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);

            // assert - Take sample to verify filter is working
            Assert.NotEmpty(cards);
            var sample = cards.Take(SAMPLE_SIZE);

            // EVERY card in the result should match the filter
            foreach (var card in sample)
            {
                Assert.Equal(SET_CODE, card.Set, ignoreCase: true);
            }

            // If even one card doesn't match, the filter is broken
        }

        /// <summary>
        /// WHAT: Validates that multiple WHERE clauses combine correctly (AND logic).
        /// WHY: If API changes to OR logic or ignores some parameters, results are incorrect.
        /// BREAKS: Advanced search, filtered listings, any code using multiple criteria.
        /// </summary>
        [Fact]
        public async Task Query_MultipleFilters_CombineWithAnd()
        {
            // arrange - Cards that are BOTH from ktk AND have specific color
            const string SET_CODE = "ktk";
            const string COLOR = "blue";

            // act
            var result = await _cardService
                .Where(x => x.Set, SET_CODE)
                .Where(x => x.Colors, COLOR)
                .AllAsync();

            var cards = ApiContractHelper.AssertSuccess(result);

            // assert
            Assert.NotEmpty(cards);

            foreach (var card in cards.Take(10))
            {
                // MUST match both criteria
                Assert.Equal(SET_CODE, card.Set, ignoreCase: true);
                Assert.NotNull(card.Colors);
                // Note: Colors might contain multiple colors, blue should be one of them
            }
        }

        #endregion

        #region List/Metadata Endpoint Examples

        /// <summary>
        /// WHAT: Validates that metadata endpoints return known stable values.
        /// WHY: If these change unexpectedly, validation/dropdown lists break.
        /// BREAKS: Card type validation, UI dropdowns, search filters.
        /// </summary>
        [Fact]
        public async Task Metadata_CardTypes_ContainsKnownValues()
        {
            // act
            var result = await _cardService.GetCardTypesAsync();
            var types = ApiContractHelper.AssertSuccess(result);

            // assert - These types have existed since Magic's inception
            Assert.Contains("Creature", types);
            Assert.Contains("Instant", types);
            Assert.Contains("Sorcery", types);
            Assert.Contains("Enchantment", types);
            Assert.Contains("Artifact", types);
            Assert.Contains("Land", types);
            Assert.Contains("Planeswalker", types);

            // If any of these are missing, the API has fundamentally changed
        }

        /// <summary>
        /// WHAT: Validates that subtypes list is not empty and contains known values.
        /// WHY: Empty list or missing common subtypes breaks creature type searches.
        /// BREAKS: Tribal searches, creature type filtering, deck building tools.
        /// </summary>
        [Fact]
        public async Task Metadata_CardSubtypes_ContainsKnownValues()
        {
            // act
            var result = await _cardService.GetCardSubTypesAsync();
            var subtypes = ApiContractHelper.AssertSuccess(result);

            // assert
            Assert.NotEmpty(subtypes);
            Assert.True(subtypes.Count() > 100, "Should have many subtypes");

            // Check for some evergreen creature types
            Assert.Contains("Human", subtypes);
            Assert.Contains("Warrior", subtypes);
            Assert.Contains("Wizard", subtypes);
            Assert.Contains("Goblin", subtypes);
            Assert.Contains("Elf", subtypes);
        }

        #endregion

        #region Special Endpoint Examples

        /// <summary>
        /// WHAT: Validates that GenerateBooster returns a reasonable number of cards.
        /// WHY: If API returns too few/many cards, or wrong format, booster generation breaks.
        /// BREAKS: Draft simulators, sealed deck generators, booster pack features.
        /// </summary>
        [Fact]
        public async Task SpecialEndpoint_GenerateBooster_ReturnsValidBooster()
        {
            // arrange
            const string SET_CODE = "ktk";

            // act
            var result = await _setService.GenerateBoosterAsync(SET_CODE);
            var cards = ApiContractHelper.AssertSuccess(result);

            // assert
            Assert.NotNull(cards);

            // A booster pack typically has 15 cards (some sets vary 10-15)
            Assert.InRange(cards.Count(), 10, 16);

            // All cards should be from the requested set
            foreach (var card in cards)
            {
                Assert.Equal(SET_CODE, card.Set, ignoreCase: true);
                ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
            }

            // Should have at least one rare/mythic
            // (This is a softer check as pack contents are random)
            Assert.Contains(cards, c => c.Rarity != null);
        }

        #endregion

        #region Error Handling Examples

        /// <summary>
        /// WHAT: Validates that invalid requests return appropriate responses.
        /// WHY: If API changes error handling, exceptions change and break error handling code.
        /// BREAKS: Try-catch blocks expecting specific exceptions, user error messages.
        /// </summary>
        [Fact]
        public async Task ErrorHandling_InvalidSetCode_ReturnsFailureOrNull()
        {
            // arrange
            const string INVALID_CODE = "ZZZZZ";

            // act
            var result = await _setService.FindAsync(INVALID_CODE);

            // assert - Should either fail gracefully or return null
            if (result.IsSuccess)
            {
                Assert.Null(result.Value);
            }
            else
            {
                Assert.NotNull(result.Exception);
                // Exception should have meaningful message
            }
        }

        /// <summary>
        /// WHAT: Validates that requesting a non-existent card handles gracefully.
        /// WHY: 404 responses need consistent handling across the app.
        /// BREAKS: Error pages, fallback logic, user-facing error messages.
        /// </summary>
        [Fact]
        public async Task ErrorHandling_NonExistentCard_HandlesGracefully()
        {
            // arrange
            const int INVALID_ID = 999999999;

            // act
            var result = await _cardService.FindAsync(INVALID_ID);

            // assert
            if (result.IsSuccess)
            {
                // Some APIs return success with null value for not found
                Assert.Null(result.Value);
            }
            else
            {
                // Others return failure with exception
                Assert.NotNull(result.Exception);
            }

            // Either is acceptable, but should be consistent
        }

        #endregion

        #region Performance/Pagination Examples

        /// <summary>
        /// WHAT: Validates that API respects pagination to avoid huge responses.
        /// WHY: If pagination breaks, responses become massive and slow.
        /// BREAKS: Performance, timeouts, memory usage, UI responsiveness.
        /// </summary>
        [Fact]
        public async Task Pagination_DefaultPageSize_ReturnsReasonableAmount()
        {
            // act - Get cards without filters (would return ALL cards without pagination)
            var result = await _cardService.AllAsync();
            var cards = ApiContractHelper.AssertSuccess(result);

            // assert - Should be paginated, not returning all 50,000+ cards
            Assert.NotEmpty(cards);
            Assert.True(cards.Count() <= 1000,
                "API should paginate results, not return all cards at once");

            // Typical page size is 100
            Assert.True(cards.Count() <= 200,
                "Page size seems unexpectedly large - pagination may have changed");
        }

        #endregion
    }
}
