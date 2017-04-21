using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public abstract class LoopStatement : Statement
    {
        /// <summary>
        /// Refence to first instruction after enclosure statement
        /// </summary>
        public InstructionReference AfterRefence { get; private set; }

        public LoopStatement()
        {
            AfterRefence = new InstructionReference();
        }
    }
}
