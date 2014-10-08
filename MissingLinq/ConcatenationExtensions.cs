using System;
using System.Collections.Generic;
using System.Linq;

namespace MissingLinq
{
    public static class ConcatenationExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T item)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            foreach (var i in enumerable)
            {
                yield return i;
            }
            yield return item;
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, params T[] items)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            return enumerable.Concat(items);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T item)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            yield return item;
            foreach (var i in enumerable)
            {
                yield return i;
            }
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, params T[] items)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            return items.Concat(enumerable);
        }
    }
}