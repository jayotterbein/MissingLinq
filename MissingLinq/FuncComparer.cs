using System;
using System.Collections.Generic;
using System.Linq;

namespace MissingLinq
{
    public class FuncComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> _compareFunc;

        public FuncComparer(Func<T, T, int> compareFunc)
        {
            _compareFunc = compareFunc;
        }

        public FuncComparer(params Func<T, T, int>[] compareFuncs)
        {
            var allComparers = compareFuncs
                .Select(x => new FuncComparer<T>(x))
                .ToArray<IComparer<T>>();
            var chainComparer = new ChainedComparer<T>(allComparers);
            _compareFunc = chainComparer.Compare;
        }

        public int Compare(T x, T y)
        {
            return _compareFunc(x, y);
        }
    }
}