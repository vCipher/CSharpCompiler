using CSharpCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class DfaBuilder
    {
        private DfaStateFactory _stateFactory;

        public DfaBuilder()
        {
            _stateFactory = new DfaStateFactory();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="builder">Source DFA builder</param>
        public DfaBuilder(DfaBuilder builder)
        {
            _stateFactory = new DfaStateFactory(builder._stateFactory);
        }

        public Dfa CovertFrom(Nfa nfa)
        {
            return new Coverter(nfa, _stateFactory).Covert();
        }

        public static HashSet<NfaState> GetStates(Nfa nfa)
        {
            HashSet<NfaState> states = new HashSet<NfaState>();
            HashSet<NfaState> visited = new HashSet<NfaState>();
            SeekGraph(nfa.Head, states, visited);
            return states;
        }

        public static HashSet<DfaState> GetStates(Dfa nfa)
        {
            HashSet<DfaState> states = new HashSet<DfaState>();
            HashSet<DfaState> visited = new HashSet<DfaState>();
            SeekGraph(nfa.Head, states, visited);
            return states;
        }

        public static Transition CreateTransition(DfaState from, DfaState to, char character)
        {
            Transition transition = new Transition(from, to, character);
            from.Transitions.Add(transition);
            return transition;
        }

        private static void SeekGraph<TState>(TState head, ISet<TState> states, ISet<TState> visited, Predicate<Transition> predicate = null)
            where TState : IState
        {
            states.Add(head);
            visited.Add(head);

            head.Transitions
                .Where(trans => !visited.Contains((TState)trans.To))
                .Where(trans => predicate.With(p => p(trans), true))
                .ForEach(trans => SeekGraph((TState)trans.To, states, visited, predicate));
        }

        private sealed class Coverter
        {
            private Nfa _nfa;
            private HashSet<NfaState> _states;
            private DfaStateFactory _stateFactory;
            private Dictionary<NfaState, HashSet<NfaState>> _epsilonClosures;

            private HashSet<DfaState> _visited;
            private Queue<DfaState> _queue;
            private HashSet<char> _alphabet;

            public Coverter(Nfa nfa, DfaStateFactory stateFactory)
            {
                _nfa = nfa;
                _stateFactory = stateFactory;
                _states = GetStates(nfa);
                _alphabet = GetAlphabet(_states);
                _epsilonClosures = _states
                    .ToDictionary(state => state, state => GetEpsilonClosure(state));

                _visited = new HashSet<DfaState>();
                _queue = new Queue<DfaState>();                
            }

            public Dfa Covert()
            {
                DfaState head = GetEpsilonClosures(new[] { _nfa.Head });
                _visited.Add(head);
                _queue.Enqueue(head);

                while (_queue.Any())
                {
                    DfaState from = _queue.Dequeue();
                    foreach (char character in _alphabet)
                    {
                        DfaState to = GetEpsilonClosures(Delta(from, character));

                        if (to.NfaStates.IsNullOrEmpty())
                            continue;

                        CreateTransition(from, to, character);

                        if (_visited.Add(to))
                        {
                            _queue.Enqueue(to);
                            CheckForAcceptance(to);
                        }   
                    }
                }

                return new Dfa(head);
            }

            private void CheckForAcceptance(DfaState dfaState)
            {
                foreach (var nfaState in dfaState.NfaStates)
                {
                    if (nfaState.IsAccepting)
                    {
                        dfaState.IsAccepting = true;
                        dfaState.Alias = nfaState.Alias;
                        return;
                    }
                }
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
                return GetOrCreateState(
                    states.SelectMany(state => _epsilonClosures[state])
                );
            }

            private DfaState GetEpsilonClosures(DfaState state)
            {
                return GetEpsilonClosures(state.NfaStates);
            }

            private HashSet<NfaState> GetEpsilonClosure(NfaState state)
            {
                HashSet<NfaState> states = new HashSet<NfaState>();
                HashSet<NfaState> visited = new HashSet<NfaState>();
                SeekGraph(state, states, visited, trans => trans.IsEpsilon);
                return states;
            }

            private IEnumerable<NfaState> Delta(DfaState state, char character)
            {
                return state.NfaStates
                        .SelectMany(nfaState => nfaState.Transitions)
                        .Where(trans => trans.Character == character)
                        .Select(trans => trans.To)
                        .Cast<NfaState>().ToHashSet();
            }

            private DfaState GetOrCreateState(IEnumerable<NfaState> nfaStates)
            {
                if (nfaStates.IsNullOrEmpty())
                    return _stateFactory.CreateEmpty();

                foreach (var dfaState in _visited)
                {
                    if (dfaState.NfaStates.SetEquals(nfaStates))
                        return dfaState;
                }

                return _stateFactory.Create(nfaStates);
            }

        }
    }
}
