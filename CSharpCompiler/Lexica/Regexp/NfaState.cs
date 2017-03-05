using System;
using System.Collections.Generic;

namespace CSharpCompiler.Lexica.Regexp
{
    public class NfaState : ITransitionState<NfaState>, IEquatable<NfaState>
    {
        public bool IsAccepting { get; set; }

        public string Alias { get; set; }

        public int Id { get; private set; }

        public ISet<Transition<NfaState>> Transitions { get; private set; }

        public NfaState(int id)
        {
            Id = id;
            Transitions = new HashSet<Transition<NfaState>>();
            IsAccepting = false;
            Alias = null;
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
