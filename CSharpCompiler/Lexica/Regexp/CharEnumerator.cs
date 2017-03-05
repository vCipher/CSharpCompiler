using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class CharEnumerator : Enumerator<char>
    {
        public CharEnumerator(string source) : base(source.ToCharArray())
        { }

        public bool MovePrevious()
        {
            if (CanMove(-MOVE_STEP))
            {
                _index -= MOVE_STEP;
                _currentElement = _source[_index];
                return true;
            }
            
            return false;
        }
    }
}
