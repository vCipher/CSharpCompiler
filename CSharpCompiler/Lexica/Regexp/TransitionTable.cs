using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpCompiler.Lexica.Regexp
{
    [Serializable]
    public sealed class TransitionTable : IEquatable<TransitionTable>
    {
        public const int UNKNOWN_STATE = -1;
        
        private static readonly Lazy<TransitionTable> _default = new Lazy<TransitionTable>(() => GetDefaultTransitionTable());
        public static TransitionTable Default
        {
            get { return _default.Value; }
        }


        public int Head { get; private set; }

        public int[] Accepting { get; private set; }

        public Dictionary<int, string> Aliases { get; private set; }

        public Dictionary<int, Dictionary<char, int>> Transitions { get; private set; }


        public int this[int id, char ch]
        {
            get
            {
                Dictionary<char, int> states;
                if (!Transitions.TryGetValue(id, out states))
                    return UNKNOWN_STATE;

                int state;
                if (!states.TryGetValue(ch, out state))
                    return UNKNOWN_STATE;

                return state;
            }
        }


        public TransitionTable(int head, Dictionary<int, Dictionary<char, int>> transitions, Dictionary<int, string> aliases)
        {
            Head = head;
            Transitions = transitions;
            Aliases = aliases;
            Accepting = aliases.Keys.ToArray();
        }

        public static TransitionTable FromFile(string path)
        {
            using (var fReader = new StreamReader(path))
            using (var tReader = new TransitionTableReader(fReader))
                return tReader.Read();
        }

        public static TransitionTable FromString(string content)
        {
            using (var sReader = new StringReader(content))
            using (var tReader = new TransitionTableReader(sReader))
                return tReader.Read();
        }

        private static TransitionTable GetSourceTransitionTable()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream("CSharpCompiler.vocabulary.txt"))
                return (TransitionTable)new BinaryFormatter().Deserialize(stream);
        }

        private static TransitionTable GetDefaultTransitionTable()
        {
            var assembly = Assembly.GetExecutingAssembly();

#if FROM_SOURCE
            using (var stream = assembly.GetManifestResourceStream("CSharpCompiler.vocabulary.txt"))
            using (var reader = new StreamReader(stream))
            using (var tReader = new TransitionTableReader(reader))
                return tReader.Read();
#else
            using (var stream = assembly.GetManifestResourceStream("CSharpCompiler.vocabulary.bin"))
                return (TransitionTable)new BinaryFormatter().Deserialize(stream);
#endif
        }

        public bool IsAcceptingState(int state)
        {
            return Array.BinarySearch(Accepting, state) >= 0;
        }

        public string GetTokenAlias(int state)
        {
            return Aliases[state];
        }

        public bool Equals(TransitionTable other)
        {
            return Head == other.Head &&
                Equals(Accepting, other.Accepting) &&
                Equals(Aliases, other.Aliases) &&
                Equals(Transitions, other.Transitions);
        }

        private bool Equals(int[] @this, int[] other)
        {
            return @this.SequenceEqual(other);
        }

        private bool Equals(Dictionary<int, string> @this, Dictionary<int, string> other)
        {
            return new DictionaryComparer<int, string>()
                .Equals(@this, other);
        }

        private bool Equals(Dictionary<int, Dictionary<char, int>> @this, Dictionary<int, Dictionary<char, int>> other)
        {
            return new DictionaryComparer<int, Dictionary<char, int>>(new DictionaryComparer<char, int>())
                .Equals(@this, other);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TransitionTable))
                return false;

            return Equals((TransitionTable)obj);
        }

        public override int GetHashCode()
        {
            int res = 31;
            res ^= Head.GetHashCode();
            res ^= Accepting.GetHashCode();
            res ^= Aliases.GetHashCode();
            res ^= Transitions.GetHashCode();
            return res;
        }
    }
}
