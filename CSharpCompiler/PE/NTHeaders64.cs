using System.Runtime.InteropServices;

namespace CSharpCompiler.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NTHeaders64
    {
        public uint Signature;
        public FileHeader FileHeader;
        public OptionHeader64 OptionalHeader;
    }
}
