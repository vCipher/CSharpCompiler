using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class BreakStatement : Statement
    {
        public LoopStatement Enclosure { get; private set; }

        public BreakStatement(LoopStatement enclosure)
        {
            Enclosure = enclosure;
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Emit(OpCodes.Br, Enclosure.AfterRefence);
        }
    }
}
