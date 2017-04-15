using CSharpCompiler.Lexica.Tokens;
using System;

namespace CSharpCompiler.Semantics.TypeSystem
{
    internal static class ExtensionMethods
    {
        public static KnownTypeCode GetKnownTypeCode(this TokenTag tokenTag)
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

        public static KnownTypeCode GetKnownTypeCode(this Type type)
        {
            if (type == typeof(object)) return KnownTypeCode.Object;
            if (type == typeof(bool)) return KnownTypeCode.Boolean;
            if (type == typeof(char)) return KnownTypeCode.Char;
            if (type == typeof(sbyte)) return KnownTypeCode.SByte;
            if (type == typeof(byte)) return KnownTypeCode.Byte;
            if (type == typeof(ushort)) return KnownTypeCode.UInt16;
            if (type == typeof(short)) return KnownTypeCode.Int16;
            if (type == typeof(uint)) return KnownTypeCode.UInt32;
            if (type == typeof(int)) return KnownTypeCode.Int32;
            if (type == typeof(ulong)) return KnownTypeCode.UInt64;
            if (type == typeof(long)) return KnownTypeCode.Int64;
            if (type == typeof(float)) return KnownTypeCode.Single;
            if (type == typeof(double)) return KnownTypeCode.Double;
            if (type == typeof(decimal)) return KnownTypeCode.Decimal;
            if (type == typeof(string)) return KnownTypeCode.String;
            if (type == typeof(void)) return KnownTypeCode.Void;
            return KnownTypeCode.None;
        }

        public static bool IsPrimitiveType(this TokenTag tokenTag)
        {
            return GetKnownTypeCode(tokenTag) != KnownTypeCode.None;
        }
    }
}
