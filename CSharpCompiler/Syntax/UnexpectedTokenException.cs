using CSharpCompiler.Lexica.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax
{
    [Serializable]
    public sealed class UnexpectedTokenException : SyntaxException
    {
        public UnexpectedTokenException(TokenTag expected, Token actual) 
            : base(string.Format("Expected {0} tokens. But actual is {1}", expected, actual))
        { }
    }
}
