# GitHub Actions Workflows

This directory contains the CI/CD workflows for the MTG SDK .NET project.

## Workflows

### CI Workflow (`ci.yml`)

Runs on every push to `master` and on all pull requests.

**Features:**
- Builds and tests on multiple platforms (Ubuntu, Windows, macOS)
- Uses GitVersion for semantic versioning
- Runs tests with code coverage (Ubuntu only)
- Uploads coverage reports to Codecov
- Creates NuGet package artifacts
- Validates compatibility across all target frameworks

**Triggers:**
- Push to `master` branch
- Pull requests to `master` branch
- Manual trigger via workflow dispatch

### Release Workflow (`release.yml`)

Publishes the NuGet package to NuGet.org when a GitHub release is created.

**Features:**
- Builds in Release configuration
- Runs full test suite
- Creates and publishes NuGet package
- Uses GitVersion for package versioning
- Uploads package as release artifact

**Triggers:**
- GitHub release published
- Manual trigger via workflow dispatch

### API Contract Tests Workflow (`api-contract-tests.yml`)

Runs integration tests against the live MTG API to detect breaking changes.

**Features:**
- Tests against real MTG API endpoints
- Automatically creates GitHub issues when tests fail
- Uploads test results as artifacts
- Runs on a weekly schedule (Mondays at 2 AM UTC)
- Can be triggered manually with option to disable issue creation

**Triggers:**
- Scheduled: Weekly on Mondays at 2:00 AM UTC
- Manual trigger via workflow dispatch

**Purpose:**
- Early detection of API breaking changes
- Validation that DTOs match API responses
- Monitoring API stability over time

**Note:** These tests are NOT run in regular CI to avoid blocking development with external API dependencies.

## Required Secrets

To use these workflows, you need to configure the following secrets in your GitHub repository settings (Settings → Secrets and variables → Actions):

### `CODECOV_TOKEN`
- **Required for:** CI workflow (code coverage upload)
- **How to get:**
  1. Sign up at [codecov.io](https://codecov.io)
  2. Add your repository
  3. Copy the upload token
- **Current token:** Already embedded in old Nuke build (f7c9d882-99e0-4a44-a651-16ed7cee7bc4)
- **Set it:** `gh secret set CODECOV_TOKEN -b "your-token-here"`

### `NUGET_API_KEY`
- **Required for:** Release workflow (publishing to NuGet.org)
- **How to get:**
  1. Log in to [nuget.org](https://www.nuget.org)
  2. Go to Account → API Keys
  3. Create a new API key with "Push" permission for your package
- **Set it:** `gh secret set NUGET_API_KEY -b "your-api-key-here"`

## Setting Up Secrets

### Using GitHub CLI:
```bash
# Set Codecov token
gh secret set CODECOV_TOKEN -b "your-codecov-token"

# Set NuGet API key
gh secret set NUGET_API_KEY -b "your-nuget-api-key"
```

### Using GitHub Web UI:
1. Go to your repository on GitHub
2. Navigate to Settings → Secrets and variables → Actions
3. Click "New repository secret"
4. Enter the secret name and value
5. Click "Add secret"

## Migrating from Nuke Build

The GitHub Actions workflows replace the Nuke build system with the following equivalents:

| Nuke Target | GitHub Actions Equivalent |
|-------------|---------------------------|
| Clean | Implicit in fresh runner environment |
| Restore | `dotnet restore` step |
| Compile | `dotnet build` step with GitVersion |
| Test | `dotnet test` step with coverage |
| PublishCodeCoverage | `codecov/codecov-action@v4` |
| Pack | `dotnet pack` step |
| NuGet Publish | `dotnet nuget push` in release workflow |

## Local Development

For local development, you can still use standard dotnet CLI commands:

```bash
# Restore, build, and test
dotnet restore src/MtgApiManager.sln
dotnet build src/MtgApiManager.sln --configuration Release
dotnet test src/MtgApiManager.sln --configuration Release

# Create NuGet package
dotnet pack src/MtgApiManager.Lib/MtgApiManager.Lib.csproj --configuration Release --output ./artifacts
```

The Nuke build scripts can be removed or kept for local convenience if desired.
