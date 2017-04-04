using CSharpCompiler.Utility;
using System.Collections.Generic;

namespace CSharpCompiler.Lexica.Tokens
{
    public sealed class TokenEnumerator : Enumerator<Token>
    {
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
