using System;
using System.Collections.Generic;

namespace MissingLinq
{
    public static class FuncEqualityComparer
    {
        public static IEqualityComparer<T> Create<T>(Func<T, T, bool> func)
        {
            return new FuncEqualityComparerType<T>(func);
        }

        private class FuncEqualityComparerType<T> : IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> _compareFunc;

            public FuncEqualityComparerType(Func<T, T, bool> compareFunc)
            {
                _compareFunc = compareFunc;
            }

            public bool Equals(T x, T y)
            {
                return _compareFunc(x, y);
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }
    }
}