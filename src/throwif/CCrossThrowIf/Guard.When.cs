using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

public static partial class Guard
{
    /// <summary>
    /// Conditional validation - only validates when a condition is met
    /// </summary>
    public static class When
    {
        /// <summary>
        /// Validates only when a condition is true
        /// </summary>
        public static T Condition<T>(
            T value,
            bool condition,
            Action<T> validation,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (condition)
            {
                try
                {
                    validation(value);
                }
                catch (Exception ex) when (ex is not ArgumentException && ex is not ArgumentNullException)
                {
                    throw new ArgumentException(
                        $"Conditional validation failed for '{paramName}'",
                        paramName,
                        ex);
                }
            }
            
            return value;
        }

        /// <summary>
        /// Validates a value is not null only when another value is provided
        /// </summary>
        [return: NotNullIfNotNull(nameof(dependsOn))]
        public static T? NotNullIf<T, TDependency>(
            T? value,
            TDependency? dependsOn,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            [CallerArgumentExpression(nameof(dependsOn))] string? dependsOnName = null)
            where T : class
        {
            if (dependsOn is not null && value is null)
            {
                throw new ArgumentNullException(
                    paramName,
                    $"Value '{paramName}' cannot be null when '{dependsOnName}' is provided");
            }
            
            return value;
        }

        /// <summary>
        /// Validates based on environment (Debug/Release)
        /// </summary>
        public static T InDebugMode<T>(
            T value,
            Action<T> debugValidation,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
#if DEBUG
            try
            {
                debugValidation(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Debug validation failed for '{paramName}'",
                    paramName,
                    ex);
            }
#endif
            return value;
        }

        /// <summary>
        /// Validates one of multiple conditions must be true
        /// </summary>
        public static T OneOf<T>(
            T value,
            params Predicate<T>[] conditions)
        {
            foreach (var condition in conditions)
            {
                if (condition(value))
                {
                    return value;
                }
            }
            
            throw new ArgumentException(
                "Value does not satisfy any of the specified conditions");
        }

        /// <summary>
        /// Validates mutually exclusive conditions
        /// </summary>
        public static (T1?, T2?) ExclusiveOr<T1, T2>(
            T1? value1,
            T2? value2,
            [CallerArgumentExpression(nameof(value1))] string? param1 = null,
            [CallerArgumentExpression(nameof(value2))] string? param2 = null)
            where T1 : class
            where T2 : class
        {
            if (value1 is not null && value2 is not null)
            {
                throw new ArgumentException(
                    $"Only one of '{param1}' or '{param2}' can be provided, not both");
            }
            
            if (value1 is null && value2 is null)
            {
                throw new ArgumentException(
                    $"Either '{param1}' or '{param2}' must be provided");
            }
            
            return (value1, value2);
        }
    }
}