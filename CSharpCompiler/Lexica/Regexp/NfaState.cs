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

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is NfaState) && Equals((NfaState)obj);
        }

        public bool Equals(NfaState other)
        {
            return Id == other.Id;
        }
    }
}
