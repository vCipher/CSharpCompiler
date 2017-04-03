namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class PrefixDecrement : Expression
    {
        public Expression Operand { get; private set; }
        public bool IsStatementExpression { get; private set; }
        
        public PrefixDecrement(Expression operand, bool isStatementExpression)
        {
            Operand = operand;
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitPrefixDecrement(this);
        }
    }
}
