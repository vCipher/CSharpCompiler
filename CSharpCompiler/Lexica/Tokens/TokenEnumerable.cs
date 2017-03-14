using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Lexica.Tokens
{
    public sealed class TokenEnumerable : IEnumerable<Token>
    {
        private Lazy<List<Token>> _tokens;

        internal TokenEnumerable(Scanner scanner)
        {
            _tokens = new Lazy<List<Token>>(() => scanner.Scan());
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return new TokenEnumerator(_tokens.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TokenEnumerator(_tokens.Value);
        }
    }
}
