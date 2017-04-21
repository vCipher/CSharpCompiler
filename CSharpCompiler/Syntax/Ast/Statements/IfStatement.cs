using System;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
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

        public override void Build(MethodBuilder builder)
        {
            BuildCondition(builder);
            BuildBody(builder);
        }

        private void BuildCondition(MethodBuilder builder)
        {
            Condition.Build(builder);
            builder.Emit(OpCodes.Brfalse, AfterRefence);
        }

        private void BuildBody(MethodBuilder builder)
        {
            Body.Build(builder);
            builder.ResolveOnNextStep(AfterRefence);
        }
    }
}
