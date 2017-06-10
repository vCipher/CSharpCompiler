using System;

namespace CSharpCompiler.PE
{
    [Flags]
    public enum DllCharacteristics : ushort
    {
        DYNAMIC_BASE = 0x0040,
        FORCE_INTEGRITY = 0x0080,
        NX_COMPAT = 0x0100,
        NO_ISOLATION = 0x0200,
        NO_SEH = 0x0400,
        NO_BIND = 0x0800,
        WDM_DRIVER = 0x2000,
        TERMINAL_SERVER_AWARE = 0x8000
    }
}
