using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MissingLinq
{
    public static class RandomSelectionExtensions
    {
        private static readonly ThreadLocal<Random> ThreadRandom;

        static RandomSelectionExtensions()
        {
            ThreadRandom = new ThreadLocal<Random>(() => new Random());
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            return Random(enumerable, ThreadRandom.Value, null, false);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, Random random)
        {
            return Random(enumerable, random, null, false);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, Random random, Func<T, bool> predicate)
        {
            return Random(enumerable, random, predicate, false);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return Random(enumerable, ThreadRandom.Value, predicate, false);
        }

        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable)
        {
            return Random(enumerable, ThreadRandom.Value, null, true);
        }

        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Random random)
        {
            return Random(enumerable, random, null, true);
        }

        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Random random, Func<T, bool> predicate)
        {
            return Random(enumerable, random, predicate, true);
        }
        
        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return Random(enumerable, ThreadRandom.Value, predicate, true);
        }

        public static IList<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            var list = enumerable.ToList();
            list.Sort((x, y) => ThreadRandom.Value.Next());
            return list;
        }

        private static T Random<T>(IEnumerable<T> enumerable, Random random, bool useDefaultIfNotFound)
        {
            return Random(enumerable, random, null, useDefaultIfNotFound);
        }

        private static T Random<T>(
            IEnumerable<T> enumerable,
            Random random,
            Func<T, bool> predicate,
            bool useDefaultIfNotFound)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (random == null)
                throw new ArgumentNullException("random");

            var sequence = (predicate == null) ? enumerable : enumerable.Where(predicate);
            var current = default(T);
            var count = 0;
            foreach (var item in sequence)
            {
                count++;
                if (random.Next(count) == 0)
                    current = item;
            }
            if (count == 0 && !useDefaultIfNotFound)
                throw new ArgumentException("Cannot obtain random element from empty sequence.", "enumerable");
            return current;
        }
    }
}