using CSharpCompiler.Lexica.Regexp;
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

        public bool Equals(Transition<TState> other)
        {
            return From.Equals(other.From) &&
                To.Equals(other.To) &&
                Character == other.Character;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Transition<TState>))
                return false;

            return Equals((Transition<TState>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= From?.GetHashCode() ?? 0;
                res ^= To?.GetHashCode() ?? 0;
                res ^= Character.GetHashCode();
                return res;
            }
        }
    }
}
