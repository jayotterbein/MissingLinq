using System;

namespace MissingLinq
{
    public static class FuncExtensions
    {
        public static Predicate<T> ToPredicate<T>(this Func<T, bool> predicateFunc)
        {
            return x => predicateFunc(x);
        }
    }
}