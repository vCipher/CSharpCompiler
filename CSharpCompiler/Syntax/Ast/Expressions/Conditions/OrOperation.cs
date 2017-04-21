using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using System;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions.Relations
{
    public sealed class OrOperation : BinaryOperation
    {
        public OrOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override ITypeInfo InferType()
        {
            var leftType = LeftOperand.InferType();
            var rightType = RightOperand.InferType();

            if (KnownType.Boolean.Equals(leftType) && KnownType.Boolean.Equals(rightType))
                return KnownType.Boolean;

            throw new TypeInferenceException("Can't infer a type for: {0} and for: {1}", leftType, rightType);
        }
    }
}
