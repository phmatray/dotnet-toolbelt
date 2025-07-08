using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

public static partial class Guard
{
    /// <summary>
    /// High-performance validation for Span and Memory types
    /// </summary>
    public static class Span
    {
        /// <summary>
        /// Ensures a span is not empty
        /// </summary>
        public static ReadOnlySpan<T> NotEmpty<T>(
            ReadOnlySpan<T> span,
            [CallerArgumentExpression(nameof(span))] string? paramName = null)
        {
            if (span.IsEmpty)
            {
                throw new ArgumentException(
                    $"Span '{paramName}' cannot be empty",
                    paramName);
            }
            
            return span;
        }

        /// <summary>
        /// Ensures a span has a specific length
        /// </summary>
        public static ReadOnlySpan<T> Length<T>(
            ReadOnlySpan<T> span,
            int expectedLength,
            [CallerArgumentExpression(nameof(span))] string? paramName = null)
        {
            if (span.Length != expectedLength)
            {
                throw new ArgumentException(
                    $"Span '{paramName}' must have length {expectedLength}, but has length {span.Length}",
                    paramName);
            }
            
            return span;
        }

        /// <summary>
        /// Ensures a span has a minimum length
        /// </summary>
        public static ReadOnlySpan<T> MinLength<T>(
            ReadOnlySpan<T> span,
            int minLength,
            [CallerArgumentExpression(nameof(span))] string? paramName = null)
        {
            if (span.Length < minLength)
            {
                throw new ArgumentException(
                    $"Span '{paramName}' must have at least {minLength} elements, but has {span.Length}",
                    paramName);
            }
            
            return span;
        }

        /// <summary>
        /// Ensures all elements in a span satisfy a condition
        /// </summary>
        public static ReadOnlySpan<T> All<T>(
            ReadOnlySpan<T> span,
            Predicate<T> predicate,
            [CallerArgumentExpression(nameof(span))] string? paramName = null)
        {
            for (int i = 0; i < span.Length; i++)
            {
                if (!predicate(span[i]))
                {
                    throw new ArgumentException(
                        $"Element at index {i} in span '{paramName}' does not satisfy the predicate",
                        paramName);
                }
            }
            
            return span;
        }

        /// <summary>
        /// Ensures a Memory is not empty
        /// </summary>
        public static Memory<T> NotEmpty<T>(
            Memory<T> memory,
            [CallerArgumentExpression(nameof(memory))] string? paramName = null)
        {
            if (memory.IsEmpty)
            {
                throw new ArgumentException(
                    $"Memory '{paramName}' cannot be empty",
                    paramName);
            }
            
            return memory;
        }

        /// <summary>
        /// Validates a span contains no null elements
        /// </summary>
        public static ReadOnlySpan<T> NoNulls<T>(
            ReadOnlySpan<T?> span,
            [CallerArgumentExpression(nameof(span))] string? paramName = null) where T : class
        {
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] is null)
                {
                    throw new ArgumentException(
                        $"Element at index {i} in span '{paramName}' is null",
                        paramName);
                }
            }
            
            return span!;
        }
    }
}