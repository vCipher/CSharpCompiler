using System;

namespace CSharpCompiler.Lexica.Regexp
{
    public static class Transition
    {
        public const char EPSILON_CHAR = char.MinValue;

        public static Transition<TState> Create<TState>(TState from, TState to) where TState : ITransitionState<TState>
        {
            return Create(from, to, EPSILON_CHAR);
        }

        public static Transition<TState> Create<TState>(TState from, TState to, char character) where TState : ITransitionState<TState>
        {
            var transition = new Transition<TState>(from, to, character);
            from.Transitions.Add(transition);
            return transition;
        }
    }

    public sealed class Transition<TState> : IEquatable<Transition<TState>> where TState : IState
    {
        public TState From { get; set; }
        public TState To { get; set; }
        public char Character { get; set; }

        public bool IsEpsilon
        {
            get { return Character == Transition.EPSILON_CHAR; }
        }

        public Transition(TState from, TState to, char character)
        {
            From = from;
            To = to;
            Character = character;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + From?.GetHashCode() ?? 0;
                hash = hash * 23 + To?.GetHashCode() ?? 0;
                hash = hash * 23 + Character.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is Transition<TState>) && Equals((Transition<TState>)obj);
        }

        public bool Equals(Transition<TState> other)
        {
            return (From != null && From.Equals(other.From))
                && (To != null && To.Equals(other.To))
                && (Character == other.Character);
        }
    }
}
