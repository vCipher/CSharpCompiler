namespace CSharpCompiler.Syntax.Ast.Statements
{
    public abstract class Statement : AstNode
    {
        public abstract void Accept(IStatementVisitor visitor);
    }
}
