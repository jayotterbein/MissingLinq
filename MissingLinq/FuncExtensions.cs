using System;

namespace MissingLinq
{
    public static class FuncExtensions
    {
        public static Func<T, bool> ToFunc<T>(this Predicate<T> predicate)
        {
            return x => predicate(x);
        }
    }
}