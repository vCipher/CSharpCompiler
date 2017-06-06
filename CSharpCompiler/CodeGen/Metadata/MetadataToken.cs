using System;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MetadataToken : IEquatable<MetadataToken>
    {
        private readonly uint _value;

        public uint RID
        {
            get { return _value & 0x00ffffff; }
        }

        public MetadataTokenType Type
        {
            get { return (MetadataTokenType)(_value & 0xff000000); }
        }

        public MetadataToken(MetadataTokenType type)
        {
            _value = (uint)type;
        }

        public MetadataToken(MetadataTokenType type, uint rid)
        {
            _value = (uint)type | rid;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= (int)_value;
                return res;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is MetadataToken)
                return Equals((MetadataToken)obj);

            return false;
        }

        public bool Equals(MetadataToken other)
        {
            return _value == other._value;
        }

        public static bool operator ==(MetadataToken @this, MetadataToken other)
        {
            return @this.Equals(other);
        }

        public static bool operator !=(MetadataToken @this, MetadataToken other)
        {
            return !@this.Equals(other);
        }
    }
}
