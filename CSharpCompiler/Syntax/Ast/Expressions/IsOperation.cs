using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Metadata;
using System;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class IsOperation : Expression
    {
        public Expression Operand { get; private set; }

        public AstType Type { get; private set; }

        public IsOperation(Expression operand, AstType type)
        {
            Operand = operand;
            Type = type;
        }

        public override IType InferType()
        {
            return KnownType.Boolean;
        }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}