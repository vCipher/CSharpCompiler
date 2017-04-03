namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class TernaryOperation : Expression
    {
        public Expression Condition { get; private set; }
        public Expression TrueBranch { get; private set; }
        public Expression FalseBranch { get; private set; }

        public TernaryOperation(Expression condition, Expression trueBranch, Expression falseBranch)
        {
            Condition = condition;
            TrueBranch = trueBranch;
            FalseBranch = falseBranch;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitTernaryOperation(this);
        }
    }
}
