using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Cil;
using System;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Literal : Expression
    {
        public Token Value { get; private set; }

        public Literal(Token value)
        {
            Value = value;
        }

        public override IType InferType()
        {
            switch (Value.Tag)
            {
                case TokenTag.INT_LITERAL: return KnownType.Int32;
                case TokenTag.FLOAT_LITERAL: return KnownType.Single;
                case TokenTag.DOUBLE_LITERAL: return KnownType.Double;
                case TokenTag.STRING_LITERAL: return KnownType.String;
                case TokenTag.TRUE: return KnownType.Boolean;
                case TokenTag.FALSE: return KnownType.Boolean;
            }

            throw new TypeInferenceException("Can't inference type from literal: {0}", Value);
        }

        public override void Build(MethodBuilder builder)
        {
            switch (Value.Tag)
            {
                case TokenTag.INT_LITERAL: builder.Emit(OpCodes.Ldc_I4, int.Parse(Value.Lexeme)); return;
                case TokenTag.STRING_LITERAL: builder.Emit(OpCodes.Ldstr, Value.Lexeme); return;
            }

            throw new NotSupportedException(string.Format("Not supported literal: {0}", Value));
        }
    }
}