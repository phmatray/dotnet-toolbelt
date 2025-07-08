# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CCross.ThrowIf is a .NET library providing a fluent API for throwing exceptions based on various conditions. It uses expression trees to capture both parameter names and values for better error messages.

## Essential Commands

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests with code coverage (already configured per xUnit v3 docs)
dotnet test --collect:"Code Coverage"

# Run the demo application
dotnet run --project CCrossThrowIf.Demo

# Run a specific test
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"

# Check for build warnings (important for nullable reference types)
dotnet build -warnaserror
```

## Architecture and Key Patterns

### Expression-Based API Design

The library heavily uses `Expression<Func<T>>` to extract both parameter names and values. This pattern is implemented in:
- `Metadata<T>` class: Extracts member name and value from expressions
- `Helper.GetMetadata<T>()` extension method: Creates Metadata instances

### Exception Creation Strategy

The `Helper.CreateException<TException>()` method dynamically creates exceptions with appropriate constructor parameters based on the exception type:
- `ArgumentNullException`: Uses (paramName, message) constructor
- `ArgumentOutOfRangeException`: Uses (paramName, actualValue, message) constructor
- `ArgumentException`: Uses (message, paramName) constructor
- Other exceptions: Uses (message) constructor

### Static Class Hierarchy

```
ThrowIf (static)
├── Argument (static) - ArgumentException, ArgumentNullException, ArgumentOutOfRangeException
├── Collection (static) - Not implemented
├── Value (static) - Not implemented
└── ArrayIndex (static) - Not implemented

ThrowIf<TException> (static generic) - For custom exception types
```

### Test Project Configuration

- Uses xUnit v3 with Microsoft Testing Platform
- Code coverage is configured with `Microsoft.Testing.Extensions.CodeCoverage`
- `InternalsVisibleTo` attribute allows testing internal types
- All 178 tests currently passing

### Key Implementation Details

1. **Nullable Reference Types**: Project uses C# nullable reference types (`<Nullable>enable</Nullable>`)
2. **Target Framework**: .NET 9.0
3. **Internal Types**: `Helper` and `Metadata<T>` are internal but accessible to tests via `InternalsVisibleTo`

### Common Development Patterns

When adding new validation methods:
1. Add the method to the appropriate static class (e.g., `ThrowIf.Argument`)
2. Use expression parameter: `Expression<Func<T>> expression`
3. Extract metadata: `var metadata = expression.GetMetadata()`
4. Check condition and throw: `if (condition) throw Helper.CreateException<ExceptionType>(message, metadata.Name, metadata.Value)`
5. Add corresponding method to `ThrowIf<TException>` for generic exception support
6. Write comprehensive tests covering all scenarios

### New Guard API

A modern guard clause API is also available in `Guard.cs` that:
- Returns validated values for method chaining
- Uses `CallerArgumentExpression` instead of expression trees
- Provides better performance and simpler syntax
- Supports fluent extension methods

Example:
```csharp
// Old expression-based API
ThrowIf.Argument.IsNullOrWhiteSpace(() => username);

// New Guard API
var validUsername = Guard.Against.NullOrWhiteSpace(username);
```