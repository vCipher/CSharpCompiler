using CSharpCompiler.PE.Metadata.Heaps;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataHeaps
    {
        public GuidHeap Guids { get; set; }
        public BlobHeap Blobs { get; set; }
        public StringHeap Strings { get; set; }
        public UserStringHeap UserStrings { get; set; }
        public TableHeapWriter Tables { get; set; }

        public MetadataHeaps()
        {
            Guids = new GuidHeap();
            Blobs = new BlobHeap(new byte[] { 0x00 });
            Strings = new StringHeap(new byte[] { 0x00 });
            UserStrings = new UserStringHeap(new byte[] { 0x00 });
            Tables = new TableHeapWriter(this);
        }

        public HeapFlags GetHeapFlags()
        {
            HeapFlags flags = HeapFlags.None;
            if (Strings.IsLarge) flags |= HeapFlags.BigStrings;
            if (Guids.IsLarge) flags |= HeapFlags.BigGUID;
            if (Blobs.IsLarge) flags |= HeapFlags.BigBlob;

            return flags;
        }
    }
}
