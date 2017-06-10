using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Utility
{
    public static class ExtenstionMethods
    {
        public static TRes Pipe<TSrc, TRes>(this TSrc src, Func<TSrc, TRes> func)
        {
            return (src == null) ? default(TRes) : func(src);
        }

        public static string EmptyIfNull(this string src)
        {
            return src ?? string.Empty;
        }

        public static IEnumerable<TSrc> EmptyIfNull<TSrc>(this IEnumerable<TSrc> src)
        {
            return src ?? Enumerable.Empty<TSrc>();
        }

        public static TSrc[] EmptyIfNull<TSrc>(this TSrc[] src)
        {
            return src ?? Empty<TSrc>.Array;
        }

        public static bool IsNullOrEmpty<TSrc>(this IEnumerable<TSrc> src)
        {
            return src == null || !src.Any();
        }

        public static bool IsNullOrEmpty(this IEnumerable src)
        {
            return src == null || !src.GetEnumerator().MoveNext();
        }

        public static void ForEach<TSrc>(this IEnumerable<TSrc> src, Action<TSrc> action)
        {
            if (src == null) return;

            foreach (var item in src)
                action?.Invoke(item);
        }

        public static void ForEach<TSrc>(this IEnumerable<TSrc> src, Action<TSrc, EnumerationInfo> action)
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

        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> factory)
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;

            value = factory();
            dict.Add(key, value);

            return value;
        }

        public static Stack<TSrc> ToStack<TSrc>(this IEnumerable<TSrc> src)
        {
            return new Stack<TSrc>(src);
        }

        public static Stack<TSrc> ToInvertedStack<TSrc>(this IEnumerable<TSrc> src)
        {
            var stack = new Stack<TSrc>();
            foreach (var item in src.Reverse())
            {
                stack.Push(item);
            }

            return stack;
        }

        public static HashSet<TSrc> ToHashSet<TSrc>(this IEnumerable<TSrc> src)
        {
            return new HashSet<TSrc>(src);
        }

        public static Collection<TSrc> ToCollection<TSrc>(this IEnumerable<TSrc> src)
        {
            return new Collection<TSrc>(src.ToList());
        }

        public static Collection<TSrc> ToCollection<TSrc>(this IEnumerable<TSrc> src, int count)
        {
            return new Collection<TSrc>(src.ToList(count));
        }

        public static List<TSrc> ToList<TSrc>(this IEnumerable<TSrc> src, int count)
        {
            var list = new List<TSrc>(count);
            foreach (var item in src)
            {
                list.Add(item);
            }

            return list;
        }

        public static TSrc[] ToArray<TSrc>(this IEnumerable<TSrc> src, int count)
        {
            var array = new TSrc[count];
            var iterator = src.GetEnumerator();
            for (int index = 0; iterator.MoveNext(); index++)
            {
                array[index] = iterator.Current;
            }

            return array;
        }
    }
}
