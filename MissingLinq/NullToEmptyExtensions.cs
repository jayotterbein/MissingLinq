using System.Collections.Generic;
using System.Linq;

namespace MissingLinq
{
    public static class NullToEmptyExtensions
    {
        public static IEnumerable<TItem> NullToEmpty<TItem>(this IEnumerable<TItem> enumreable)
        {
            return enumreable ?? Enumerable.Empty<TItem>();
        }

        public static ICollection<TItem> NullToEmpty<TItem>(this ICollection<TItem> collection)
        {
            return collection ?? new List<TItem>();
        }

        public static IList<TItem> NullToEmpty<TItem>(this IList<TItem> list)
        {
            return list ?? new List<TItem>();
        }
    }
}