using CSharpCompiler.Syntax.Ast.Statements;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class SyntaxTree : AstNode
    {
        public List<Statement> Statements { get; private set; }

        public SyntaxTree(List<Statement> statements)
        {
            Statements = statements;
        }

        public SyntaxTree(params Statement[] statements)
        {
            Statements = new List<Statement>(statements);
        }
    }
}
