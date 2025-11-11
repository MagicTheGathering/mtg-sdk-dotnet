# MTG API Manager Integration Tests

This project contains integration tests that verify the library's contract with the live MTG API (magicthegathering.io).

## Purpose

These tests are designed to:

1. **Detect API Breaking Changes**: Identify when the MTG API changes in ways that break the library
2. **Verify Response Structure**: Ensure DTOs still match the API responses
3. **Test Real Behavior**: Exercise the library against actual API responses, not mocks
4. **Monitor API Stability**: Track if the API is responding correctly over time

## Test Organization

```
Live/
├── CardApiContractTests.cs    # Tests for Card API endpoints
├── SetApiContractTests.cs     # Tests for Set API endpoints
└── ApiHeadersTests.cs         # Tests for rate limiting and multi-request behavior

TestHelpers/
└── ApiContractHelper.cs       # Shared utilities for integration tests
```

## Running the Tests

### Automated Runs (Recommended)

These tests run automatically via GitHub Actions (see `.github/workflows/api-contract-tests.yml`):

- **Schedule**: Weekly on Mondays at 2:00 AM UTC
- **Purpose**: Early detection of API changes
- **On Failure**: Automatically creates a GitHub issue with details
- **Manual Trigger**: Can be run on-demand from the Actions tab

This approach ensures:
- ✅ API changes are detected quickly
- ✅ Development workflow is not blocked
- ✅ Historical data shows API stability over time

### Run Locally/Manually

All tests are tagged with the `Category=Live` trait. To run them locally:

```bash
# Run all integration tests
dotnet test src/MtgApiManager.Lib.IntegrationTest --filter "Category=Live"

# Run with detailed output
dotnet test src/MtgApiManager.Lib.IntegrationTest --filter "Category=Live" --verbosity detailed

# Run specific test class
dotnet test src/MtgApiManager.Lib.IntegrationTest --filter "FullyQualifiedName~CardApiContractTests"
```

### Why Not in CI?

These tests are excluded from the main CI workflow because they:
- Call the real MTG API (requires network, slower)
- May be rate-limited by the API
- Could fail due to API downtime (outside our control)
- Should not block PR merges or development

By running on a schedule instead, we get the monitoring benefits without impacting development velocity.

## Test Design Philosophy

### Stable Test Data

Tests use well-known, stable cards and sets that are unlikely to change:
- **Black Lotus** (multiverse ID: 3) - Alpha edition
- **Lightning Bolt** - Common across many sets
- **Khans of Tarkir** (set code: ktk) - Recent, complete set

### What Tests Check

1. **Response Structure**: Core properties exist (Name, Id, Code, etc.)
2. **Expected Values**: Known cards/sets return expected data
3. **Query Filtering**: Where clauses return matching results
4. **Rate Limiting**: Multiple requests complete successfully
5. **Special Endpoints**: Card types, subtypes, formats, booster generation

### What Tests Don't Check

- Exact card counts or pagination details (can change as sets are added)
- Specific rate limit values (may change by API provider)
- Performance/latency (varies by network and API load)

## Test Helper Methods

The `ApiContractHelper` class provides utilities:

```csharp
// Assert operation succeeded and return value
var card = ApiContractHelper.AssertSuccess(result);

// Assert operation failed
var exception = ApiContractHelper.AssertFailure(result);

// Assert property is not null/empty
ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));

// Create service provider
var provider = ApiContractHelper.CreateServiceProvider();
```

## Environment Variables

- `SKIP_LIVE_API_TESTS=true` - Currently not used, but reserved for future use to conditionally skip tests
- `API_TEST_TIMEOUT=30` - Timeout in seconds for API calls (default: 30)

## Adding New Tests

When adding new integration tests:

1. Add them to an existing test class or create a new one in `Live/`
2. Mark with appropriate traits: `[Trait("Category", "Integration")]`, `[Trait("Category", "Live")]`
3. Use stable, well-known test data
4. Check response structure, not exact content (unless testing known stable data)
5. Use `ApiContractHelper` methods for common assertions
6. Tests will automatically run in the scheduled workflow

## Example Test

```csharp
[Fact]
[Trait("Category", "Integration")]
[Trait("Category", "Live")]
public async Task FindAsync_KnownCard_ReturnsValidStructure()
{
    // arrange
    const int MULTIVERSE_ID = 3; // Black Lotus

    // act
    var result = await _cardService.FindAsync(MULTIVERSE_ID);

    // assert
    var card = ApiContractHelper.AssertSuccess(result);
    ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
    Assert.Equal("Black Lotus", card.Name);
}
```

The traits ensure the test is:
- Excluded from regular CI runs
- Included in the scheduled API contract test workflow
- Easy to filter when running locally

## Troubleshooting

**Tests fail with timeout**: Increase `API_TEST_TIMEOUT` or check network connectivity

**Tests fail with rate limiting errors**: The MTG API has rate limits. Space out test runs or reduce parallel execution.

**Tests fail with 404/not found**: The API may be down or the test data has changed. Verify the API is accessible at https://api.magicthegathering.io/v1/

**Tests fail with deserialization errors**: **This is the key signal!** The API response structure may have changed. Investigate which DTOs need updating.
