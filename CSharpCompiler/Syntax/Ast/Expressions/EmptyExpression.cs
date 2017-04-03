namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class EmptyExpression : Expression
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitEmptyExpression(this);
        }
    }
}
