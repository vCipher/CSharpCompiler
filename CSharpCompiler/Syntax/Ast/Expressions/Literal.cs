using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Literal : Expression
    {
        public Token Value { get; private set; }

        public Literal(Token value)
        {
            Value = value;
        }

        private static PrimitiveType GetType(Token value)
        {
            switch (value.Tag)
            {
                case TokenTag.INT_CONST:
                    return PrimitiveType.INT;
                case TokenTag.FLOAT_CONST:
                    return PrimitiveType.FLOAT;
                case TokenTag.DOUBLE_CONST:
                    return PrimitiveType.DOUBLE;
                case TokenTag.TRUE:
                case TokenTag.FALSE:
                    return PrimitiveType.BOOL;
                default:
                    throw new TypeInferenceException(value);
            }
        }
    }
}