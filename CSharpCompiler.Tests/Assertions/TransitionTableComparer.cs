using CSharpCompiler.Lexica.Regexp;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class TransitionTableComparer : IEqualityComparer<TransitionTable>
    {
        public static readonly TransitionTableComparer Default = new TransitionTableComparer();

        private DictionaryComparer<int, string> _aliasesComparer;
        private DictionaryComparer<int, Dictionary<char, int>> _transComparer;

        private TransitionTableComparer()
        {
            _aliasesComparer = new DictionaryComparer<int, string>();
            _transComparer = new DictionaryComparer<int, Dictionary<char, int>>(new DictionaryComparer<char, int>());
        }

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
            return _aliasesComparer.Equals(first, second);
        }

        private bool Equals(Dictionary<int, Dictionary<char, int>> first, Dictionary<int, Dictionary<char, int>> second)
        {
            return _transComparer.Equals(first, second);
        }

        public int GetHashCode(TransitionTable obj)
        {
            if (obj == null)
                return 0;

            int hash = 17;
            hash = hash * 23 + obj.Head.GetHashCode();
            hash = hash * 23 + obj.Accepting.GetHashCode();
            hash = hash * 23 + GetHashCode(obj.Aliases);
            hash = hash * 23 + GetHashCode(obj.Transitions);
            return hash;
        }

        private int GetHashCode(Dictionary<int, string> aliases)
        {
            return _aliasesComparer.GetHashCode(aliases);
        }

        private int GetHashCode(Dictionary<int, Dictionary<char, int>> trans)
        {
            return _transComparer.GetHashCode(trans);
        }
    }
}
