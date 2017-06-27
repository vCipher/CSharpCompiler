using CSharpCompiler.Lexica.Regexp;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class TransitionTableComparer : IEqualityComparer<TransitionTable>
    {
        public static readonly TransitionTableComparer Default = new TransitionTableComparer();

        private DictionaryComparer<ushort, string> _aliasesComparer;
        private DictionaryComparer<char, ushort> _transComparer;

        private TransitionTableComparer()
        {
            _aliasesComparer = new DictionaryComparer<ushort, string>();
            _transComparer = new DictionaryComparer<char, ushort>();
        }

        public bool Equals(TransitionTable first, TransitionTable second)
        {
            return first.Head == second.Head &&
                Equals(first.Accepting, second.Accepting) &&
                Equals(first.Aliases, second.Aliases) &&
                Equals(first.Transitions, second.Transitions);
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

        private bool Equals(ushort[] first, ushort[] second)
        {
            return first.SequenceEqual(second);
        }

        private bool Equals(Dictionary<ushort, string> first, Dictionary<ushort, string> second)
        {
            return _aliasesComparer.Equals(first, second);
        }

        private bool Equals(Dictionary<char, ushort>[] first, Dictionary<char, ushort>[] second)
        {
            return first.SequenceEqual(second, _transComparer);
        }

        private int GetHashCode(Dictionary<ushort, string> aliases)
        {
            return _aliasesComparer.GetHashCode(aliases);
        }

        private int GetHashCode(Dictionary<char, ushort>[] trans)
        {
            if (trans == null)
                return 0;

            unchecked
            {
                int hash = 1;
                foreach (var item in trans)
                    hash = (hash * 37) ^ _transComparer.GetHashCode(item);

                return hash;
            }
        }
    }
}
