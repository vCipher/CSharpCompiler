namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public abstract class Expression : AstNode
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }
}
