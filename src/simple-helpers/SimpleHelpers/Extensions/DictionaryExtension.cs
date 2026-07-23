/* Author : 
 * Philippe Matray
 * 
 * Date : 
 * 2017-01-25
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleHelpers.Extensions
{
    public static class DictionaryExtension
    {
        /// <summary>
        ///     Remove all occurrences from a dictionary with a predicate.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="predicate">The condition to remove an entity.</param>
        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            foreach (var item in dictionary.Where(predicate).ToList())
                dictionary.Remove(item.Key);
        }
    }
}