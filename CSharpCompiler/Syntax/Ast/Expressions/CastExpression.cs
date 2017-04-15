using System;
using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class CastExpression : Expression
    {
        public Expression Operand { get; private set; }

        public TypeNode Type { get; private set; }

        public CastExpression(TypeNode type, Expression operand)
        {
            Operand = operand;
            Type = type;
        }

        public override ITypeInfo InferType()
        {
            return Type.ToType();
        }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}