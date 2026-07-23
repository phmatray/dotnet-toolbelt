using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

// ReSharper disable once CheckNamespace
namespace System.Linq;

/// <summary>
/// Contains extension methods for sequences.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Filters a sequence of values based on a predicate and removes null values.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains non-null elements from the input sequence.</returns>
    [Pure]
    [return: NotNull]
    public static IEnumerable<TSource> WhereNotNull<TSource>(
        [NotNull] this IEnumerable<TSource?> source)
        where TSource : class
    {
        ArgumentNullException.ThrowIfNull(source);
        return source.Where(item => item is not null)!;
    }

    /// <summary>
    /// Projects each element of a sequence into a new form and removes null values.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to invoke a transform function on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains non-null elements from the transformed input sequence.</returns>
    [Pure]
    [return: NotNull]
    public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(
        [NotNull] this IEnumerable<TSource> source,
        [NotNull] Func<TSource, TResult?> selector)
        where TResult : class
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);
        return source.Select(selector).WhereNotNull();
    }
}