namespace CSharpCompiler.Semantic.Cil
{
    public enum OpCodeType : byte
    {
        /// <summary>
        /// This enumerator value is reserved and should not be used.
        /// </summary>
        Annotation,
        /// <summary>
        /// These are Microsoft intermediate language (MSIL) instructions that are used as a synonym for other MSIL instructions. 
        /// For example, ldarg.0 represents the ldarg instruction with an argument of 0.
        /// </summary>
        Macro,
        /// <summary>
        /// Describes a reserved Microsoft intermediate language (MSIL) instruction.
        /// </summary>
        Nternal,
        /// <summary>
        /// Describes a Microsoft intermediate language (MSIL) instruction that applies to objects.
        /// </summary>
        Objmodel,
        /// <summary>
        /// Describes a prefix instruction that modifies the behavior of the following instruction.
        /// </summary>
        Prefix,
        /// <summary>
        /// Describes a built-in instruction.
        /// </summary>
        Primitive
    }
}
