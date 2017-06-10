using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CSharpCompiler.Utility
{
    public static class Func
    {
        public static Func<TArg, TRes> Memoize<TArg, TRes>(Func<TArg, TRes> func)
        {
            var cache = new ConcurrentDictionary<TArg, Lazy<TRes>>();
            return arg => cache.GetOrAdd(arg, new Lazy<TRes>(() => func(arg))).Value;
        }

        public static Func<TArg, TRes> Memoize<TArg, TRes>(Func<TArg, TRes> func, IEqualityComparer<TArg> comparer)
        {
            var cache = new ConcurrentDictionary<TArg, Lazy<TRes>>(comparer);
            return arg => cache.GetOrAdd(arg, new Lazy<TRes>(() => func(arg))).Value;
        }
    }
}
