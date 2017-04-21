using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax.Ast.Expressions;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class ForStatement : LoopStatement
    {
        public List<VarDeclaration> Declarations { get; private set; }
        public Expression Condition { get; private set; }
        public Expression PostIteration { get; private set; }
        public Statement Body { get; private set; }
        public InstructionReference BodyReference { get; private set; }
        public InstructionReference ConditionalReference { get; private set; }

        public ForStatement() : base()
        {
            BodyReference = new InstructionReference();
            ConditionalReference = new InstructionReference();
        }

        public void Init(List<VarDeclaration> declarations, Expression condition, Expression postIteration, Statement body)
        {
            Declarations = declarations;
            Condition = condition;
            PostIteration = postIteration;
            Body = body;
        }

        public override void Build(MethodBuilder builder)
        {
            BuildVarDeclarations(builder);
            BuildGoto(builder, ConditionalReference);
            BuildBody(builder);
            BuildPostIteration(builder);
            BuildCondition(builder);
            builder.ResolveOnNextStep(AfterRefence);
        }

        private void BuildVarDeclarations(MethodBuilder builder)
        {
            foreach (var declaration in Declarations)
            {
                declaration.Build(builder);
            }
        }

        private void BuildCondition(MethodBuilder builder)
        {
            builder.ResolveOnNextStep(ConditionalReference);
            Condition.Build(builder);
            builder.Emit(OpCodes.Brtrue, BodyReference);
        }

        private void BuildPostIteration(MethodBuilder builder)
        {
            PostIteration.Build(builder);
        }

        private void BuildBody(MethodBuilder builder)
        {
            builder.ResolveOnNextStep(BodyReference);
            Body.Build(builder);
        }

        private void BuildGoto(MethodBuilder builder, InstructionReference reference)
        {
            builder.Emit(OpCodes.Br, reference);
        }
    }
}
