using System;

namespace CSharpCompiler.Scanners.Tokens
{
    public sealed class TypeToken : Token, IEquatable<TypeToken>
    {
        /// <summary>
        /// Used in memory allocation.
        /// </summary>
        public int Width { get; set; }

        public TypeToken(string lexeme, int tag, int width = 4) : base(lexeme, tag)
        {
            Width = width;
        }

        public bool Equals(TypeToken other)
        {
            return base.Equals(other) && Width == other.Width;
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
                int res = base.GetHashCode();
                res ^= Width;
                return res;
            }
        }
    }
}
