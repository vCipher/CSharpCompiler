using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public abstract class Statement : AstNode
    {
        public abstract void Build(MethodBuilder builder);
    }
}
