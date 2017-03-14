using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TableHeapHeader
    {
        public uint Reserved1;
        public byte MajorVersion;
        public byte MinorVersion;
        public byte HeapOffsetSizes;
        public byte Reserved2;
        public ulong MaskValid;
        public ulong MaskSorted;
    }
}
