using System;

namespace CSharpCompiler.PE.Sections.Text
{
    [Flags]
    public enum PEFileKinds : uint
    {
        /// <summary>
        /// not a PE file
        /// </summary>
        Not = 0x00000000,
        /// <summary>
        /// flag IL_ONLY is set in CLR header
        /// </summary>
        ILOnly = 0x00000001,
        /// <summary>
        /// flag 32BITREQUIRED is set and 32BITPREFERRED is clear in CLR header
        /// </summary>
        Required32Bit = 0x00000002,
        /// <summary>
        /// PE32+ file (64 bit)
        /// </summary>
        Plus32 = 0x00000004,
        /// <summary>
        /// PE32 without CLR header
        /// </summary>
        Unmanaged32 = 0x00000008,
        /// <summary>
        /// flags 32BITREQUIRED and 32BITPREFERRED are set in CLR header
        /// </summary>
        Preferred32Bit = 0x00000010
    }
}
