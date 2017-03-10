using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public sealed class PrimitiveType : AstType
    {
        public static readonly PrimitiveType BOOL = new PrimitiveType(Tokens.BOOL);
        public static readonly PrimitiveType BYTE = new PrimitiveType(Tokens.BYTE);
        public static readonly PrimitiveType CHAR = new PrimitiveType(Tokens.CHAR);
        public static readonly PrimitiveType DECIMAL = new PrimitiveType(Tokens.DECIMAL);
        public static readonly PrimitiveType DOUBLE = new PrimitiveType(Tokens.DOUBLE);
        public static readonly PrimitiveType FLOAT = new PrimitiveType(Tokens.FLOAT);
        public static readonly PrimitiveType INT = new PrimitiveType(Tokens.INT);
        public static readonly PrimitiveType LONG = new PrimitiveType(Tokens.LONG);
        public static readonly PrimitiveType OBJECT = new PrimitiveType(Tokens.OBJECT);
        public static readonly PrimitiveType SBYTE = new PrimitiveType(Tokens.SBYTE);
        public static readonly PrimitiveType SHORT = new PrimitiveType(Tokens.SHORT);
        public static readonly PrimitiveType STRING = new PrimitiveType(Tokens.STRING);
        public static readonly PrimitiveType USHORT = new PrimitiveType(Tokens.USHORT);
        public static readonly PrimitiveType UINT = new PrimitiveType(Tokens.UINT);
        public static readonly PrimitiveType ULONG = new PrimitiveType(Tokens.ULONG);
        public static readonly PrimitiveType VOID = new PrimitiveType(Tokens.VOID);

        public Token TypeToken { get; private set; }

        public PrimitiveType(Token typeToken)
        {
            TypeToken = typeToken;
        }

        public static bool IsPrimitiveType(Token token)
        {
            return ToKnownTypeCode(token.Tag) != KnownTypeCode.None;
        }

        private static KnownTypeCode ToKnownTypeCode(TokenTag tokenTag)
        {
            switch (tokenTag)
            {
                case TokenTag.OBJECT: return KnownTypeCode.Object;
                case TokenTag.BOOL: return KnownTypeCode.Boolean;
                case TokenTag.CHAR: return KnownTypeCode.Char;
                case TokenTag.SBYTE: return KnownTypeCode.SByte;
                case TokenTag.SHORT: return KnownTypeCode.Int16;
                case TokenTag.USHORT: return KnownTypeCode.UInt16;
                case TokenTag.INT: return KnownTypeCode.Int32;
                case TokenTag.UINT: return KnownTypeCode.UInt32;
                case TokenTag.LONG: return KnownTypeCode.Int64;
                case TokenTag.ULONG: return KnownTypeCode.UInt64;
                case TokenTag.FLOAT: return KnownTypeCode.Single;
                case TokenTag.DOUBLE: return KnownTypeCode.Double;
                case TokenTag.DECIMAL: return KnownTypeCode.Decimal;
                case TokenTag.STRING: return KnownTypeCode.String;
                case TokenTag.VOID: return KnownTypeCode.Void;
                default: return KnownTypeCode.None;
            }
        }

        public override IType ToType()
        {
            var typeCode = ToKnownTypeCode(TypeToken.Tag);
            if (typeCode == KnownTypeCode.None)
                throw new UnknownTypeException(TypeToken.ToString());

            return new KnownType(typeCode);
        }
    }
}
