using System;
using System.Collections.Generic;

namespace MissingLinq
{
    public static class ToSortedDictionaryExtensions
    {
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TItem, TKey, TElement>(
            this IEnumerable<TItem> enumerable,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IComparer<TKey> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            var result = new SortedDictionary<TKey, TElement>(comparer);
            foreach (var item in enumerable)
            {
                var key = keySelector(item);
                var value = elementSelector(item);
                result.Add(key, value);
            }
            return result;
        }

        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TItem, TKey, TElement>(
            this IEnumerable<TItem> enumerable,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector)
        {
            return ToSortedDictionary(
                enumerable,
                keySelector,
                elementSelector,
                Comparer<TKey>.Default);
        }

        public static SortedDictionary<TKey, TItem> ToSortedDictionary<TItem, TKey>(
            this IEnumerable<TItem> enumerable,
            Func<TItem, TKey> keySelector)
        {
            return ToSortedDictionary(
                enumerable,
                keySelector,
                Comparer<TKey>.Default);
        }

        public static SortedDictionary<TKey, TItem> ToSortedDictionary<TItem, TKey>(
            this IEnumerable<TItem> enumerable,
            Func<TItem, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return ToSortedDictionary(
                enumerable,
                keySelector,
                x => x,
                comparer);
        }
    }
}