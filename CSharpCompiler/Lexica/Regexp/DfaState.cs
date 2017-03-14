using System;
using System.Collections;
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
                int res = 31;
                res ^= Id.GetHashCode();
                return res;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is DfaState)
                return Equals((DfaState)obj);

            return false;
        }

        public bool Equals(DfaState other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }
    }
}
