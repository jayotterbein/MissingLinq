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
            {
                throw new ArgumentNullException("predicate");
            }
            return enumerable == null || !enumerable.Any(predicate);
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var i = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> enumerable, Func<T, T, bool> equalityFunc)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            return enumerable.Distinct(FuncEqualityComparer.Create(equalityFunc));
        }

        public static IEnumerable<T> Pipe<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            return Pipe(enumerable, (x, i) => action(x));
        }

        public static IEnumerable<T> Pipe<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            var i = 0;
            foreach (var item in enumerable)
            {
                action(item, i);
                yield return item;
                i++;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException("chunkSize", chunkSize, "Can't chunk enumerable into lists of size <= 0.");
            }
            if (enumerable == null)
            {
                yield return new T[0];
            }
            else
            {
                var array = new T[chunkSize];
                var index = 0;
                foreach (var item in enumerable)
                {
                    array[index++] = item;
                    if (index == chunkSize)
                    {
                        yield return array;
                        index = 0;
                        array = new T[chunkSize];
                    }
                }
                if (index > 0)
                {
                    yield return array
                        .Take(index)
                        .ToArray();
                }
            }
        }
    }
}