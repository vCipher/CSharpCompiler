namespace CSharpCompiler.Syntax.Ast.Types
{
    public interface ITypeVisitor
    {
        void VisitArrayType(ArrayTypeNode node);
        void VisitPrimitiveType(PrimitiveTypeNode node);
    }
}
