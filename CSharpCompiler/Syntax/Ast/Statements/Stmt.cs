using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public abstract class Stmt : AstNode
    {
        public abstract void Build(MethodBuilder builder);
    }
}
