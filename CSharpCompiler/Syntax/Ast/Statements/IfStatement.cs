using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Syntax.Ast.Expressions;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class IfStatement : Statement
    {
        public Expression Condition { get; private set; }
        public Statement Body { get; private set; }
        public InstructionReference AfterRefence { get; private set; }

        public IfStatement(Expression condition, Statement body)
        {
            Condition = condition;
            Body = body;
            AfterRefence = new InstructionReference();
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.VisitIfStatement(this);
        }
    }
}
