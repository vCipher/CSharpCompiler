using CSharpCompiler.Syntax.Ast;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public class Stmt : AstNode
    {
        public static readonly Stmt Empty = new Stmt();
    }
}
