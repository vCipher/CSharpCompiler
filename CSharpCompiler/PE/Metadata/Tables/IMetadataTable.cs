using CSharpCompiler.PE.Metadata.Heaps;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public interface IMetadataTable
    {
        int Length { get; }
        void Write(TableHeapWriter heap);
        void Read(TableHeapReader heap);
    }
}
