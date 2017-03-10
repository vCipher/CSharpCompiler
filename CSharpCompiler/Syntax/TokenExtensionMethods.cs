﻿using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax
{
    internal static class TokenExtensionMethods
    {
        public static bool IsPrimitiveTypeToken(this Token token)
        {
            return PrimitiveType.IsPrimitiveType(token);
        }

        public static bool IsUnaryToken(this Token token)
        {
            return token.Tag == TokenTag.MINUS ||
                token.Tag == TokenTag.NOT ||
                token.Tag == TokenTag.INCREMENT ||
                token.Tag == TokenTag.DECREMENT;
        }

        public static bool IsRelationToken(this Token token)
        {
            return token.Tag == TokenTag.EQUAL ||
                token.Tag == TokenTag.NOT_EQUAL ||
                token.Tag == TokenTag.LESS ||
                token.Tag == TokenTag.GREATER ||
                token.Tag == TokenTag.LESS_OR_EQUAL ||
                token.Tag == TokenTag.GREATER_OR_EQUAL;
        }

        public static bool IsArithmeticToken(this Token token)
        {
            return token.Tag == TokenTag.PLUS ||
                token.Tag == TokenTag.MINUS;
        }

        public static bool IsFactorToken(this Token token)
        {
            return token.Tag == TokenTag.MULTIPLY ||
                token.Tag == TokenTag.DIVIDE ||
                token.Tag == TokenTag.MOD ||
                token.Tag == TokenTag.LEFT_SHIFT ||
                token.Tag == TokenTag.RIGHT_SHIFT;
        }

        public static bool IsLiteralToken(this Token token)
        {
            return token.Tag == TokenTag.INT_CONST ||
                token.Tag == TokenTag.FLOAT_CONST ||
                token.Tag == TokenTag.DOUBLE_CONST ||
                token.Tag == TokenTag.TRUE ||
                token.Tag == TokenTag.FALSE;
        }

        public static bool IsConditionToken(this Token token)
        {
            return token.Tag == TokenTag.OR ||
                token.Tag == TokenTag.AND ||
                token.Tag == TokenTag.BIT_OR ||
                token.Tag == TokenTag.BIT_XOR ||
                token.Tag == TokenTag.BIT_AND;
        }
    }
}
