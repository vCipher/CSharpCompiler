using CSharpCompiler.Lexica.Regexp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class TransitionTableComparer : IEqualityComparer<TransitionTable>
    {
        public bool Equals(TransitionTable first, TransitionTable second)
        {
            return first.Head == second.Head &&
                Equals(first.Accepting, second.Accepting) &&
                Equals(first.Aliases, second.Aliases) &&
                Equals(first.Transitions, second.Transitions);
        }

        private bool Equals(int[] first, int[] second)
        {
            return first.SequenceEqual(second);
        }

        private bool Equals(Dictionary<int, string> first, Dictionary<int, string> second)
        {
            return new DictionaryComparer<int, string>()
                .Equals(first, second);
        }

        private bool Equals(Dictionary<int, Dictionary<char, int>> first, Dictionary<int, Dictionary<char, int>> second)
        {
            return new DictionaryComparer<int, Dictionary<char, int>>(new DictionaryComparer<char, int>())
                .Equals(first, second);
        }

        public int GetHashCode(TransitionTable obj)
        {
            if (obj == null)
                return 0;

            int res = 31;
            res ^= obj.Head.GetHashCode();
            res ^= obj.Accepting.GetHashCode();
            res ^= obj.Aliases.GetHashCode();
            res ^= obj.Transitions.GetHashCode();
            return res;
        }
    }
}
