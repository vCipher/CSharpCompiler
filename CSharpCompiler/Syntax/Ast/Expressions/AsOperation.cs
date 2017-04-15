using System;
using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class AsOperation : Expression
    {
        public Expression Operand { get; private set; }

        public TypeNode Type { get; private set; }

        public AsOperation(Expression operand, TypeNode type)
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
