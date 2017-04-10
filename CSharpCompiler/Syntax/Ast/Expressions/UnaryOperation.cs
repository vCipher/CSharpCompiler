using System;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class UnaryOperation : Expression
    {
        public Token Operator { get; private set; }

        public Expression Operand { get; private set; }

        public UnaryOperation(Token @operator, Expression operand)
        {
            Operator = @operator;
            Operand = operand;
        }

        public override IType InferType()
        {
            return Operand.InferType();
        }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
