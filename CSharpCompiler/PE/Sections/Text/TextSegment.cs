namespace CSharpCompiler.PE.Sections.Text
{
    public enum TextSegment
    {
        ImportAddressTable,
        CLRHeader,
        ILCode,
        Resources,
        Data,
        StrongNameSignature,

        // Metadata
        MetadataHeader,
        TableHeap,
        StringHeap,
        UserStringHeap,
        GuidHeap,
        BlobHeap,
        // End Metadata

        DebugDirectory,
        ImportDirectory,
        ImportHintNameTable,
        StartupStub
    }
}
