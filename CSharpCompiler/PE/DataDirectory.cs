using System.Runtime.InteropServices;

namespace CSharpCompiler.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataDirectory
    {
        public uint RVA;
        public uint Size;

        public DataDirectory(uint rva, uint size)
        {
            RVA = rva;
            Size = size;
        }

        public bool IsZero()
        {
            return RVA == 0 && Size == 0;
        }
    }
}
