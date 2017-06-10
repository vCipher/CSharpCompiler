using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct FieldLayoutRow
    {
        public readonly uint Offset;
        public readonly MetadataToken Field;

        public FieldLayoutRow(uint offset, MetadataToken field) : this()
        {
            Offset = offset;
            Field = field;
        }
    }

    public sealed class FieldLayoutTable : MetadataTable<FieldLayoutRow>
    {
        public FieldLayoutTable() : base() { }
        public FieldLayoutTable(int count) : base(count) { }

        protected override FieldLayoutRow ReadRow(TableHeap heap)
        {
            return new FieldLayoutRow(
                heap.ReadUInt32(),
                heap.ReadToken(MetadataTokenType.Field)
            );
        }

        protected override void WriteRow(FieldLayoutRow row, TableHeap heap)
        {
            heap.WriteUInt32(row.Offset);
            heap.WriteToken(row.Field);
        }
    }
}
