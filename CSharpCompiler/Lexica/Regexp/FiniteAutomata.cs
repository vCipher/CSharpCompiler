using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Lexica.Regexp
{
    public class FiniteAutomata<TState> where TState : ITransitionState<TState>
    {
        public TState Head { get; set; }

        public FiniteAutomata(TState head)
        {
            Head = head;
        }

        public HashSet<TState> GetStates()
        {
            return GetStates(Head);
        }

        public HashSet<TState> GetStates(Predicate<Transition<TState>> predicate)
        {
            return GetStates(Head, predicate);
        }

        public static HashSet<TState> GetStates(TState head)
        {
            HashSet<TState> states = new HashSet<TState>();
            SeekGraph(head, states, new HashSet<TState>());
            return states;
        }

        public static HashSet<TState> GetStates(TState head, Predicate<Transition<TState>> predicate)
        {
            HashSet<TState> states = new HashSet<TState>();
            SeekGraph(head, states, new HashSet<TState>(), predicate);
            return states;
        }

        public static void SeekGraph(TState head, ISet<TState> states, ISet<TState> visited)
        {
            states.Add(head);
            visited.Add(head);

            head.Transitions
                .Where(trans => !visited.Contains(trans.To))
                .ForEach(trans => SeekGraph(trans.To, states, visited));
        }

        public static void SeekGraph(TState head, ISet<TState> states, ISet<TState> visited, Predicate<Transition<TState>> predicate)
        {
            states.Add(head);
            visited.Add(head);

            head.Transitions
                .Where(trans => !visited.Contains(trans.To))
                .Where(trans => predicate(trans))
                .ForEach(trans => SeekGraph(trans.To, states, visited, predicate));
        }
    }
}
