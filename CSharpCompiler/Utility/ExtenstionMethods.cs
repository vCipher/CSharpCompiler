using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Utility
{
    internal static class ExtenstionMethods
    {
        internal static TRes Pipe<TSrc, TRes>(this TSrc src, Func<TSrc, TRes> func)
        {
            return (src == null) ? default(TRes) : func(src);
        }

        internal static IEnumerable<TSrc> EmptyIfNull<TSrc>(this IEnumerable<TSrc> src)
        {
            return (src == null) ? Enumerable.Empty<TSrc>() : src;
        }

        internal static TSrc[] EmptyIfNull<TSrc>(this TSrc[] src)
        {
            return (src == null) ? Empty<TSrc>.Array : src;
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
            if (src == null) return;

            foreach (var item in src)
                action?.Invoke(item);
        }

        internal static void ForEach<TSrc>(this IEnumerable<TSrc> src, Action<TSrc, EnumerationInfo> action)
        {
            if (src == null || action == null) return;

            IEnumerator<TSrc> enumerator = src.GetEnumerator();
            bool isFirst = true;
            bool hasNext = enumerator.MoveNext();

            for (int index = 0; hasNext; index++)
            {
                TSrc current = enumerator.Current;
                hasNext = enumerator.MoveNext();
                action(current, new EnumerationInfo(index, isFirst, !hasNext));
                isFirst = false;
            }
        }

        internal static HashSet<TSrc> ToHashSet<TSrc>(this IEnumerable<TSrc> src)
        {
            return new HashSet<TSrc>(src);
        }

        internal static Collection<TSrc> ToCollection<TSrc>(this IEnumerable<TSrc> src)
        {
            return new Collection<TSrc>(src.ToList());
        }

        internal static List<TSrc> ToSingletonList<TSrc>(this TSrc src)
        {
            return new List<TSrc> { src };
        }
    }
}
