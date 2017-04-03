using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class BlockStatement : Statement
    {
        public IList<Statement> Statements { get; private set; }

        public BlockStatement(IList<Statement> statements)
        {
            Statements = statements;
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.VisitBlockStatement(this);
        }
    }
}
