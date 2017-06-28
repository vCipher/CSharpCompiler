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

        protected override TypeSpecRow ReadRow(TableHeapReader heap)
        {
            return new TypeSpecRow(heap.ReadBlobOffset());
        }

        protected override void WriteRow(TypeSpecRow row, TableHeapWriter heap)
        {
            heap.WriteBlob(row.Signature);
        }
    }
}
