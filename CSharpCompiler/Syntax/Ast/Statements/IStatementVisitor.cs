namespace CSharpCompiler.Syntax.Ast.Statements
{
    public interface IStatementVisitor
    {
        void VisitBlockStatement(BlockStatement node);
        void VisitBreakStatement(BreakStatement node);
        void VisitDeclarationStatement(DeclarationStatement node);
        void VisitExpressionStatement(ExpressionStatement node);
        void VisitForStatement(ForStatement node);
        void VisitIfStatement(IfStatement node);
    }
}
