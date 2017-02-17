using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class TransitionTableReader : IDisposable
    {
        private TextReader _reader;

        public TransitionTableReader(TextReader reader)
        {
            _reader = reader;
        }

        public TransitionTable Read()
        {
            Nfa nfa = ReadLines()
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .Where(line => !line.StartsWith("#"))
                .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(parts => CreateNFA(parts[0].Trim(), parts[1].Trim()))
                .Aggregate((fst, snd) => Nfa.Builder.Union(fst, snd));

            Dfa dfa = Dfa.Builder.CovertFrom(nfa);
            HashSet<DfaState> states = DfaBuilder.GetStates(dfa);

            Dictionary<int, Dictionary<char, int>> table = states
                .OrderBy(state => state.Id)
                .ToDictionary(
                    state => state.Id, 
                    state => state.Transitions
                        .ToDictionary(trans => trans.Character, trans => trans.To.Id)
                );

            Dictionary<int, string> aliases = states.Where(state => state.IsAccepting)
                .OrderBy(state => state.Id)
                .ToDictionary(state => state.Id, state => state.Alias);

            return new TransitionTable(dfa.Head.Id, table, aliases);
        }

        private IEnumerable<string> ReadLines()
        {
            for (string line = _reader.ReadLine(); line != null; line = _reader.ReadLine())
                yield return line;
        }

        private Nfa CreateNFA(string regexp, string alias)
        {
            Nfa nfa = Nfa.Builder.Parse(regexp);
            nfa.Tail.IsAccepting = true;
            nfa.Tail.Alias = alias;
            return nfa;
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

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
