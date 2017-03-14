using CSharpCompiler.Syntax.Ast.Statements;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class SyntaxTree : AstNode
    {
        public List<Stmt> Statements { get; private set; }

        public SyntaxTree(List<Stmt> statements)
        {
            Statements = statements;
        }

        public SyntaxTree(params Stmt[] statements)
        {
            Statements = new List<Stmt>(statements);
        }
    }
}
