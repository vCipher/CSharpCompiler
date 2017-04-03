namespace CSharpCompiler.Syntax.Ast.Variables
{
    public interface IVarDeclarationVisitor
    {
        void VisitVarDeclaration(VarDeclaration node);
    }
}
