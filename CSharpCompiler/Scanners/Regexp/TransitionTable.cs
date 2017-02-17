using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class TransitionTable
    {
        private static Lazy<TransitionTable> _default = new Lazy<TransitionTable>(() => GetDefaultTransitionTable());
        public static TransitionTable Default
        {
            get { return _default.Value; }
        }

        private Dictionary<int, Dictionary<char, int>> _table;
        private Dictionary<int, string> _aliases;
        private int[] _accepting;

        public int Head { get; private set; }

        public int this[int id, char ch]
        {
            get
            {
                Dictionary<char, int> transitions;
                if (!_table.TryGetValue(id, out transitions))
                    return -1;

                int state;
                if (!transitions.TryGetValue(ch, out state))
                    return -1;

                return state;
            }
        }

        public TransitionTable(int head, Dictionary<int, Dictionary<char, int>> table, Dictionary<int, string> aliases)
        {
            Head = head;
            _table = table;
            _aliases = aliases;
            _accepting = aliases.Keys.ToArray();
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

        private static TransitionTable GetDefaultTransitionTable()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream("CSharpCompiler.vocabulary.txt"))
            using (var fReader = new StreamReader(stream))
            using (var tReader = new TransitionTableReader(fReader))
                return tReader.Read();
        }

        public bool IsAcceptingState(int state)
        {
            return Array.BinarySearch(_accepting, state) >= 0;
        }

        public string GetTokenAlias(int state)
        {
            return _aliases[state];
        }
    }
}
