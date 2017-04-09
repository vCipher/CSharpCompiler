namespace CSharpCompiler.Semantics.Cil
{
    public enum FlowControl : byte
    {
        /// <summary>
        /// Branch instruction.
        /// </summary>
        Branch,
        /// <summary>
        /// Break instruction.
        /// </summary>
        Break,
        /// <summary>
        /// Call instruction.
        /// </summary>
        Call,
        /// <summary>
        /// Conditional branch instruction.
        /// </summary>
        Cond_Branch,
        /// <summary>
        /// Provides information about a subsequent instruction. For example, 
        /// the Unaligned instruction of Reflection.Emit.Opcodes has FlowControl.Meta 
        /// and specifies that the subsequent pointer instruction might be unaligned.
        /// </summary>
        Meta,
        /// <summary>
        /// Normal flow of control.
        /// </summary>
        Next,
        /// <summary>
        /// This enumerator value is reserved and should not be used.
        /// </summary>
        Phi,
        /// <summary>
        /// Return instruction.
        /// </summary>
        Return,
        /// <summary>
        /// Exception throw instruction.
        /// </summary>
        Throw
    }
}
