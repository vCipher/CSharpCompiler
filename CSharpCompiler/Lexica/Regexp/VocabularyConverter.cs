using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class VocabularyConverter : IDisposable
    {
        private TextReader _reader;

        public VocabularyConverter(Stream stream)
        {
            _reader = new StreamReader(stream);
        }

        public VocabularyConverter(string content)
        {
            _reader = new StringReader(content);
        }

        public TransitionTable Convert()
        {
            Nfa nfa = CreateNfa();
            Dfa dfa = Dfa.FromNfa(nfa);

            var states = dfa.GetStates();
            var transitions = GetTransitions(states);
            var aliases = GetAliases(states);

            return new TransitionTable((ushort)dfa.Head.Id, transitions, aliases);
        }

        private static Dictionary<ushort, string> GetAliases(HashSet<DfaState> states)
        {
            return states.Where(state => state.IsAccepting)
                .OrderBy(state => state.Id)
                .ToDictionary(state => (ushort)state.Id, state => state.Alias);
        }

        private static Dictionary<char, ushort>[] GetTransitions(HashSet<DfaState> states)
        {
            var result = new Dictionary<char, ushort>[states.Count];

            foreach (var state in states.OrderBy(state => state.Id))
            {
                result[state.Id] = state.Transitions.ToDictionary(
                        trans => trans.Character,
                        trans => (ushort)trans.To.Id);
            }

            return result;
        }

        private IEnumerable<string> ReadLines()
        {
            for (string line = _reader.ReadLine(); line != null; line = _reader.ReadLine())
            {
                if (!string.IsNullOrWhiteSpace(line))
                    yield return line.Trim();
            }
        }

        private Nfa CreateNfa()
        {
            return ReadLines()
                .Where(line => !IsComment(line))
                .Aggregate<string, Nfa>(null, (nfa, line) => Union(nfa, line));
        }

        private bool IsComment(string line)
        {
            return !string.IsNullOrWhiteSpace(line) && line.StartsWith("#");
        }

        private Nfa Union(Nfa nfa, string line)
        {
            return (nfa == null)
                ? CreateNfa(line)
                : nfa.Union(builder => CreateNfa(builder, line));
        }

        private Nfa CreateNfa(NfaBuilder builder, string line)
        {
            return ParseLine(line, (regexp, alias) => CreateNfa(builder, regexp, alias));
        }

        private Nfa CreateNfa(string line)
        {
            return ParseLine(line, (regexp, alias) => CreateNfa(regexp, alias));
        }

        private Nfa CreateNfa(string regexp, string alias)
        {
            Nfa nfa = Nfa.Parse(regexp);
            nfa.Tail.IsAccepting = true;
            nfa.Tail.Alias = alias;
            return nfa;
        }

        private Nfa CreateNfa(NfaBuilder builder, string regexp, string alias)
        {
            Nfa nfa = builder.Parse(regexp);
            nfa.Tail.IsAccepting = true;
            nfa.Tail.Alias = alias;
            return nfa;
        }

        private T ParseLine<T>(string line, Func<string, string, T> func)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentNullException("line");

            const int regexpIndex = 0;
            const int aliasIndex = 1;

            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                throw new ArgumentException(string.Format("Line: \"{0}\" has invalid format", line), "line");

            string regexp = parts[regexpIndex].Trim();
            string alias = parts[aliasIndex].Trim();
            return func(regexp, alias);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _reader.Dispose();
                }

                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
