using System;
using System.Collections.Generic;
using System.Linq;

namespace MissingLinq
{
    public static class ListExtensions
    {
        public static IEnumerable<T> GetAndRemove<T>(this IList<T> list, Predicate<T> predicate)
        {
            return GetAndRemove(list, (x, i) => predicate(x));
        }

        public static IEnumerable<T> GetAndRemove<T>(this IList<T> list, Func<T, int, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (list.IsReadOnly)
                throw new ArgumentException("Unable to modify a read only list.", "list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var indecies = new List<int>(list.Count);
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item, i))
                {
                    indecies.Add(i);
                    yield return item;
                }
            }

            foreach (var i in Enumerable.Reverse(indecies))
            {
                list.RemoveAt(i);
            }
        }

        public static void RemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            RemoveAll(list, (x, i) => predicate(x));
        }

        public static void RemoveAll<T>(this IList<T> list, Func<T, int, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i], i))
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}