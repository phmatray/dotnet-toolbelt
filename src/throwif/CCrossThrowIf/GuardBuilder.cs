using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

/// <summary>
/// Fluent builder for complex validation chains
/// </summary>
public sealed class GuardBuilder<T>
{
    private readonly T _value;
    private readonly string? _paramName;
    private readonly List<(Predicate<T> predicate, string message)> _validations = new();

    internal GuardBuilder(T value, string? paramName)
    {
        _value = value;
        _paramName = paramName;
    }

    /// <summary>
    /// Adds a validation rule
    /// </summary>
    public GuardBuilder<T> Must(
        Predicate<T> predicate,
        string? errorMessage = null,
        [CallerArgumentExpression(nameof(predicate))] string? predicateExpression = null)
    {
        _validations.Add((predicate, errorMessage ?? $"Value must satisfy: {predicateExpression}"));
        return this;
    }

    /// <summary>
    /// Adds a validation that the value must not satisfy a condition
    /// </summary>
    public GuardBuilder<T> MustNot(
        Predicate<T> predicate,
        string? errorMessage = null,
        [CallerArgumentExpression(nameof(predicate))] string? predicateExpression = null)
    {
        _validations.Add((
            value => !predicate(value),
            errorMessage ?? $"Value must not satisfy: {predicateExpression}"
        ));
        return this;
    }

    /// <summary>
    /// Adds a validation only when a condition is met
    /// </summary>
    public GuardBuilder<T> When(
        bool condition,
        Predicate<T> predicate,
        string? errorMessage = null)
    {
        if (condition)
        {
            _validations.Add((predicate, errorMessage ?? "Conditional validation failed"));
        }
        return this;
    }

    /// <summary>
    /// Transforms the value if all validations pass
    /// </summary>
    public GuardBuilder<TResult> Transform<TResult>(Func<T, TResult> transformer)
    {
        // First validate
        Validate();
        
        // Then transform
        var transformed = transformer(_value);
        return new GuardBuilder<TResult>(transformed, _paramName);
    }

    /// <summary>
    /// Executes all validations and returns the value
    /// </summary>
    public T Validate()
    {
        foreach (var (predicate, message) in _validations)
        {
            if (!predicate(_value))
            {
                throw new ArgumentException(message, _paramName);
            }
        }
        
        return _value;
    }

    /// <summary>
    /// Executes all validations and returns a result
    /// </summary>
    public ValidationResult<T> TryValidate()
    {
        foreach (var (predicate, message) in _validations)
        {
            if (!predicate(_value))
            {
                return ValidationResult<T>.Failure(message);
            }
        }
        
        return ValidationResult<T>.Success(_value);
    }
}

/// <summary>
/// Result of a validation attempt
/// </summary>
public readonly struct ValidationResult<T>
{
    public bool IsValid { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }

    private ValidationResult(bool isValid, T? value, string? errorMessage)
    {
        IsValid = isValid;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static ValidationResult<T> Success(T value) => 
        new(true, value, null);

    public static ValidationResult<T> Failure(string errorMessage) => 
        new(false, default, errorMessage);

    public T GetValueOrThrow()
    {
        if (!IsValid)
        {
            throw new InvalidOperationException(ErrorMessage ?? "Validation failed");
        }
        
        return Value!;
    }

    public bool TryGetValue([NotNullWhen(true)] out T? value)
    {
        value = Value;
        return IsValid;
    }
}

/// <summary>
/// Extensions for fluent validation
/// </summary>
public static class GuardBuilderExtensions
{
    /// <summary>
    /// Starts a fluent validation chain
    /// </summary>
    public static GuardBuilder<T> Validate<T>(
        this T value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        return new GuardBuilder<T>(value, paramName);
    }

    /// <summary>
    /// Adds a not-null validation for reference types
    /// </summary>
    public static GuardBuilder<T> NotNull<T>(
        this GuardBuilder<T?> builder) where T : class
    {
        return builder.Must(
            value => value is not null,
            "Value cannot be null");
    }

    /// <summary>
    /// Adds a not-empty validation for strings
    /// </summary>
    public static GuardBuilder<string> NotEmpty(
        this GuardBuilder<string?> builder)
    {
        return builder.Must(
            value => !string.IsNullOrEmpty(value),
            "Value cannot be null or empty");
    }

    /// <summary>
    /// Adds a range validation for comparable types
    /// </summary>
    public static GuardBuilder<T> InRange<T>(
        this GuardBuilder<T> builder,
        T min,
        T max) where T : IComparable<T>
    {
        return builder.Must(
            value => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0,
            $"Value must be between {min} and {max}");
    }
}