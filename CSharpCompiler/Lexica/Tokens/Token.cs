using System;
using System.Diagnostics;

namespace CSharpCompiler.Lexica.Tokens
{
    /// <summary>
    /// Basic symbols, used to forming strings.
    /// </summary>
    [DebuggerDisplay("[{Tag}, {Lexeme}]")]
    public struct Token : IEquatable<Token>
    {
        public TokenTag Tag { get; private set; }
        public string Lexeme { get; private set; }
        
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

        public override bool Equals(object obj)
        {
            if (obj is Token)
                return Equals((Token)obj);

            return false;
        }

        public bool Equals(Token other)
        {
            return Tag == other.Tag && string.Equals(Lexeme, other.Lexeme);
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
