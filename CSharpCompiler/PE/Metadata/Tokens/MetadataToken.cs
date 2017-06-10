using System;
using System.Runtime.InteropServices;

namespace CSharpCompiler.PE.Metadata.Tokens
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

        public MetadataToken(MetadataTokenType type) : this()
        {
            _value = (uint)type;
        }

        public MetadataToken(MetadataTokenType type, uint rid) : this()
        {
            _value = (uint)type | rid;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is MetadataToken) && Equals((MetadataToken)obj);
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
