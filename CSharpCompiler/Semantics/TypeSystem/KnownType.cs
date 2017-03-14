using System;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class KnownType : IType, IEquatable<KnownType>
    {
        public static readonly KnownType Object = new KnownType(KnownTypeCode.Object);
        public static readonly KnownType Boolean = new KnownType(KnownTypeCode.Boolean);
        public static readonly KnownType Char = new KnownType(KnownTypeCode.Char);
        public static readonly KnownType SByte = new KnownType(KnownTypeCode.SByte);
        public static readonly KnownType Byte = new KnownType(KnownTypeCode.Byte);
        public static readonly KnownType UInt16 = new KnownType(KnownTypeCode.UInt16);
        public static readonly KnownType Int16 = new KnownType(KnownTypeCode.Int16);
        public static readonly KnownType UInt32 = new KnownType(KnownTypeCode.UInt32);
        public static readonly KnownType Int32 = new KnownType(KnownTypeCode.Int32);
        public static readonly KnownType UInt64 = new KnownType(KnownTypeCode.UInt64);
        public static readonly KnownType Int64 = new KnownType(KnownTypeCode.Int64);
        public static readonly KnownType Single = new KnownType(KnownTypeCode.Single);
        public static readonly KnownType Double = new KnownType(KnownTypeCode.Double);
        public static readonly KnownType Decimal = new KnownType(KnownTypeCode.Decimal);
        public static readonly KnownType String = new KnownType(KnownTypeCode.String);
        public static readonly KnownType Void = new KnownType(KnownTypeCode.Void);
        public static readonly KnownType Type = new KnownType(KnownTypeCode.Type);
        public static readonly KnownType Array = new KnownType(KnownTypeCode.Array);
        public static readonly KnownType Attribute = new KnownType(KnownTypeCode.Attribute);
        public static readonly KnownType ValueType = new KnownType(KnownTypeCode.ValueType);
        public static readonly KnownType Enum = new KnownType(KnownTypeCode.Enum);
        public static readonly KnownType Delegate = new KnownType(KnownTypeCode.Delegate);

        public KnownTypeCode TypeCode { get; private set; }
        public ElementType ElementType { get { return TypeCode.GetElementType(); } }

        public KnownType(KnownTypeCode typeCode)
        {
            TypeCode = typeCode;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= TypeCode.GetHashCode();
                return res;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is KnownType)
                return Equals((KnownType)obj);

            return false;
        }

        public bool Equals(KnownType other)
        {
            return TypeCode == other.TypeCode;
        }
    }
}
