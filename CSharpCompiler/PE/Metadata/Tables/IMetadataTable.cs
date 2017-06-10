using CSharpCompiler.PE.Metadata.Heaps;
using System.Collections;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public interface IMetadataTable
    {
        int Length { get; }
        void Write(TableHeap heap);
        void Read(TableHeap heap);
    }
}
