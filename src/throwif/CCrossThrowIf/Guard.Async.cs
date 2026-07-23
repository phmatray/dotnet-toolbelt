using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

public static partial class Guard
{
    /// <summary>
    /// Async-aware guard clauses for modern async/await patterns
    /// </summary>
    public static class Async
    {
        /// <summary>
        /// Validates a Task is not null and not faulted
        /// </summary>
        [return: NotNull]
        public static async Task<T> NotFaulted<T>(
            [NotNull] Task<T>? task,
            [CallerArgumentExpression(nameof(task))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(task, paramName);
            
            try
            {
                return await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Task '{paramName}' faulted with exception: {ex.Message}",
                    paramName,
                    ex);
            }
        }

        /// <summary>
        /// Validates a Task completes within a timeout
        /// </summary>
        [return: NotNull]
        public static async Task<T> CompletesWithin<T>(
            [NotNull] Task<T>? task,
            TimeSpan timeout,
            [CallerArgumentExpression(nameof(task))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(task, paramName);
            
            using var cts = new CancellationTokenSource(timeout);
            try
            {
                return await task.WaitAsync(cts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException(
                    $"Task '{paramName}' did not complete within {timeout}");
            }
        }

        /// <summary>
        /// Validates an async enumerable is not null and not empty
        /// </summary>
        [return: NotNull]
        public static async IAsyncEnumerable<T> NotEmpty<T>(
            [NotNull] IAsyncEnumerable<T>? source,
            [CallerArgumentExpression(nameof(source))] string? paramName = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source, paramName);
            
            var hasItems = false;
            await foreach (var item in source.WithCancellation(cancellationToken))
            {
                hasItems = true;
                yield return item;
            }
            
            if (!hasItems)
            {
                throw new ArgumentException(
                    $"Async enumerable '{paramName}' cannot be empty",
                    paramName);
            }
        }

        /// <summary>
        /// Validates all items in an async enumerable satisfy a predicate
        /// </summary>
        [return: NotNull]
        public static async IAsyncEnumerable<T> All<T>(
            [NotNull] IAsyncEnumerable<T>? source,
            Func<T, bool> predicate,
            [CallerArgumentExpression(nameof(source))] string? paramName = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source, paramName);
            
            var index = 0;
            await foreach (var item in source.WithCancellation(cancellationToken))
            {
                if (!predicate(item))
                {
                    throw new ArgumentException(
                        $"Item at index {index} in async enumerable '{paramName}' does not satisfy the predicate",
                        paramName);
                }
                index++;
                yield return item;
            }
        }
    }
}