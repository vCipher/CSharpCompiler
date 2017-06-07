using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class DfaState : ITransitionState<DfaState>, IEquatable<DfaState>
    {
        private const int EMPTY_STATE_ID = -1;
        public static readonly DfaState Empty = new DfaState(EMPTY_STATE_ID, Enumerable.Empty<NfaState>());

        public bool IsAccepting { get; set; }
        public string Alias { get; set; }
        public int Id { get; private set; }

        public ISet<Transition<DfaState>> Transitions { get; private set; }
        public ISet<NfaState> NfaStates { get; private set; }        
        
        public DfaState(int id, IEnumerable<NfaState> nfaStates)
        {
            Id = id;
            NfaStates = new HashSet<NfaState>(nfaStates);
            Transitions = new HashSet<Transition<DfaState>>();
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
            return (obj is DfaState) && Equals((DfaState)obj);
        }

        public bool Equals(DfaState other)
        {
            return (other != null) && Id == other.Id;
        }
    }
}
