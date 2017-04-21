using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Lexica.Tokens
{
    public sealed class TokenEnumerable : IEnumerable<Token>
    {
        private List<Token> _tokens;

        internal TokenEnumerable(Scanner scanner)
        {
            _tokens = scanner.Scan();
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return new TokenEnumerator(_tokens);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TokenEnumerator(_tokens);
        }
    }
}
