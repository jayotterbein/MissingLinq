using System;
using System.Collections.Generic;

namespace MissingLinq
{
    public class FuncEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _compareFunc;

        public FuncEqualityComparer(Func<T, T, bool> compareFunc)
        {
            _compareFunc = compareFunc;
        }

        public bool Equals(T x, T y)
        {
            return _compareFunc(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}