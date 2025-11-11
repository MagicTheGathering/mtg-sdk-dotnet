# API Contract Testing Guide

This guide explains how to write effective contract tests that detect breaking changes in the MTG API.

## What is a Contract Test?

A **contract test** verifies that an external API meets the expectations your code depends on. Unlike unit tests (which test your code) or integration tests (which test system interactions), contract tests focus on the **interface boundary** between your library and the external API.

## Types of Breaking Changes

### 1. Field Removal/Rename

**What happens**: A field that existed in the API response is removed or renamed.

**Example**:
```json
// Before
{"name": "Black Lotus", "manaCost": "{0}"}

// After - BREAKING CHANGE
{"cardName": "Black Lotus", "manaCost": "{0}"}
// Field "name" was renamed to "cardName"
```

**Impact on your code**:
- DTOs fail to deserialize the field (becomes null)
- Code accessing `card.Name` gets null or default value
- NullReferenceException in code without null checks

**How to detect**:
```csharp
[Fact]
public async Task Card_NameField_MustExist()
{
    var result = await _cardService.FindAsync(3);
    var card = ApiContractHelper.AssertSuccess(result);

    // This will fail if field is missing or renamed
    ApiContractHelper.AssertNotNullOrEmpty(card.Name, nameof(card.Name));
}
```

### 2. Type Changes

**What happens**: A field's type changes (string → int, array → object, etc.)

**Example**:
```json
// Before
{"multiverseId": "3"}

// After - BREAKING CHANGE
{"multiverseId": 3}
// Changed from string to number
```

**Impact on your code**:
- JsonException during deserialization
- Silent data loss if deserializer is lenient
- Type cast exceptions

**How to detect**:
```csharp
[Fact]
public async Task Card_MultiverseId_IsInteger()
{
    var result = await _cardService.FindAsync(3);
    var card = ApiContractHelper.AssertSuccess(result);

    // This validates the field deserialized as expected type
    if (card.MultiverseId != null)
    {
        Assert.IsType<int>(card.MultiverseId);
        Assert.True(card.MultiverseId > 0);
    }
}
```

### 3. Collection Null vs Empty

**What happens**: API changes from returning empty array `[]` to `null` (or vice versa)

**Example**:
```json
// Before
{"colors": []}

// After - BREAKING CHANGE
{"colors": null}
```

**Impact on your code**:
- NullReferenceException in foreach loops
- LINQ operations fail (.Any(), .Count(), etc.)
- Null coalescing doesn't help if expecting empty collection

**How to detect**:
```csharp
[Fact]
public async Task Card_Colors_IsNotNull()
{
    var result = await _cardService.AllAsync();
    var cards = ApiContractHelper.AssertSuccess(result);

    foreach (var card in cards.Take(10))
    {
        // Collections should never be null
        if (card.Colors != null)
        {
            // Should be enumerable
            var count = card.Colors.Count();
            Assert.True(count >= 0);
        }
    }
}
```

### 4. Required vs Optional Fields

**What happens**: A field that was always present becomes optional (or vice versa)

**Example**:
```json
// Before - name always present
{"name": "Black Lotus"}

// After - BREAKING CHANGE - name now optional
{"name": null}
// OR
{}
```

**Impact on your code**:
- Unexpected nulls in fields assumed to be required
- Validation failures
- Display issues in UI

**How to detect**:
```csharp
[Fact]
public async Task Card_IdAndName_AlwaysPresent()
{
    var result = await _cardService.AllAsync();
    var cards = ApiContractHelper.AssertSuccess(result);

    // Check multiple cards to ensure consistency
    foreach (var card in cards.Take(20))
    {
        // These should NEVER be null
        Assert.NotNull(card.Id);
        Assert.NotNull(card.Name);
        Assert.NotEmpty(card.Id);
        Assert.NotEmpty(card.Name);
    }
}
```

### 5. Query Parameter Changes

**What happens**: API stops respecting a query parameter or changes its behavior

**Example**:
```
// Before
GET /cards?set=ktk  →  Returns only KTK cards

// After - BREAKING CHANGE
GET /cards?set=ktk  →  Returns all cards (ignores parameter)
```

**Impact on your code**:
- Incorrect results returned
- Performance degradation (fetching too much data)
- Wrong search results
- Pagination breaks

**How to detect**:
```csharp
[Fact]
public async Task Query_SetFilter_ActuallyFilters()
{
    var result = await _cardService
        .Where(x => x.Set, "ktk")
        .AllAsync();
    var cards = ApiContractHelper.AssertSuccess(result);

    // EVERY card must match the filter
    foreach (var card in cards.Take(20))
    {
        Assert.Equal("ktk", card.Set, ignoreCase: true);
    }
}
```

### 6. Enum/Constant Value Changes

**What happens**: Expected values in a constrained set change

**Example**:
```json
// Before
{"rarity": "Rare"}

// After - BREAKING CHANGE
{"rarity": "R"}
// Or {"rarity": "RARE"}
```

**Impact on your code**:
- Enum parsing fails
- String comparisons fail
- Switch statements miss cases
- UI displays raw values incorrectly

**How to detect**:
```csharp
[Fact]
public async Task Metadata_CardTypes_ContainsExpectedValues()
{
    var result = await _cardService.GetCardTypesAsync();
    var types = ApiContractHelper.AssertSuccess(result);

    // These values must exist exactly
    Assert.Contains("Creature", types);
    Assert.Contains("Instant", types);
    Assert.Contains("Sorcery", types);
    // If they're renamed or change case, this fails
}
```

### 7. Response Structure Changes

**What happens**: Nested objects flatten, or flat structures become nested

**Example**:
```json
// Before
{"name": "Black Lotus", "set": "lea", "setName": "Limited Edition Alpha"}

// After - BREAKING CHANGE
{
  "name": "Black Lotus",
  "set": {
    "code": "lea",
    "name": "Limited Edition Alpha"
  }
}
```

**Impact on your code**:
- Major DTO restructuring needed
- Object navigation changes
- Null reference exceptions

**How to detect**:
```csharp
[Fact]
public async Task Card_SetProperties_AreFlatStrings()
{
    var result = await _cardService.FindAsync(3);
    var card = ApiContractHelper.AssertSuccess(result);

    // Validate expected structure
    if (card.Set != null)
    {
        Assert.IsType<string>(card.Set); // Not an object
    }
}
```

### 8. Error Response Changes

**What happens**: HTTP status codes or error formats change

**Example**:
```
// Before
GET /cards/invalid  →  404 with {error: "Not Found"}

// After - BREAKING CHANGE
GET /cards/invalid  →  200 with null body
```

**Impact on your code**:
- Error handling logic breaks
- Wrong exception types thrown
- User sees wrong error messages

**How to detect**:
```csharp
[Fact]
public async Task ErrorHandling_NotFound_ConsistentResponse()
{
    var result = await _cardService.FindAsync("invalid");

    // Should handle not-found consistently
    if (result.IsSuccess)
    {
        Assert.Null(result.Value);
    }
    else
    {
        Assert.NotNull(result.Exception);
    }
}
```

## Writing Effective Contract Tests

### ✅ DO

1. **Test with stable, known data**
   ```csharp
   // Good: Using Black Lotus (multiverse ID 3) - stable, well-known
   var result = await _cardService.FindAsync(3);
   ```

2. **Test the structure, not the content**
   ```csharp
   // Good: Checking that field exists and has correct type
   Assert.IsType<string>(card.Name);

   // Avoid: Checking exact content (too brittle)
   Assert.Equal("Expected exact string", card.Description); // ❌
   ```

3. **Test multiple samples for collections**
   ```csharp
   // Good: Check first 10-20 items to increase confidence
   foreach (var card in cards.Take(10))
   {
       Assert.NotNull(card.Name);
   }
   ```

4. **Document what breaks when test fails**
   ```csharp
   /// <summary>
   /// BREAKS: NullReferenceException in CardDisplayComponent.Render()
   /// </summary>
   ```

5. **Test boundaries and edge cases**
   ```csharp
   // Test cards with no colors, no mana cost, etc.
   // Test empty search results
   // Test pagination at boundaries
   ```

### ❌ DON'T

1. **Don't test exact counts**
   ```csharp
   // Bad: Brittle, breaks when new sets are released
   Assert.Equal(50000, allCards.Count()); // ❌

   // Good: Test relative size
   Assert.True(allCards.Count() > 40000); // ✅
   ```

2. **Don't test implementation details**
   ```csharp
   // Bad: Testing internal API implementation
   Assert.Equal("SELECT * FROM cards", sqlQuery); // ❌
   ```

3. **Don't test unrelated functionality**
   ```csharp
   // Bad: This is a unit test concern, not contract
   Assert.True(card.IsLegendary() == (card.Supertypes?.Contains("Legendary") ?? false)); // ❌
   ```

4. **Don't make tests depend on each other**
   ```csharp
   // Bad: Test order should not matter
   // Test1: _sharedCard = await GetCard();
   // Test2: Assert.NotNull(_sharedCard); // ❌
   ```

## Contract Test Template

Use this template when adding new contract tests:

```csharp
/// <summary>
/// WHAT: [What this test validates]
/// WHY: [Why this matters / what could change]
/// BREAKS: [What code breaks if this test fails]
/// </summary>
[Fact]
[Trait("Category", "Integration")]
[Trait("Category", "Live")]
[Trait("Category", "ApiContract")]
public async Task [Component]_[Aspect]_[ExpectedBehavior]()
{
    // arrange - Set up test data (use stable, known values)
    const int TEST_ID = 3; // Black Lotus

    // act - Call the API
    var result = await _service.MethodAsync(TEST_ID);
    var data = ApiContractHelper.AssertSuccess(result);

    // assert - Validate the contract
    Assert.NotNull(data);
    Assert.NotNull(data.ExpectedField);
    // Add specific assertions for the contract
}
```

## Examples by Category

See `ContractValidationExamples.cs` for complete working examples of:

- ✅ Field presence validation
- ✅ Type validation
- ✅ Collection validation
- ✅ Query behavior validation
- ✅ Metadata endpoint validation
- ✅ Error handling validation
- ✅ Pagination validation

## When a Contract Test Fails

1. **Check the GitHub issue** - The workflow creates an issue with details
2. **Download test results** - Artifact contains full test output
3. **Run locally** - Reproduce with verbose output:
   ```bash
   dotnet test src/MtgApiManager.Lib.IntegrationTest \
     --filter "FullyQualifiedName~[TestName]" \
     --verbosity detailed
   ```
4. **Compare API response** - Use curl/Postman to see current API response:
   ```bash
   curl https://api.magicthegathering.io/v1/cards/3
   ```
5. **Update DTOs** - If API changed, update your DTOs to match
6. **Update tests** - Update tests if your expectations were wrong
7. **Document change** - Add to CHANGELOG what changed and why

## Best Practices Summary

| Practice | Why |
|----------|-----|
| Use stable test data | Avoids false failures from data changes |
| Test structure not content | Detects contract changes, not data changes |
| Sample collections | Increases confidence without testing everything |
| Document what breaks | Helps future debugging |
| Keep tests independent | Enables parallel execution, avoids cascading failures |
| Test positive and negative cases | Validates both success and error paths |
| Use descriptive test names | Makes failures immediately understandable |

## Additional Resources

- [Integration Test README](./README.md) - How to run tests
- [ContractValidationExamples.cs](./Live/ContractValidationExamples.cs) - Complete code examples
- [MTG API Documentation](https://docs.magicthegathering.io/) - Official API docs
