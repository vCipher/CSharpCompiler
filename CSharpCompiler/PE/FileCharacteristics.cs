using System;

namespace CSharpCompiler.PE
{
    [Flags]
    public enum FileCharacteristics : ushort
    {
        RELOCS_STRIPPED = 0x0001,
        EXECUTABLE_IMAGE = 0x0002,
        LINE_NUMS_STRIPPED = 0x0004,
        LOCAL_SYMS_STRIPPED = 0x0008,
        AGGRESIVE_WS_TRIM = 0x0010,
        LARGE_ADDRESS_AWARE = 0x0020,
        MACHINE_16BIT = 0x0040,
        BYTES_REVERSED_LO = 0x0080,
        MACHINE_32BIT = 0x0100,
        DEBUG_STRIPPED = 0x0200,
        REMOVABLE_RUN_FROM_SWAP = 0x0400,
        NET_RUN_FROM_SWAP = 0x0800,
        SYSTEM = 0x1000,
        DLL = 0x2000,
        UP_SYSTEM_ONLY = 0x4000,
        BYTES_REVERSED_HI = 0x8000
    }
}
