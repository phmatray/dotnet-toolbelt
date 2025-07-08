# Migration Guide: From Expression-based API to Modern Guard API

## Overview

The new API design offers several improvements:
- **Better Performance**: No expression tree compilation overhead
- **Simpler Syntax**: Direct method calls instead of lambda expressions
- **Return Values**: Methods return validated values for chaining
- **Modern C# Features**: Uses CallerArgumentExpression, nullable annotations, and generic math
- **IntelliSense Friendly**: Better IDE support and discoverability

## Key Differences

### 1. Expression Removal
**Old API:**
```csharp
ThrowIf.Argument.IsNullOrWhiteSpace(() => username);
```

**New API:**
```csharp
Guard.Against.NullOrWhiteSpace(username);
```

### 2. Return Values
**Old API:**
```csharp
ThrowIf.Argument.IsNull(() => repository);
_repository = repository; // Separate assignment
```

**New API:**
```csharp
_repository = Guard.Against.Null(repository); // Validate and assign
```

### 3. Method Chaining
**Old API:**
```csharp
ThrowIf.Argument.IsNullOrWhiteSpace(() => input);
var processed = input.Trim().ToUpper();
```

**New API:**
```csharp
var processed = Guard.Against.NullOrWhiteSpace(input)
    .Trim()
    .ToUpper();
```

## Migration Examples

### String Validation

#### IsNullOrWhiteSpace
```csharp
// Old
ThrowIf.Argument.IsNullOrWhiteSpace(() => name, "Name is required");

// New
var validName = Guard.Against.NullOrWhiteSpace(name, message: "Name is required");
```

#### IsNullOrEmpty
```csharp
// Old
ThrowIf.Argument.IsNullOrEmpty(() => email);

// New
var validEmail = Guard.Against.NullOrEmpty(email);
```

### Numeric Validation

#### IsNegative
```csharp
// Old
ThrowIf.Argument.IsNegative(() => age);

// New
var validAge = Guard.Against.Negative(age);
```

#### IsGreaterThan
```csharp
// Old
ThrowIf.Argument.IsGreaterThan(() => count, 100);

// New (using range)
var validCount = Guard.Against.OutOfRange(count, 0, 100);
```

### DateTime Validation

#### IsInThePast
```csharp
// Old
ThrowIf.Argument.IsInThePast(() => appointmentDate);

// New
var validDate = Guard.Against.PastDateTime(appointmentDate);
```

### Generic Validation

#### IsNull
```csharp
// Old
ThrowIf.Argument.IsNull(() => service);

// New
var validService = Guard.Against.Null(service);
```

#### IsDefault
```csharp
// Old
ThrowIf.Argument.IsDefault(() => id);

// New
var validId = Guard.Against.Default(id);
```

## Constructor Pattern

**Old Pattern:**
```csharp
public class UserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;
    private readonly string _connectionString;
    
    public UserService(IUserRepository repository, ILogger logger, string connectionString)
    {
        ThrowIf.Argument.IsNull(() => repository);
        ThrowIf.Argument.IsNull(() => logger);
        ThrowIf.Argument.IsNullOrEmpty(() => connectionString);
        
        _repository = repository;
        _logger = logger;
        _connectionString = connectionString;
    }
}
```

**New Pattern:**
```csharp
public class UserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;
    private readonly string _connectionString;
    
    public UserService(IUserRepository repository, ILogger logger, string connectionString)
    {
        _repository = Guard.Against.Null(repository);
        _logger = Guard.Against.Null(logger);
        _connectionString = Guard.Against.NullOrEmpty(connectionString);
    }
}
```

## Fluent Extensions

The new API also supports fluent extension methods:

```csharp
// Using extension methods
var email = userInput
    .GuardNullOrWhiteSpace()
    .Trim()
    .ToLowerInvariant();

// Combining guards
var config = configuration
    .GuardNull()
    .ConnectionString
    .GuardNullOrEmpty();
```

## Custom Exception Types

If you need specific exception types (like the old `ThrowIf<TException>`), you can create wrapper methods:

```csharp
public static class CustomGuard
{
    public static T InvalidOperation<T>(T value, Func<T, bool> condition, string? message = null)
    {
        if (condition(value))
        {
            throw new InvalidOperationException(message ?? "Invalid operation");
        }
        return value;
    }
}
```

## Benefits of Migration

1. **Performance**: Eliminate expression compilation overhead
2. **Readability**: Cleaner, more intuitive syntax
3. **Functional Style**: Support for method chaining and functional composition
4. **Type Safety**: Better null-safety with nullable reference types
5. **Modern C#**: Leverage latest language features

## Gradual Migration

You can migrate gradually by:
1. Keep both APIs during transition
2. Update new code to use the new API
3. Refactor existing code module by module
4. Remove old API once migration is complete

## Breaking Changes

- Method names have changed (e.g., `IsNull` → `Null`)
- Parameters are passed directly, not as expressions
- All methods now return the validated value
- Some validations are combined (e.g., separate greater/less than → range validation)