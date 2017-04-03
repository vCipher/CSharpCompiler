using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ArrayCreation : Expression
    {
        public TypeNode ContainedType { get; private set; }
        public Expression Initializer { get; private set; }
        public bool IsStatementExpression { get; private set; }

        public ArrayCreation(TypeNode containedType, Expression initializer, bool isStatementExpression)
        {
            ContainedType = containedType;
            Initializer = initializer;
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitArrayCreation(this);
        }
    }
}
