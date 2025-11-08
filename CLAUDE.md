# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the Magic: The Gathering SDK for C# .NET - a wrapper around the MTG API (magicthegathering.io). The library is published to NuGet as `MtgApiManager.Lib` and supports .NET Standard 2.0, .NET 6.0, and .NET 8.0 LTS.

## Build System

This project uses **GitHub Actions** for CI/CD automation. The workflows are located in `.github/workflows/`.

### GitHub Actions Workflows

**CI Workflow** (`.github/workflows/ci.yml`)
- Runs on push to `master` and all pull requests
- Builds and tests on Ubuntu, Windows, and macOS
- Uses GitVersion for semantic versioning
- Generates code coverage reports (Codecov)
- Creates NuGet package artifacts

**Release Workflow** (`.github/workflows/release.yml`)
- Runs when a GitHub release is published
- Builds, tests, and publishes to NuGet.org
- Requires `NUGET_API_KEY` secret to be configured

### Required GitHub Secrets

Configure these in repository settings (Settings → Secrets and variables → Actions):
- `CODECOV_TOKEN` - For uploading code coverage reports
- `NUGET_API_KEY` - For publishing to NuGet.org

### Local Development Commands

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


## Architecture

### High-Level Design Patterns

1. **Service Provider Pattern**: `MtgServiceProvider` acts as the central factory for creating service instances
2. **Repository Pattern**: Services (`CardService`, `SetService`) act as repositories for fetching MTG data from the API
3. **Fluent Query Builder Pattern**: Query building through method chaining via `IMtgQueryable<TService, TQuery>`
4. **DTO/Model Separation**: Clear separation between API contracts (DTOs) and domain models

### Project Structure

- **`src/MtgApiManager.Lib/`** - Main library
  - `Core/` - Core abstractions, API endpoints, versioning, header management
  - `Service/` - Service implementations (`CardService`, `SetService`, etc.)
  - `Model/` - Domain models representing MTG entities
  - `Dto/` - Data Transfer Objects matching API responses
  - `Utility/` - Utilities including rate limiting
- **`src/MtgApiManager.Lib.Test/`** - Unit tests (xUnit, Moq, AutoBogus) - targets .NET 8.0
- **`src/MtgApiManager.Lib.TestApp/`** - WPF test application for manual testing - targets .NET 8.0
- **`build/`** - Nuke build system configuration

### Key Abstractions

- **`IMtgServiceProvider`**: Factory for creating service instances
  - `GetCardService()` - Returns card service
  - `GetSetService()` - Returns set service

- **`IMtgQueryable<TService, TQuery>`**: Enables fluent query building
  - `Where<U>(Expression<Func<TQuery, U>> property, string value)` - Adds query parameters
  - `AllAsync()` - Executes the query and clears the query state

- **`IOperationResult<T>`**: Wraps all service call results
  - `IsSuccess` - Indicates if operation succeeded
  - `Value` - Contains the result value
  - `Exception` - Contains exception if operation failed

- **`IModelMapper`**: Translates DTOs to domain models

### Rate Limiting

The SDK implements automatic rate limiting to comply with the MTG API's rate limits:

- Implemented in the `RateLimit` class using `SemaphoreSlim` for thread safety
- Configurable requests per hour
- Automatically spreads calls over 10-second intervals to avoid bursts
- Can be enabled/disabled via constructor
- Rate limiting is transparent to the consumer - the SDK handles delays automatically

### HTTP Client

- Uses **Flurl.Http** for HTTP requests
- Base class `ServiceBase<TModel>` handles URL construction, API versioning, error handling, and rate limiting integration
- All services inherit from this base and implement specific endpoint logic
- Supports cancellation tokens throughout

### Query Builder Pattern

The `Where()` method uses LINQ expressions for type-safe query building:

```csharp
var result = await cardService
    .Where(x => x.Set, "ktk")
    .Where(x => x.SubTypes, "warrior,human")
    .AllAsync();
```

**Important**: The query state is cleared after calling `AllAsync()`, so queries are not reusable.

## Testing

- **Framework**: xUnit
- **Mocking**: Moq
- **Test Data Generation**: AutoBogus
- **Code Coverage**: Coverlet (with Codecov integration)

Test project targets .NET 8.0 and includes test data files in `Data/` directory (e.g., `card1.json`).

### Running Specific Tests

```bash
# Run all tests
dotnet test src/MtgApiManager.Lib.Test/MtgApiManager.Lib.Test.csproj

# Run tests with filter
dotnet test src/MtgApiManager.Lib.Test/MtgApiManager.Lib.Test.csproj --filter "FullyQualifiedName~CardService"

# Run tests with coverage (via Nuke)
./build.ps1 Test
```

## Important Development Notes

### Internal Visibility
The main library exposes internals to the test project via `InternalsVisibleTo` attribute:
- `MtgApiManager.Lib.Test`
- `DynamicProxyGenAssembly2` (for Moq)

### Multi-Targeting
The library targets multiple frameworks (netstandard2.0, net6.0, net8.0). When making changes, ensure compatibility across all target frameworks. Note that .NET 6.0 is out of support as of November 2024, but is maintained for backward compatibility.

### API Response Handling
- All API calls return `IOperationResult<T>` - always check `IsSuccess` before accessing `Value`
- DTOs represent the exact JSON structure from the API
- Models are the public-facing domain objects
- `ModelMapper` handles conversion from DTOs to Models

### Dependencies
- **Flurl.Http 3.2.0** - HTTP client
- **System.Text.Json 9.0.0** - JSON serialization (upgraded to fix security vulnerabilities)
