using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public abstract class Expression : AstNode
    {
        public abstract ITypeInfo InferType();
        public abstract void Build(MethodBuilder builder);
    }
}
