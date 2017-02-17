using System;

namespace CSharpCompiler.Scanners.Tokens
{
    /// <summary>
    /// Basic symbols, used to forming strings.
    /// </summary>
    public class Token : IEquatable<Token>
    {
        public int Tag { get; set; }

        public string Lexeme { get; set; }

        public Token(int tag)
        {
            Lexeme = tag.ToString();
            Tag = tag;
        }

        public Token(string lexeme, int tag)
        {
            Lexeme = lexeme;
            Tag = tag;
        }

        public override string ToString()
        {
            return Lexeme;
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
                res ^= (int)Tag;
                res ^= (Lexeme == null ? 0 : Lexeme.GetHashCode());
                return res;
            }
        }
    }
}
