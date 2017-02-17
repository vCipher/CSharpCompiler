using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class DfaState : IState, IEquatable<DfaState>
    {
        public bool IsAccepting { get; set; }

        public string Alias { get; set; }

        public int Id { get; private set; }

        public ISet<Transition> Transitions { get; private set; }

        public ISet<NfaState> NfaStates { get; private set; }

        
        public DfaState(int id, IEnumerable<NfaState> nfaStates)
        {
            Id = id;
            NfaStates = new HashSet<NfaState>(nfaStates);
            Transitions = new HashSet<Transition>();
            IsAccepting = false;
            Alias = null;
        }
        
        public bool Equals(DfaState other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DfaState))
                return false;

            return Equals((DfaState)obj);
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
