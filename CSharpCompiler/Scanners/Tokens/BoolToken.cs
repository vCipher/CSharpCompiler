using System;

namespace CSharpCompiler.Scanners.Tokens
{
    public sealed class BoolToken : Token, IEquatable<BoolToken>
    {
        public bool Value { get; private set; }

        public BoolToken(bool value, int tag) : base(tag)
        {
            Value = value;
        }

        public static explicit operator bool (BoolToken src)
        {
            return src.Value;
        }

        public bool Equals(BoolToken other)
        {
            return base.Equals(other) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BoolToken))
                return false;

            return Equals((BoolToken)obj);
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
