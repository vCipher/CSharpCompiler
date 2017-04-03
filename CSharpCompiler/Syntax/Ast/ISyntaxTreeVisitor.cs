namespace CSharpCompiler.Syntax.Ast
{
    public interface ISyntaxTreeVisitor
    {
        void VisitSyntaxTree(SyntaxTree node);
    }
}
