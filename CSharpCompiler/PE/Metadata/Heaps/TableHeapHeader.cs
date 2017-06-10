using System.Runtime.InteropServices;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TableHeapHeader
    {
        public uint Reserved;
        public byte MajorVersion;
        public byte MinorVersion;
        public HeapFlags HeapFlags;
        public byte Rid;
        public ulong MaskValid;
        public ulong MaskSorted;
    }
}
