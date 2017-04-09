namespace CSharpCompiler.Semantics.Cil
{
    public sealed class InstructionReference : IInstructionReference
    {
        private Instruction _instruction;

        public int Offset
        {
            get
            {
                if (_instruction == null) throw new SemanticException("Instruction reference did not resolved");
                return _instruction.Offset;
            }
        }

        public void Resolve(Instruction instruction)
        {
            _instruction = instruction;
        }
    }
}
