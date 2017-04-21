using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions.Relations
{
    public abstract class RelationOperation : BinaryOperation
    {
        public RelationOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override ITypeInfo InferType()
        {
            var leftType = LeftOperand.InferType();
            var rightType = RightOperand.InferType();

            if (KnownType.Boolean.Equals(leftType) && KnownType.Boolean.Equals(rightType))
                return KnownType.Boolean;

            throw new TypeInferenceException("Can't infer type for: {0} and for: {1}", leftType, rightType);
        }
    }
}
