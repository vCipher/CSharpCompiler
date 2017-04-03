namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public interface IArgumentVisitor
    {
        void VisitArgument(Argument node);
    }
}
