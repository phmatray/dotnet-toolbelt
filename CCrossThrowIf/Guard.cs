using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace CCrossThrowIf;

/// <summary>
/// Modern guard clause library that returns validated values
/// </summary>
public static partial class Guard
{
    public static class Against
    {
        #region String Guards

        /// <summary>
        /// Ensures the string is not null or whitespace and returns it.
        /// </summary>
        [return: NotNull]
        public static string NullOrWhiteSpace(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(
                    paramName,
                    message ?? $"Value cannot be null or whitespace. (Parameter '{paramName}')");
            }

            return value;
        }

        /// <summary>
        /// Ensures the string is not null or empty and returns it.
        /// </summary>
        [return: NotNull]
        public static string NullOrEmpty(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(
                    paramName,
                    message ?? $"Value cannot be null or empty. (Parameter '{paramName}')");
            }

            return value;
        }

        #endregion

        #region Null Guards

        /// <summary>
        /// Ensures the value is not null and returns it.
        /// </summary>
        [return: NotNull]
        public static T Null<T>(
            [NotNull] T? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(
                    paramName,
                    message ?? $"Value cannot be null. (Parameter '{paramName}')");
            }

            return value;
        }

        /// <summary>
        /// Ensures the nullable value is not null and returns the underlying value.
        /// </summary>
        public static T Null<T>(
            T? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : struct
        {
            if (!value.HasValue)
            {
                throw new ArgumentNullException(
                    paramName,
                    message ?? $"Value cannot be null. (Parameter '{paramName}')");
            }

            return value.Value;
        }

        #endregion

        #region Numeric Guards

        /// <summary>
        /// Ensures the value is not negative and returns it.
        /// </summary>
        public static T Negative<T>(
            T value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : INumber<T>
        {
            if (value < T.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    message ?? $"Value cannot be negative. (Parameter '{paramName}')");
            }

            return value;
        }

        /// <summary>
        /// Ensures the value is not zero and returns it.
        /// </summary>
        public static T Zero<T>(
            T value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : INumber<T>
        {
            if (value == T.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    message ?? $"Value cannot be zero. (Parameter '{paramName}')");
            }

            return value;
        }

        /// <summary>
        /// Ensures the value is not negative or zero and returns it.
        /// </summary>
        public static T NegativeOrZero<T>(
            T value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : INumber<T>
        {
            if (value <= T.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    message ?? $"Value must be positive. (Parameter '{paramName}')");
            }

            return value;
        }

        #endregion

        #region Range Guards

        /// <summary>
        /// Ensures the value is not outside the specified range and returns it.
        /// </summary>
        public static T OutOfRange<T>(
            T value,
            T minimum,
            T maximum,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : IComparable<T>
        {
            if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    message ?? $"Value must be between {minimum} and {maximum}. (Parameter '{paramName}')");
            }

            return value;
        }

        #endregion

        #region DateTime Guards

        /// <summary>
        /// Ensures the DateTime is not in the past and returns it.
        /// </summary>
        public static DateTime PastDateTime(
            DateTime value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (value < DateTime.Now)
            {
                throw new ArgumentException(
                    message ?? $"DateTime cannot be in the past. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        /// <summary>
        /// Ensures the DateTime is not in the future and returns it.
        /// </summary>
        public static DateTime FutureDateTime(
            DateTime value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException(
                    message ?? $"DateTime cannot be in the future. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        #endregion

        #region TimeSpan Guards

        /// <summary>
        /// Ensures the TimeSpan is not negative or zero and returns it.
        /// </summary>
        public static TimeSpan NegativeOrZeroTimeSpan(
            TimeSpan value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (value <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    message ?? $"TimeSpan must be positive. (Parameter '{paramName}')");
            }

            return value;
        }

        #endregion

        #region Collection Guards

        /// <summary>
        /// Ensures the collection is not null or empty and returns it.
        /// </summary>
        [return: NotNull]
        public static T NullOrEmpty<T>(
            [NotNull] T? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null) where T : System.Collections.IEnumerable
        {
            if (value is null)
            {
                throw new ArgumentNullException(
                    paramName,
                    message ?? $"Collection cannot be null. (Parameter '{paramName}')");
            }

            var enumerator = value.GetEnumerator();
            var hasItems = enumerator.MoveNext();
            
            if (enumerator is IDisposable disposable)
            {
                disposable.Dispose();
            }

            if (!hasItems)
            {
                throw new ArgumentException(
                    message ?? $"Collection cannot be empty. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        #endregion

        #region Boolean Guards

        /// <summary>
        /// Ensures the value is not true and returns it.
        /// </summary>
        public static bool True(
            bool value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (value)
            {
                throw new ArgumentException(
                    message ?? $"Value cannot be true. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        /// <summary>
        /// Ensures the value is not false and returns it.
        /// </summary>
        public static bool False(
            bool value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (!value)
            {
                throw new ArgumentException(
                    message ?? $"Value cannot be false. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        #endregion

        #region Equality Guards

        /// <summary>
        /// Ensures the value is not equal to the test value and returns it.
        /// </summary>
        public static T EqualTo<T>(
            T value,
            T testValue,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (EqualityComparer<T>.Default.Equals(value, testValue))
            {
                throw new ArgumentException(
                    message ?? $"Value cannot be equal to {testValue}. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        /// <summary>
        /// Ensures the value is not the default value for its type and returns it.
        /// </summary>
        public static T Default<T>(
            T value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            if (EqualityComparer<T>.Default.Equals(value, default!))
            {
                throw new ArgumentException(
                    message ?? $"Value cannot be the default value. (Parameter '{paramName}')",
                    paramName);
            }

            return value;
        }

        #endregion
    }
}

/// <summary>
/// Extension methods for fluent guard clauses
/// </summary>
public static class GuardExtensions
{
    /// <summary>
    /// Fluent guard against null values
    /// </summary>
    [return: NotNull]
    public static T GuardNull<T>(
        [NotNull] this T? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null,
        string? message = null) where T : class
        => Guard.Against.Null(value, paramName, message);

    /// <summary>
    /// Fluent guard against null or empty strings
    /// </summary>
    [return: NotNull]
    public static string GuardNullOrEmpty(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null,
        string? message = null)
        => Guard.Against.NullOrEmpty(value, paramName, message);

    /// <summary>
    /// Fluent guard against null or whitespace strings
    /// </summary>
    [return: NotNull]
    public static string GuardNullOrWhiteSpace(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null,
        string? message = null)
        => Guard.Against.NullOrWhiteSpace(value, paramName, message);
}