using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Lexica.Regexp
{
    internal sealed class DfaBuilder
    {
        private int _stateCounter;

        private readonly Nfa _nfa;
        private readonly HashSet<NfaState> _states;
        private readonly HashSet<DfaState> _visited;
        private readonly HashSet<char> _alphabet;
        private readonly Queue<DfaState> _queue;
        private readonly Dictionary<NfaState, HashSet<NfaState>> _epsilonClosures;

        public DfaBuilder(Nfa nfa)
        {
            _stateCounter = 0;
            _nfa = nfa;
            _states = nfa.GetStates();
            _alphabet = GetAlphabet(_states);
            _epsilonClosures = _states
                .ToDictionary(state => state, state => GetEpsilonClosure(state));

            _visited = new HashSet<DfaState>();
            _queue = new Queue<DfaState>();
        }

        public Dfa Build()
        {
            DfaState head = GetEpsilonClosures(new[] { _nfa.Head });
            _visited.Add(head);
            _queue.Enqueue(head);

            while (_queue.Any())
            {
                DfaState from = _queue.Dequeue();
                _alphabet.ForEach(character => BuildState(from, character));
            }

            return new Dfa(head);
        }

        private void BuildState(DfaState from, char character)
        {
            DfaState to = GetEpsilonClosures(Delta(from, character));
            if (to == DfaState.Empty)
                return;

            Transition.Create(from, to, character);
            if (!_visited.Add(to))
                return;

            _queue.Enqueue(to);
            SetAcceptance(to);
        }

        private void SetAcceptance(DfaState dfaState)
        {
            dfaState.NfaStates
                .FirstOrDefault(state => state.IsAccepting)
                .Do(state => SetAcceptance(dfaState, state));
        }

        private void SetAcceptance(DfaState dfaState, NfaState nfaState)
        {
            dfaState.IsAccepting = true;
            dfaState.Alias = nfaState.Alias;
        }

        private HashSet<char> GetAlphabet(IEnumerable<NfaState> states)
        {
            return states.SelectMany(state => state.Transitions)
                .Where(trans => !trans.IsEpsilon)
                .Select(trans => trans.Character)
                .ToHashSet();
        }

        private DfaState GetEpsilonClosures(IEnumerable<NfaState> states)
        {
            IEnumerable<NfaState> closures = states
                .SelectMany(state => _epsilonClosures[state]);

            return GetOrCreateState(closures);
        }

        private DfaState GetEpsilonClosures(DfaState state)
        {
            return GetEpsilonClosures(state.NfaStates);
        }

        private HashSet<NfaState> GetEpsilonClosure(NfaState state)
        {
            return Nfa.GetStates(state, trans => trans.IsEpsilon);
        }

        private IEnumerable<NfaState> Delta(DfaState state, char character)
        {
            return state.NfaStates
                .SelectMany(nfaState => nfaState.Transitions)
                .Where(trans => trans.Character == character)
                .Select(trans => trans.To)
                .ToHashSet();
        }

        private DfaState GetOrCreateState(IEnumerable<NfaState> nfaStates)
        {
            if (nfaStates.IsNullOrEmpty())
                return DfaState.Empty;

            foreach (var dfaState in _visited)
            {
                if (dfaState.NfaStates.SetEquals(nfaStates))
                    return dfaState;
            }

            return CreateState(nfaStates);
        }

        private DfaState CreateState(IEnumerable<NfaState> nfaStates)
        {
            return new DfaState(_stateCounter++, nfaStates);
        }
    }
}
