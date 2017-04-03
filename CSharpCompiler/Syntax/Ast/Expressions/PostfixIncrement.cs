namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class PostfixIncrement : Expression
    {
        public Expression Operand { get; private set; }
        public bool IsStatementExpression { get; private set; }
        
        public PostfixIncrement(Expression operand, bool isStatementExpression)
        {
            Operand = operand;
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitPostfixIncrement(this);
        }
    }
}
