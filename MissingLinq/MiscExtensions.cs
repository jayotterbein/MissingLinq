using System;
using System.Collections.Generic;
using System.Linq;

namespace MissingLinq
{
    public static class MiscExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable == null || !enumerable.Any());
        }

        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return enumerable == null || !enumerable.Any(predicate);
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var i = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                    return i;
                i++;
            }
            return -1;
        }

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> enumerable, Func<T, T, bool> equalityFunc)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return enumerable.Distinct(new FuncEqualityComparer<T>(equalityFunc));
        }

        public static IEnumerable<T> Pipe<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (action == null)
                throw new ArgumentNullException("action");
            foreach (var item in enumerable)
            {
                action(item);
                yield return item;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (action == null)
                throw new ArgumentNullException("action");
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}