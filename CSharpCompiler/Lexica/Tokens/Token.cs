using System;

namespace CSharpCompiler.Lexica.Tokens
{
    /// <summary>
    /// Basic symbols, used to forming strings.
    /// </summary>
    public struct Token : IEquatable<Token>
    {
        public TokenTag Tag { get; set; }

        public string Lexeme { get; set; }
        
        public Token(TokenTag tag)
        {
            Lexeme = null;
            Tag = tag;
        }

        public Token(string lexeme, TokenTag tag)
        {
            Lexeme = lexeme;
            Tag = tag;
        }

        public override string ToString()
        {
            return string.Format("<{0}, {1}>", Tag, Lexeme);
        }

        public bool Equals(Token other)
        {
            return Tag == other.Tag && string.Equals(Lexeme, other.Lexeme);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Token))
                return false;

            return Equals((Token)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= Tag.GetHashCode();
                res ^= (Lexeme == null ? 0 : Lexeme.GetHashCode());
                return res;
            }
        }

        public static bool operator ==(Token first, Token second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Token first, Token second)
        {
            return !first.Equals(second);
        }
    }
}
