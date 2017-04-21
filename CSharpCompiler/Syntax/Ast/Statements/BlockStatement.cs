using System.Collections.Generic;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class BlockStatement : Statement
    {
        public List<Statement> Statements { get; private set; }

        public BlockStatement(List<Statement> statements)
        {
            Statements = statements;
        }

        public override void Build(MethodBuilder builder)
        {
            foreach (var statement in Statements)
            {
                statement.Build(builder);
            }
        }
    }
}
