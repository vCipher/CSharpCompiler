using CSharpCompiler.Syntax.Ast.Expressions;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }

        public ExpressionStatement(Expression expr)
        {
            Expression = expr;
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.VisitExpressionStatement(this);
        }

        public static implicit operator Expression(ExpressionStatement Statement)
        {
            return Statement.Expression;
        }

        public static implicit operator ExpressionStatement(Expression expr)
        {
            return new ExpressionStatement(expr);
        }
    }
}
