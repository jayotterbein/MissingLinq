using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MissingLinq
{
    /// <summary>
    /// This class will compare using a list of comparers; once a comparer returns non-zero it will use that as the result.
    /// If all comparers return 0, the result is 0.
    /// </summary>
    public class ChainedComparer<T> : IComparer<T>, IEnumerable<IComparer<T>>
    {
        private readonly IComparer<T>[] _comparers;

        public ChainedComparer(params IComparer<T>[] comparers)
        {
            _comparers = comparers;
        }

        public int Compare(T x, T y)
        {
            return _comparers
                .Select(comparer => comparer.Compare(x, y))
                .FirstOrDefault(c => c != 0);
        }

        public IEnumerator<IComparer<T>> GetEnumerator()
        {
            return ((IEnumerable<IComparer<T>>)_comparers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}