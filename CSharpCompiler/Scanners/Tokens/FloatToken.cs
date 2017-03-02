using System;

namespace CSharpCompiler.Scanners.Tokens
{
    public sealed class FloatToken : Token, IEquatable<FloatToken>
    {
        public float Value { get; private set; }

        public FloatToken(float value) : base(value.ToString(), TokenTag.FLOAT_CONST)
        {
            Value = value;
        }

        public FloatToken(string value) : base(value, TokenTag.FLOAT_CONST)
        {
            Value = float.Parse(value);
        }

        public bool Equals(FloatToken other)
        {
            return base.Equals(other) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FloatToken))
                return false;

            return Equals((FloatToken)obj);
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
