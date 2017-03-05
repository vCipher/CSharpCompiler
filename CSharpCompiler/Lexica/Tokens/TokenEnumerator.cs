using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Lexica.Tokens
{
    public sealed class TokenEnumerator : Enumerator<Token>
    {
        protected override Token DefaultValue
        {
            get { return Tokens.EOF; }
        }

        public TokenEnumerator(IReadOnlyList<Token> source) : base(source)
        { }

        public Token Lookahead()
        {
            return Lookahead(MOVE_STEP);
        }

        public Token Lookahead(int shift)
        {
            return CanMove(shift) ? _source[_index + shift] : DefaultValue;
        }
    }
}
