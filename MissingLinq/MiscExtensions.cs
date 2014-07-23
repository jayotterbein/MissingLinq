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

        public static bool None<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return enumerable == null || !(enumerable.Any(x => predicate(x)));
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var itemIndexPair = enumerable.Select((x, i) => new { Item = x, Index = i }).FirstOrDefault(pair => predicate(pair.Item));
            return (itemIndexPair == null) ? -1 : itemIndexPair.Index;
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