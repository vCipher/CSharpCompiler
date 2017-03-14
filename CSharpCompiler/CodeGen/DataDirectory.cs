using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataDirectory
    {
        public uint VirtualAddress;
        public uint Size;

        public DataDirectory(uint rva, uint size)
        {
            VirtualAddress = rva;
            Size = size;
        }
    }
}
