using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum MethodImplAttributes : ushort
    {
        /// <summary>
        /// Flags about code type.
        /// </summary>
        CodeTypeMask = 0x0003,
        /// <summary>
        /// Method impl is CIL
        /// </summary>
        IL = 0x0000,
        /// <summary>
        /// Method impl is native.
        /// </summary>
        Native = 0x0001,
        /// <summary>
        /// Reserved: shall be zero in conforming implementations.
        /// </summary>
        OPTIL = 0x0002,
        /// <summary>
        /// Method impl is provided by the runtime.
        /// </summary>
        Runtime = 0x0003,

        /// <summary>
        /// Flags specifying whether the code is managed or unmanaged.
        /// </summary>
        ManagedMask = 0x0004,
        /// <summary>
        /// Method impl is unmanaged, otherwise managed.
        /// </summary>
        Unmanaged = 0x0004,
        /// <summary>
        /// Method impl is managed.
        /// </summary>
        Managed = 0x0000,

        /// <summary>
        /// Indicates method is defined; used primarily in merge scenarios.
        /// </summary>
        ForwardRef = 0x0010,
        /// <summary>
        /// Reserved: conforming implementations may ignore.
        /// </summary>
        PreserveSig = 0x0080,
        /// <summary>
        /// Reserved: shall be zero in conforming implementations.
        /// </summary>
        InternalCall = 0x1000,
        /// <summary>
        /// Method is single threaded through the body.
        /// </summary>
        Synchronized = 0x0020,
        /// <summary>
        /// Method is not optimized by the JIT.
        /// </summary>
        NoOptimization = 0x0040,
        /// <summary>
        /// Method may not be inlined.
        /// </summary>
        NoInlining = 0x0008
    }
}
