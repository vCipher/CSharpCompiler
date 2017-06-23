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
                var hash = 17;
                hash = hash * 23 + Tag.GetHashCode();
                hash = hash * 23 + Lexeme?.GetHashCode() ?? 0;
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is Token) && Equals((Token)obj);
        }

        public bool Equals(Token other)
        {
            return Tag == other.Tag && string.Equals(Lexeme, other.Lexeme);
        }

        public override string ToString()
        {
            return $"[{Tag}, {Lexeme}]";
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
