using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler
{
    internal static class ExtenstionMethods
    {
        internal static TRes With<TSrc, TRes>(this TSrc src, Func<TSrc, TRes> func)
        {
            if (src == null)
                return default(TRes);

            return func(src);
        }

        internal static TRes With<TSrc, TRes>(this TSrc src, Func<TSrc, TRes> func, TRes defaultVal)
        {
            if (src == null)
                return defaultVal;

            return func(src);
        }

        internal static void Do<TSrc>(this TSrc src, Action<TSrc> func)
        {
            if (src == null)
                return;

            func(src);
        }

        internal static IEnumerable<TSrc> EmptyIfNull<TSrc>(this IEnumerable<TSrc> src)
        {
            if (src == null)
                return Enumerable.Empty<TSrc>();

            return src;
        }

        internal static bool IsNullOrEmpty<TSrc>(this IEnumerable<TSrc> src)
        {
            return src == null || !src.Any();
        }

        internal static bool IsNullOrEmpty(this IEnumerable src)
        {
            if (src == null)
                return true;

            foreach (var item in src)
                return false;

            return true;
        }

        internal static void ForEach<TSrc>(this IEnumerable<TSrc> src, Action<TSrc> action)
        {
            if (src == null)
                return;

            foreach (var item in src)
                action?.Invoke(item);
        }

        internal static HashSet<TSrc> ToHashSet<TSrc>(this IEnumerable<TSrc> src)
        {
            return new HashSet<TSrc>(src);
        }
    }
}
