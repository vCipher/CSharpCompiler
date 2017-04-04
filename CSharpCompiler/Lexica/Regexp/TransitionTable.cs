using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class TransitionTable
    {
        public const int UNKNOWN_STATE = -1;
        
        private static readonly Lazy<TransitionTable> _default = new Lazy<TransitionTable>(TransitionTableSource.GetTransitionTable);
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
            using (var stream = File.OpenRead(path))
            using (var reader = new TransitionTableReader(stream))
                return reader.Read();
        }

        public static TransitionTable FromString(string content)
        {
            using (var reader = new TransitionTableReader(content))
                return reader.Read();
        }

        public bool IsAcceptingState(int state)
        {
            return Array.BinarySearch(Accepting, state) >= 0;
        }

        public string GetTokenAlias(int state)
        {
            return Aliases[state];
        }
    }
}
