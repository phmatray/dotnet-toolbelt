using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

public static partial class Guard
{
    /// <summary>
    /// Performance-focused validations with minimal overhead
    /// </summary>
    public static class Performance
    {
        /// <summary>
        /// Aggressive inlining for hot paths - validates not null with minimal overhead
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNull]
        public static T NotNullFast<T>([NotNull] T? value) where T : class
        {
            if (value is null)
                ThrowHelper.ThrowArgumentNullException();
            
            return value;
        }

        /// <summary>
        /// Validates a value is within range using bit manipulation for powers of 2
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint PowerOfTwo(
            uint value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value == 0 || (value & (value - 1)) != 0)
            {
                throw new ArgumentException(
                    $"Value '{paramName}' must be a power of 2",
                    paramName);
            }
            
            return value;
        }

        /// <summary>
        /// Batch validation with early exit
        /// </summary>
        public static void BatchValidate(params Action[] validations)
        {
            // Pre-allocate exception list to avoid allocations in hot path
            List<Exception>? exceptions = null;
            
            foreach (var validation in validations)
            {
                try
                {
                    validation();
                }
                catch (Exception ex)
                {
                    exceptions ??= new List<Exception>(validations.Length);
                    exceptions.Add(ex);
                }
            }
            
            if (exceptions?.Count > 0)
            {
                throw new AggregateException(
                    "One or more validations failed",
                    exceptions);
            }
        }

        /// <summary>
        /// Validates with minimal allocations using value tuples
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool isValid, string? error) TryValidate<T>(
            T value,
            Predicate<T> predicate,
            string? errorMessage = null)
        {
            return predicate(value) 
                ? (true, null) 
                : (false, errorMessage ?? "Validation failed");
        }

        /// <summary>
        /// High-performance string validation without regex
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmailFast(ReadOnlySpan<char> email)
        {
            if (email.IsEmpty) return false;
            
            var atIndex = email.IndexOf('@');
            if (atIndex <= 0 || atIndex >= email.Length - 1)
                return false;
            
            var dotIndex = email.Slice(atIndex + 1).IndexOf('.');
            return dotIndex > 0 && dotIndex < email.Length - atIndex - 2;
        }

        /// <summary>
        /// Stack-allocated validation for small collections
        /// </summary>
        public static void ValidateAll<T>(
            ReadOnlySpan<T> values,
            Predicate<T> predicate)
        {
            const int MaxStackErrors = 16;
            Span<int> errorIndices = stackalloc int[MaxStackErrors];
            var errorCount = 0;
            
            for (int i = 0; i < values.Length && errorCount < MaxStackErrors; i++)
            {
                if (!predicate(values[i]))
                {
                    errorIndices[errorCount++] = i;
                }
            }
            
            if (errorCount > 0)
            {
                var indices = errorCount == 1 
                    ? $"index {errorIndices[0]}"
                    : $"indices {string.Join(", ", errorIndices.Slice(0, errorCount).ToArray())}";
                    
                throw new ArgumentException(
                    $"Validation failed at {indices}");
            }
        }
    }

    /// <summary>
    /// Helper class for throwing exceptions without capturing stack frames in hot paths
    /// </summary>
    internal static class ThrowHelper
    {
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowArgumentNullException()
        {
            throw new ArgumentNullException();
        }

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowArgumentException(string message)
        {
            throw new ArgumentException(message);
        }
    }
}