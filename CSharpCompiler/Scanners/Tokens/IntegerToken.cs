using System;

namespace CSharpCompiler.Scanners.Tokens
{
    public sealed class IntegerToken : Token, IEquatable<IntegerToken>
    {
        public int Value { get; private set; }

        public IntegerToken(int value) : base((int)CSharpTokenTag.INT_CONST)
        {
            Value = value;
        }

        public IntegerToken(string value) : base((int)CSharpTokenTag.INT_CONST)
        {
            Value = int.Parse(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator int(IntegerToken src)
        {
            return src.Value;
        }

        public bool Equals(IntegerToken other)
        {
            return base.Equals(other) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IntegerToken))
                return false;

            return Equals((IntegerToken)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = base.GetHashCode();
                res ^= Value.GetHashCode();

                return res;
            }
        }
    }
}
