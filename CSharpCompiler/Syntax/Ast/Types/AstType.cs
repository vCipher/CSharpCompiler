using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public abstract class AstType : AstNode
    {
        public abstract IType ToType();
    }
}
