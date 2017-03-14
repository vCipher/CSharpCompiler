using CSharpCompiler.CodeGen.Metadata.Heaps;
using System.Collections;

namespace CSharpCompiler.CodeGen.Metadata.Tables
{
    public interface IMetadataTable : IEnumerable
    {
        int Length { get; }
        void Write(TableHeap heap);
    }
}
