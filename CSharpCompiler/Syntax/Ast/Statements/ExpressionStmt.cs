using System;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax.Ast.Expressions;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class ExpressionStmt : Stmt
    {
        public Expression Expression { get; set; }

        public ExpressionStmt(Expression expr)
        {
            Expression = expr;
        }

        public static implicit operator Expression(ExpressionStmt stmt)
        {
            return stmt.Expression;
        }

        public static implicit operator ExpressionStmt(Expression expr)
        {
            return new ExpressionStmt(expr);
        }

        public override void Build(MethodBuilder builder)
        {
            Expression.Build(builder);
        }
    }
}
