# Magic: The Gathering SDK - GitHub Copilot Instructions

This file provides instructions to GitHub Copilot when working with this repository.

## Project Overview

This is the **Magic: The Gathering SDK for C# .NET** - a wrapper around the MTG API (magicthegathering.io). The library is published to NuGet as `MtgApiManager.Lib` and provides a type-safe, async-first interface for querying MTG card and set data.

**Key Features:**
- Async/await pattern throughout
- Automatic rate limiting to comply with MTG API limits
- Fluent query builder interface
- Strong typing with comprehensive models
- Multi-targeting support (.NET Standard 2.0 and .NET 8.0)

## Tech Stack

**Primary Technologies:**
- **Language:** C# 12.0
- **Target Frameworks:** .NET Standard 2.0, .NET 8.0 LTS
- **HTTP Client:** Flurl.Http 4.0.2
- **JSON Serialization:** System.Text.Json 9.0.10

**Testing:**
- **Framework:** xUnit
- **Mocking:** Moq
- **Test Data Generation:** AutoBogus
- **Code Coverage:** Coverlet with Codecov integration

**Build & CI/CD:**
- **CI/CD:** GitHub Actions (Ubuntu, Windows, macOS)
- **Versioning:** GitVersion for semantic versioning
- **Package Manager:** NuGet

## Architecture & Design Patterns

### Core Design Patterns

1. **Service Provider Pattern**: `MtgServiceProvider` acts as the central factory for creating service instances
2. **Repository Pattern**: Services (`CardService`, `SetService`) act as repositories for fetching MTG data
3. **Fluent Query Builder Pattern**: Query building through method chaining via `IMtgQueryable<TService, TQuery>`
4. **DTO/Model Separation**: Clear separation between API contracts (DTOs) and domain models

### Key Abstractions

- **`IMtgServiceProvider`**: Factory for creating service instances
- **`IMtgQueryable<TService, TQuery>`**: Enables fluent query building with `Where()` and `AllAsync()` methods
- **`IOperationResult<T>`**: Wraps all service call results with `IsSuccess`, `Value`, and `Exception` properties
- **`IModelMapper`**: Translates DTOs to domain models

### Important Implementation Details

- **Rate Limiting**: Automatic rate limiting is implemented using `SemaphoreSlim` for thread-safe operation. The SDK spreads calls over 10-second intervals to avoid bursts.
- **Query State**: Queries are **NOT reusable** - the query state is cleared after calling `AllAsync()`.
- **HTTP Client**: All services inherit from `ServiceBase<TModel>` which handles URL construction, API versioning, error handling, and rate limiting.

## Project Structure

```
src/
├── MtgApiManager.Lib/              # Main library
│   ├── Core/                       # Core abstractions, API endpoints, versioning
│   ├── Service/                    # Service implementations (CardService, SetService, etc.)
│   ├── Model/                      # Domain models representing MTG entities
│   ├── Dto/                        # Data Transfer Objects matching API responses
│   └── Utility/                    # Utilities including rate limiting
├── MtgApiManager.Lib.Test/         # Unit tests (xUnit, Moq, AutoBogus) - .NET 8.0
├── MtgApiManager.Lib.IntegrationTest/ # Integration tests
└── MtgApiManager.Lib.TestApp/      # WPF test application - .NET 8.0
```

## Coding Guidelines

### General Principles

- **Async First**: All service methods that make API calls MUST be async and return `Task<T>`
- **Result Pattern**: All service calls MUST return `IOperationResult<T>` - never throw exceptions for API failures
- **Immutability**: Prefer immutable data structures where appropriate
- **Null Safety**: Use nullable reference types consistently
- **Dependency Injection**: Services should be injectable via constructor

### Naming Conventions

- **Classes/Interfaces**: PascalCase (e.g., `CardService`, `IMtgServiceProvider`)
- **Methods**: PascalCase (e.g., `FindAsync`, `AllAsync`)
- **Private Fields**: _camelCase with underscore prefix (e.g., `_httpClient`)
- **Local Variables**: camelCase (e.g., `cardResult`, `serviceProvider`)
- **Async Methods**: MUST end with `Async` suffix (e.g., `FindAsync`, `GetCardTypesAsync`)

### Code Style

- **Indentation**: 4 spaces (no tabs)
- **Line Length**: Keep lines under 120 characters where practical
- **Braces**: Use Allman style (opening brace on new line)
- **Access Modifiers**: Always specify explicitly (e.g., `public`, `private`, `internal`)
- **this Keyword**: Only use when necessary to disambiguate

### Multi-Targeting Requirements

When making changes, ensure compatibility across both target frameworks:
- **netstandard2.0** - Provides broad compatibility with .NET Framework 4.6.1+, .NET Core 2.0+
- **net8.0** - Enables modern .NET 8 APIs and optimizations

**Important**: Avoid using APIs not available in .NET Standard 2.0 unless you use conditional compilation (`#if NET8_0`).

### Error Handling

- **API Calls**: Wrap in try-catch and return `IOperationResult<T>` with `IsSuccess = false` and populated `Exception`
- **Never**: Throw exceptions from service methods - use the Result pattern
- **Validation**: Validate parameters at the start of methods and throw `ArgumentException` for invalid inputs

### Testing Requirements

- **Coverage**: Aim for >80% code coverage on the main library
- **Test Naming**: Use descriptive names that explain what is being tested (e.g., `FindAsync_WithValidId_ReturnsCard`)
- **Arrange-Act-Assert**: Structure tests using AAA pattern
- **Mocking**: Use Moq for dependencies; use AutoBogus for test data generation
- **Async Tests**: All async methods must be tested with async tests

## Build & Development Commands

### Local Development

```bash
# Restore dependencies
dotnet restore src/MtgApiManager.sln

# Build the solution
dotnet build src/MtgApiManager.sln --configuration Release

# Run all tests
dotnet test src/MtgApiManager.sln --configuration Release

# Run tests for specific project
dotnet test src/MtgApiManager.Lib.Test/MtgApiManager.Lib.Test.csproj --configuration Release

# Run tests with coverage
dotnet test src/MtgApiManager.Lib.Test/MtgApiManager.Lib.Test.csproj \
  --configuration Release \
  --collect:"XPlat Code Coverage"

# Pack the library
dotnet pack src/MtgApiManager.Lib/MtgApiManager.Lib.csproj \
  --configuration Release \
  --output ./artifacts
```

### Before Committing

1. Build the solution to ensure no compilation errors
2. Run all tests to ensure no regressions
3. Verify code coverage hasn't decreased significantly
4. Ensure code follows the style guidelines

## Common Patterns & Examples

### Creating a Service and Making API Calls

```csharp
// Create service provider
IMtgServiceProvider serviceProvider = new MtgServiceProvider();

// Get a service
ICardService cardService = serviceProvider.GetCardService();

// Make an API call - always check IsSuccess
IOperationResult<List<ICard>> result = await cardService.AllAsync();
if (result.IsSuccess)
{
    var cards = result.Value;
    // Process cards
}
else
{
    // Handle error
    var exception = result.Exception;
}
```

### Using the Fluent Query Builder

```csharp
// Build and execute a query
var result = await cardService
    .Where(x => x.Set, "ktk")
    .Where(x => x.SubTypes, "warrior,human")
    .Where(x => x.Page, 1)
    .Where(x => x.PageSize, 100)
    .AllAsync();

// Note: Query state is cleared after AllAsync()
// The following would start a new query:
var newResult = await cardService
    .Where(x => x.Set, "bfz")
    .AllAsync();
```

### Implementing a New Service Method

```csharp
public async Task<IOperationResult<ICard>> FindAsync(string id, CancellationToken cancellationToken = default)
{
    try
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("ID cannot be null or empty", nameof(id));
        }

        // Make API call
        var dto = await GetAsync<CardDto>($"{ApiEndpoint.Cards}/{id}", cancellationToken);
        
        // Map to domain model
        var model = ModelMapper.MapCard(dto);
        
        // Return success result
        return new OperationResult<ICard>
        {
            IsSuccess = true,
            Value = model
        };
    }
    catch (Exception ex)
    {
        // Return failure result
        return new OperationResult<ICard>
        {
            IsSuccess = false,
            Exception = ex
        };
    }
}
```

## Internal Visibility

The main library exposes internals to the test project via `InternalsVisibleTo` attribute:
- `MtgApiManager.Lib.Test`
- `DynamicProxyGenAssembly2` (for Moq)

## API Response Handling

- DTOs represent the exact JSON structure from the MTG API
- Models are the public-facing domain objects
- `ModelMapper` handles conversion from DTOs to Models
- Always check `IsSuccess` before accessing `Value` on `IOperationResult<T>`

## Additional Resources

For more detailed information about the architecture, refer to the `CLAUDE.md` file in the repository root.

## GitHub Actions Workflows

**CI Workflow** (`.github/workflows/ci.yml`)
- Runs on push to `master` and all pull requests
- Builds and tests on Ubuntu, Windows, and macOS
- Uses GitVersion for semantic versioning
- Generates code coverage reports

**Release Workflow** (`.github/workflows/release.yml`)
- Runs when a GitHub release is published
- Builds, tests, and publishes to NuGet.org
- Requires `NUGET_API_KEY` secret

## Contributing Guidelines

When contributing to this repository:
1. Make minimal, focused changes
2. Ensure all tests pass
3. Maintain or improve code coverage
4. Follow the existing code style
5. Update documentation if adding new features
6. Ensure compatibility with both .NET Standard 2.0 and .NET 8.0
