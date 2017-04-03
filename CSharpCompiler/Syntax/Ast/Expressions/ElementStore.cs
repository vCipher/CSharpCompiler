namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ElementStore : Expression
    {
        public Expression Array { get; private set; }
        public Expression Index { get; private set; }
        public Expression Value { get; private set; }
        public bool IsStatementExpression { get; private set; }

        public ElementStore(Expression array, Expression index, Expression value, bool isStatementExpression)
        {
            Array = array;
            Index = index;
            Value = value;
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitElementStore(this);
        }
    }
}
