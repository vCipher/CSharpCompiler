using System;
using System.Collections.Generic;

namespace CSharpCompiler.Scanners.Regexp
{
    public class NfaState : IState, IEquatable<NfaState>
    {
        public bool IsAccepting { get; set; }

        public string Alias { get; set; }

        public int Id { get; private set; }

        public ISet<Transition> Transitions { get; private set; }

        public NfaState(int id)
        {
            Id = id;
            Transitions = new HashSet<Transition>();
            IsAccepting = false;
            Alias = null;
        }

        public Transition Bind(NfaState to)
        {
            Transition transition = new Transition(this, to);
            Transitions.Add(transition);
            return transition;
        }

        public Transition Bind(NfaState to, char character)
        {
            Transition transition = new Transition(this, to, character);
            Transitions.Add(transition);
            return transition;
        }

        public bool Equals(NfaState other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NfaState))
                return false;

            return Equals((NfaState)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= Id.GetHashCode();
                return res;
            }
        }
    }
}
