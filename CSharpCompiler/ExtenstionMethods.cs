using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler
{
    internal static class ExtenstionMethods
    {
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
