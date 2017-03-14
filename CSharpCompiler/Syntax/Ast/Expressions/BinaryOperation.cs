using System;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class BinaryOperation : Expression
    {
        public Token Operator { get; private set; }

        public Expression LeftOperand { get; private set; }
        
        public Expression RightOperand { get; private set; }

        public BinaryOperation(Token @operator, Expression leftOperand, Expression rightOperand)
        {
            Operator = @operator;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public override IType InferType()
        {
            var leftType = LeftOperand.InferType();
            var rightType = RightOperand.InferType();
            return TypeInference.InferType(leftType, rightType);
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Build(this);
        }
    }
}
