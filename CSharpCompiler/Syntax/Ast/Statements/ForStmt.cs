using System.Collections.Generic;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class ForStmt : Stmt
    {
        public List<VarDeclaration> Declarations { get; private set; }
        public Expression Condition { get; private set; }
        public Expression PostIteration { get; private set; }
        public List<Stmt> Body { get; private set; }

        public ForStmt(List<VarDeclaration> declarations, Expression condition, Expression postIteration, List<Stmt> body)
        {
            Declarations = declarations;
            Condition = condition;
            PostIteration = postIteration;
            Body = body;
        }

        public override void Build(MethodBuilder builder)
        {
            var bodyRef = new InstructionReference();
            var conditionRef = new InstructionReference();

            foreach (var declaration in Declarations)
                declaration.Build(builder);

            builder.Emit(OpCodes.Br_S, conditionRef);
            builder.Resolve(bodyRef);

            foreach (var stmt in Body)
                stmt.Build(builder);

            PostIteration.Build(builder);
            builder.Resolve(conditionRef);
            Condition.Build(builder);

            builder.Emit(OpCodes.Brtrue_S, bodyRef);
        }
    }
}
