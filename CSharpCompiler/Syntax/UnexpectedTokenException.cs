using CSharpCompiler.Lexica.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax
{
    [Serializable]
    public sealed class UnexpectedTokenException : ParseException
    {
        public UnexpectedTokenException(TokenTag[] expectedTokenTags, Token actualToken)
            : base(GetMessage(expectedTokenTags, actualToken))
        { }

        public UnexpectedTokenException(TokenTag expectedTokenTag, Token actualToken) 
            : base(GetMessage(expectedTokenTag, actualToken))
        { }

        private static string GetMessage(TokenTag[] expectedTokenTags, Token actualToken)
        {
            return string.Format("Expected {0} tokens. But actual is {1}",
                string.Join(", ", expectedTokenTags),
                actualToken);
        }

        private static string GetMessage(TokenTag expectedTokenTag, Token actualToken)
        {
            return string.Format("Expected {0} tokens. But actual is {1}", expectedTokenTag, actualToken);
        }
    }
}
