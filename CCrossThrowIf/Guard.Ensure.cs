using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

/// <summary>
/// Provides compile-time and runtime contract validation with performance optimizations
/// </summary>
public static partial class Guard
{
    /// <summary>
    /// Ensures conditions are met without throwing exceptions (assertion-based)
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Ensures a condition is true, optimized away in Release builds
        /// </summary>
        [Conditional("DEBUG")]
        public static void That(
            [DoesNotReturnIf(false)] bool condition,
            [CallerArgumentExpression(nameof(condition))] string? expression = null,
            [CallerFilePath] string? filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!condition)
            {
                throw new InvalidOperationException(
                    $"Assertion failed: {expression} at {filePath}:{lineNumber}");
            }
        }

        /// <summary>
        /// Ensures a value satisfies a predicate
        /// </summary>
        public static T Satisfies<T>(
            T value,
            Predicate<T> predicate,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            [CallerArgumentExpression(nameof(predicate))] string? predicateExpression = null)
        {
            if (!predicate(value))
            {
                throw new ArgumentException(
                    $"Value '{paramName}' does not satisfy condition '{predicateExpression}'",
                    paramName);
            }
            return value;
        }

        /// <summary>
        /// Ensures all values in a collection satisfy a predicate
        /// </summary>
        public static TCollection All<TCollection, T>(
            TCollection collection,
            Predicate<T> predicate,
            [CallerArgumentExpression(nameof(collection))] string? paramName = null)
            where TCollection : IEnumerable<T>
        {
            var index = 0;
            foreach (var item in collection)
            {
                if (!predicate(item))
                {
                    throw new ArgumentException(
                        $"Item at index {index} in '{paramName}' does not satisfy the predicate",
                        paramName);
                }
                index++;
            }
            return collection;
        }

        /// <summary>
        /// Ensures at least one value in a collection satisfies a predicate
        /// </summary>
        public static TCollection Any<TCollection, T>(
            TCollection collection,
            Predicate<T> predicate,
            [CallerArgumentExpression(nameof(collection))] string? paramName = null)
            where TCollection : IEnumerable<T>
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return collection;
                }
            }
            
            throw new ArgumentException(
                $"No item in '{paramName}' satisfies the predicate",
                paramName);
        }
    }
}