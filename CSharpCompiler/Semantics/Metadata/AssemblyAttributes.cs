using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum AssemblyAttributes : uint
    {
        /// <summary>
        /// The assembly ref holds the full (unhashed) public key
        /// </summary>
        PublicKey = 0x0001,
        /// <summary>
        /// Processor Architecture unspecified
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Processor Architecture: neutral (PE32)
        /// </summary>
        MSIL = 0x0010,
        /// <summary>
        /// Processor Architecture: x86 (PE32)
        /// </summary>
        x86 = 0x0020,
        /// <summary>
        /// Processor Architecture: Itanium (PE32+)
        /// </summary>
        IA64 = 0x0030,
        /// <summary>
        /// Processor Architecture: AMD X64 (PE32+)
        /// </summary>
        AMD64 = 0x0040,
        /// <summary>
        /// Processor Architecture: ARM (PE32)
        /// </summary>
        ARM = 0x0050,
        /// <summary>
        /// From "DebuggableAttribute"
        /// </summary>
        EnableJITcompileTracking = 0x8000,
        /// <summary>
        /// From "DebuggableAttribute"
        /// </summary>
        DisableJITcompileOptimizer = 0x4000,
        /// <summary>
        /// The assembly can be retargeted (at runtime) to an assembly from a different publisher
        /// </summary>
        Retargetable = 0x0100
    }
}
