using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class Transition : IEquatable<Transition>
    {
        public IState From { get; set; }

        public IState To { get; set; }

        public char Character { get; set; }

        public bool IsEpsilon
        {
            get { return Character == char.MinValue; }
        }
        
        public Transition(IState from, IState to, char character)
        {
            From = from;
            To = to;
            Character = character;
        }

        public Transition(IState from, IState to) : this(from, to, char.MinValue)
        { }
        
        public bool Equals(Transition other)
        {
            return From == other.From &&
                To == other.To &&
                Character == other.Character;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Transition))
                return false;

            return Equals((Transition)obj);
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
