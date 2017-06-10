using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum PropertyAttributes : ushort
    {
        None = 0x0000,
        /// <summary>
        /// Property is special
        /// </summary>
        SpecialName = 0x0200,
        /// <summary>
        /// Runtime(metadata internal APIs) should check name encoding
        /// </summary>
        RTSpecialName = 0x0400,
        /// <summary>
        /// Property has default
        /// </summary>
        HasDefault = 0x1000,
        /// <summary>
        /// Reserved: shall be zero in a conforming implementation
        /// </summary>
        Unused = 0xe9ff
    }
}
