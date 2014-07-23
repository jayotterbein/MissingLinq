using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MissingLinq
{
    public static class ToCollectionExtensions
    {
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            var readonlyCollectionBuilder = new ReadOnlyCollectionBuilder<T>(enumerable);
            return readonlyCollectionBuilder.ToReadOnlyCollection();
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return new Queue<T>(enumerable);
        }

        public static ConcurrentQueue<T> ToConcurrentQueue<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return new ConcurrentQueue<T>(enumerable);
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return new Stack<T>(enumerable);
        }

        public static ConcurrentStack<T> ToConcurrentStack<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return new ConcurrentStack<T>(enumerable);
        }

        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            return new LinkedList<T>(enumerable);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return ToHashSet(enumerable, EqualityComparer<T>.Default);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> equalityComparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (equalityComparer == null)
                throw new ArgumentNullException("equalityComparer");
            return new HashSet<T>(enumerable, equalityComparer);
        }
    }
}