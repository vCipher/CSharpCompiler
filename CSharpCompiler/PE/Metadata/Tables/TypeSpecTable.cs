using CSharpCompiler.PE.Metadata.Heaps;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct TypeSpecRow
    {
        public readonly uint Signature;

        public TypeSpecRow(uint signature) : this()
        {
            Signature = signature;
        }
    }

    public sealed class TypeSpecTable : MetadataTable<TypeSpecRow>
    {
        public TypeSpecTable() : base() { }
        public TypeSpecTable(int count) : base(count) { }

        protected override TypeSpecRow ReadRow(TableHeap heap)
        {
            return new TypeSpecRow(heap.ReadBlob());
        }

        protected override void WriteRow(TypeSpecRow row, TableHeap heap)
        {
            heap.WriteBlob(row.Signature);
        }
    }
}
