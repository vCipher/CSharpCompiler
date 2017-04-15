using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public abstract class TypeNode : AstNode
    {
        public abstract ITypeInfo ToType();
    }
}
