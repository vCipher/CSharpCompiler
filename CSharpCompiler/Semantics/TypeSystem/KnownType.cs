using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class KnownType
    {
        public static readonly TypeReference Object = Resolve("Object", "System");
        public static readonly TypeReference Boolean = Resolve("Boolean", "System");
        public static readonly TypeReference Char = Resolve("Char", "System");
        public static readonly TypeReference SByte = Resolve("SByte", "System");
        public static readonly TypeReference Byte = Resolve("Byte", "System");
        public static readonly TypeReference UInt16 = Resolve("UInt16", "System");
        public static readonly TypeReference Int16 = Resolve("Int16", "System");
        public static readonly TypeReference UInt32 = Resolve("UInt32", "System");
        public static readonly TypeReference Int32 = Resolve("Int32", "System");
        public static readonly TypeReference UInt64 = Resolve("UInt64", "System");
        public static readonly TypeReference Int64 = Resolve("Int64", "System");
        public static readonly TypeReference UIntPtr = Resolve("UIntPtr", "System");
        public static readonly TypeReference IntPtr = Resolve("IntPtr", "System");
        public static readonly TypeReference Single = Resolve("Single", "System");
        public static readonly TypeReference Double = Resolve("Double", "System");
        public static readonly TypeReference Decimal = Resolve("Decimal", "System");
        public static readonly TypeReference String = Resolve("String", "System");
        public static readonly TypeReference Void = Resolve("Void", "System");

        public static KnownTypeCode GetTypeCode(ITypeInfo type)
        {
            if (type.IsTypeOf(Object)) return KnownTypeCode.Object;
            if (type.IsTypeOf(Boolean)) return KnownTypeCode.Boolean;
            if (type.IsTypeOf(Char)) return KnownTypeCode.Char;
            if (type.IsTypeOf(SByte)) return KnownTypeCode.SByte;
            if (type.IsTypeOf(Byte)) return KnownTypeCode.Byte;
            if (type.IsTypeOf(UInt16)) return KnownTypeCode.UInt16;
            if (type.IsTypeOf(Int16)) return KnownTypeCode.Int16;
            if (type.IsTypeOf(UInt32)) return KnownTypeCode.UInt32;
            if (type.IsTypeOf(Int32)) return KnownTypeCode.Int32;
            if (type.IsTypeOf(UInt64)) return KnownTypeCode.UInt64;
            if (type.IsTypeOf(Int64)) return KnownTypeCode.Int64;
            if (type.IsTypeOf(UIntPtr)) return KnownTypeCode.UIntPtr;
            if (type.IsTypeOf(IntPtr)) return KnownTypeCode.IntPtr;
            if (type.IsTypeOf(Single)) return KnownTypeCode.Single;
            if (type.IsTypeOf(Double)) return KnownTypeCode.Double;
            if (type.IsTypeOf(Decimal)) return KnownTypeCode.Decimal;
            if (type.IsTypeOf(String)) return KnownTypeCode.String;
            if (type.IsTypeOf(Void)) return KnownTypeCode.Void;
            return KnownTypeCode.None;
        }

        public static KnownTypeCode GetTypeCode(TokenTag tokenTag)
        {
            switch (tokenTag)
            {
                case TokenTag.OBJECT: return KnownTypeCode.Object;
                case TokenTag.BOOL: return KnownTypeCode.Boolean;
                case TokenTag.CHAR: return KnownTypeCode.Char;
                case TokenTag.SBYTE: return KnownTypeCode.SByte;
                case TokenTag.BYTE: return KnownTypeCode.Byte;
                case TokenTag.USHORT: return KnownTypeCode.UInt16;
                case TokenTag.SHORT: return KnownTypeCode.Int16;
                case TokenTag.UINT: return KnownTypeCode.UInt32;
                case TokenTag.INT: return KnownTypeCode.Int32;
                case TokenTag.ULONG: return KnownTypeCode.UInt64;
                case TokenTag.LONG: return KnownTypeCode.Int64;
                case TokenTag.FLOAT: return KnownTypeCode.Single;
                case TokenTag.DOUBLE: return KnownTypeCode.Double;
                case TokenTag.DECIMAL: return KnownTypeCode.Decimal;
                case TokenTag.STRING: return KnownTypeCode.String;
                case TokenTag.VOID: return KnownTypeCode.Void;
                default: return KnownTypeCode.None;
            }
        }

        public static ElementType GetElementType(KnownTypeCode typeCode)
        {
            switch (typeCode)
            {
                case KnownTypeCode.Object: return ElementType.Object;
                case KnownTypeCode.Boolean: return ElementType.Boolean;
                case KnownTypeCode.Char: return ElementType.Char;
                case KnownTypeCode.SByte: return ElementType.UInt8;
                case KnownTypeCode.Byte: return ElementType.Int8;
                case KnownTypeCode.UInt16: return ElementType.UInt16;
                case KnownTypeCode.Int16: return ElementType.Int16;
                case KnownTypeCode.UInt32: return ElementType.UInt32;
                case KnownTypeCode.Int32: return ElementType.Int32;
                case KnownTypeCode.UInt64: return ElementType.UInt64;
                case KnownTypeCode.Int64: return ElementType.Int64;
                case KnownTypeCode.UIntPtr: return ElementType.UIntPtr;
                case KnownTypeCode.IntPtr: return ElementType.IntPtr;
                case KnownTypeCode.Single: return ElementType.Single;
                case KnownTypeCode.Double: return ElementType.Double;
                case KnownTypeCode.Decimal: return ElementType.ValueType;
                case KnownTypeCode.String: return ElementType.String;
                case KnownTypeCode.Void: return ElementType.Void;
                default: throw new UnknownTypeException(typeCode.ToString());
            }
        }

        public static KnownTypeCode GetTypeCode(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Object: return KnownTypeCode.Object;
                case ElementType.Boolean: return KnownTypeCode.Boolean;
                case ElementType.Char: return KnownTypeCode.Char;
                case ElementType.UInt8: return KnownTypeCode.SByte;
                case ElementType.Int8: return KnownTypeCode.Byte;
                case ElementType.UInt16: return KnownTypeCode.UInt16;
                case ElementType.Int16: return KnownTypeCode.Int16;
                case ElementType.UInt32: return KnownTypeCode.UInt32;
                case ElementType.Int32: return KnownTypeCode.Int32;
                case ElementType.UInt64: return KnownTypeCode.UInt64;
                case ElementType.Int64: return KnownTypeCode.Int64;
                case ElementType.UIntPtr: return KnownTypeCode.UIntPtr;
                case ElementType.IntPtr: return KnownTypeCode.IntPtr;
                case ElementType.Single: return KnownTypeCode.Single;
                case ElementType.Double: return KnownTypeCode.Double;
                case ElementType.String: return KnownTypeCode.String;
                case ElementType.Void: return KnownTypeCode.Void;
                default: return KnownTypeCode.None;
            }
        }

        public static TypeReference GetType(KnownTypeCode typeCode)
        {
            switch (typeCode)
            {
                case KnownTypeCode.Object: return Object;
                case KnownTypeCode.Boolean: return Boolean;
                case KnownTypeCode.Char: return Char;
                case KnownTypeCode.SByte: return SByte;
                case KnownTypeCode.Byte: return Byte;
                case KnownTypeCode.UInt16: return UInt16;
                case KnownTypeCode.Int16: return Int16;
                case KnownTypeCode.UInt32: return UInt32;
                case KnownTypeCode.Int32: return Int32;
                case KnownTypeCode.UInt64: return UInt64;
                case KnownTypeCode.Int64: return Int64;
                case KnownTypeCode.UIntPtr: return UIntPtr;
                case KnownTypeCode.IntPtr: return IntPtr;
                case KnownTypeCode.Single: return Single;
                case KnownTypeCode.Double: return Double;
                case KnownTypeCode.Decimal: return Decimal;
                case KnownTypeCode.String: return String;
                case KnownTypeCode.Void: return Void;
                default: throw new UnknownTypeException(typeCode.ToString());
            }
        }

        private static TypeReference Resolve(string name, string @namespace)
        {
            var assemblyDef = AssemblyFactory.GetAssemblyDefinition(KnownAssembly.System_CorLib);
            var typeDef = assemblyDef.Module.GetType(name, @namespace);
            return TypeFactory.GetTypeReference(typeDef);
        }
    }
}
