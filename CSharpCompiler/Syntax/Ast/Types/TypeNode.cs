namespace CSharpCompiler.Syntax.Ast.Types
{
    public abstract class TypeNode : AstNode
    {
        public abstract void Accept(ITypeVisitor visitor);
    }
}
