using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Variables;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class ForStatement : LoopStatement
    {
        public IList<VarDeclaration> Declarations { get; private set; }
        public Expression Condition { get; private set; }
        public Expression PostIteration { get; private set; }
        public Statement Body { get; private set; }
        public InstructionReference BodyReference { get; private set; }
        public InstructionReference ConditionalReference { get; private set; }

        public ForStatement()
        {
            AfterRefence = new InstructionReference();
            BodyReference = new InstructionReference();
            ConditionalReference = new InstructionReference();
        }

        public void Init(
            IList<VarDeclaration> declarations, 
            Expression condition, 
            Expression postIteration, 
            Statement body)
        {
            Declarations = declarations;
            Condition = condition;
            PostIteration = postIteration;
            Body = body;
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.VisitForStatement(this);
        }
    }
}
