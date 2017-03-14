using System;
using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class CastExpression : Expression
    {
        public Expression Operand { get; private set; }

        public AstType Type { get; private set; }

        public CastExpression(AstType type, Expression operand)
        {
            Operand = operand;
            Type = type;
        }

        public override IType InferType()
        {
            return Type.ToType();
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Build(this);
        }
    }
}