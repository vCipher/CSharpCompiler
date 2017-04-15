using CSharpCompiler.Lexica.Tokens;
using System;
using System.Linq;

namespace CSharpCompiler.Syntax
{
    public sealed class TokenPredictor
    {
        private TokenEnumerator _enumerator;
        private int _shift;

        public bool Result { get; private set; }

        public TokenPredictor(TokenEnumerator enumerator)
        {
            _enumerator = enumerator;
            _shift = 1;
            Result = true;
        }

        public TokenPredictor(TokenPredictor predictor)
        {
            _enumerator = predictor._enumerator;
            _shift = predictor._shift;
            Result = true;
        }

        public TokenPredictor Expect(TokenTag tag)
        {
            Result = Result ? _enumerator.Lookahead(_shift++).Tag == tag : false;
            return this;
        }

        public TokenPredictor ExpectOneOf(params TokenTag[] tags)
        {
            Result = Result ? tags.Contains(_enumerator.Lookahead(_shift++).Tag) : false;
            return this;
        }

        public TokenPredictor Expect(Func<TokenPredictor, bool> condition)
        {
            Result = Result ? condition(this) : false;
            return this;
        }

        public TokenPredictor Expect(Func<TokenTag, bool> condition)
        {
            Result = Result ? condition(_enumerator.Lookahead(_shift++).Tag) : false;
            return this;
        }

        public TokenPredictor Skip(params TokenTag[] tags)
        {
            if (!Result) return this;

            while (tags.Contains(_enumerator.Lookahead(_shift).Tag))
                _shift++;

            return this;
        }

        public TokenPredictor SkipUntil(TokenTag tag)
        {
            if (!Result) return this;

            for (Token token = _enumerator.Lookahead(_shift); token.Tag != tag; _shift++)
            {
                token = _enumerator.Lookahead(_shift);
                if (token == default(Token)) throw new SyntaxException("Input stream finished");
            }

            return this;
        }

        public TokenPredictor Restore(TokenPredictor predictor)
        {
            _shift = predictor._shift;
            Result = predictor.Result;
            return this;
        }
    }
}
