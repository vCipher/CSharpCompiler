using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class KnownType
    {
        public static readonly TypeReference Object = new TypeReference("Object", "System", ElementType.Object, KnownAssembly.System);
        public static readonly TypeReference Boolean = new TypeReference("Boolean", "System", ElementType.Boolean, KnownAssembly.System);
        public static readonly TypeReference Char = new TypeReference("Char", "System", ElementType.Char, KnownAssembly.System);
        public static readonly TypeReference SByte = new TypeReference("SByte", "System", ElementType.UInt8, KnownAssembly.System);
        public static readonly TypeReference Byte = new TypeReference("Byte", "System", ElementType.Int8, KnownAssembly.System);
        public static readonly TypeReference UInt16 = new TypeReference("UInt16", "System", ElementType.UInt16, KnownAssembly.System);
        public static readonly TypeReference Int16 = new TypeReference("Int16", "System", ElementType.Int16, KnownAssembly.System);
        public static readonly TypeReference UInt32 = new TypeReference("UInt32", "System", ElementType.UInt32, KnownAssembly.System);
        public static readonly TypeReference Int32 = new TypeReference("Int32", "System", ElementType.Int32, KnownAssembly.System);
        public static readonly TypeReference UInt64 = new TypeReference("UInt64", "System", ElementType.UInt64, KnownAssembly.System);
        public static readonly TypeReference Int64 = new TypeReference("Int64", "System", ElementType.Int64, KnownAssembly.System);
        public static readonly TypeReference IntPtr = new TypeReference("IntPtr", "System", ElementType.IntPtr, KnownAssembly.System);
        public static readonly TypeReference Single = new TypeReference("Single", "System", ElementType.Single, KnownAssembly.System);
        public static readonly TypeReference Double = new TypeReference("Double", "System", ElementType.Double, KnownAssembly.System);
        public static readonly TypeReference Decimal = new TypeReference("Decimal", "System", ElementType.ValueType, KnownAssembly.System);
        public static readonly TypeReference String = new TypeReference("String", "System", ElementType.String, KnownAssembly.System);
        public static readonly TypeReference Void = new TypeReference("Void", "System", ElementType.Void, KnownAssembly.System);

        private static Dictionary<KnownTypeCode, TypeReference> _typeMap = new Dictionary<KnownTypeCode, TypeReference>
        {
            [KnownTypeCode.Object] = Object,
            [KnownTypeCode.Boolean] = Boolean,
            [KnownTypeCode.Char] = Char,
            [KnownTypeCode.SByte] = SByte,
            [KnownTypeCode.Byte] = Byte,
            [KnownTypeCode.UInt16] = UInt16,
            [KnownTypeCode.Int16] = Int16,
            [KnownTypeCode.UInt32] = UInt32,
            [KnownTypeCode.Int32] = Int32,
            [KnownTypeCode.UInt64] = UInt64,
            [KnownTypeCode.Int64] = Int64,
            [KnownTypeCode.IntPtr] = IntPtr,
            [KnownTypeCode.Single] = Single,
            [KnownTypeCode.Double] = Double,
            [KnownTypeCode.Decimal] = Decimal,
            [KnownTypeCode.String] = String,
            [KnownTypeCode.Void] = Void
        };

        public static TypeReference Get(KnownTypeCode typeCode)
        {
            TypeReference typeRef;
            if (_typeMap.TryGetValue(typeCode, out typeRef))
                return typeRef;

            throw new UnknownTypeException(typeCode.ToString());
        }
    }
}
