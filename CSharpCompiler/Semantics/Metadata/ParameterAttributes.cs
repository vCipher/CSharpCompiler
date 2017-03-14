using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum ParameterAttributes : ushort
    {
        None = 0x0000,
        /// <summary>
        /// Param is [In]
        /// </summary>
        In = 0x0001,
        /// <summary>
        /// Param is [Out]
        /// </summary>
        Out = 0x0002,
        /// <summary>
        /// Reserved flag for Runtime use only.
        /// </summary>
        Lcid = 0x0004,
        /// <summary>
        /// Reserved flag for Runtime use only.
        /// </summary>
        Retval = 0x0008,
        /// <summary>
        /// Param is optional
        /// </summary>
        Optional = 0x0010,
        /// <summary>
        /// Param has default value
        /// </summary>
        HasDefault = 0x1000,
        /// <summary>
        /// Param has field marshal
        /// </summary>
        HasFieldMarshal = 0x2000,
        /// <summary>
        /// Reserved: shall be zero in a conforming implementation
        /// </summary>
        Unused = 0xcfe0
    }
}
