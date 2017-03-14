using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Utility
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

        internal static TSrc[] EmptyIfNull<TSrc>(this TSrc[] src)
        {
            if (src == null)
                return Empty<TSrc>.Array;

            return src;
        }

        internal static bool IsNullOrEmpty<TSrc>(this IEnumerable<TSrc> src)
        {
            return src == null || !src.Any();
        }

        internal static bool IsNullOrEmpty(this IEnumerable src)
        {
            return src == null || !src.GetEnumerator().MoveNext();
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

        internal static Collection<TSrc> ToCollection<TSrc>(this IEnumerable<TSrc> src)
        {
            return new Collection<TSrc>(src.ToList());
        }
    }
}
