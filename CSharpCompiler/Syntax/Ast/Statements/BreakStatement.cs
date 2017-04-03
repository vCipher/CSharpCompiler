namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class BreakStatement : Statement
    {
        public LoopStatement Enclosure { get; private set; }

        public BreakStatement(LoopStatement enclosure)
        {
            Enclosure = enclosure;
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.VisitBreakStatement(this);
        }
    }
}
