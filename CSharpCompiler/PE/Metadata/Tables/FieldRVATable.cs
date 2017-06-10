using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct FieldRVARow
    {
        public readonly uint RVA;
        public readonly MetadataToken Field;

        public FieldRVARow(uint rva, MetadataToken field) : this()
        {
            RVA = rva;
            Field = field;
        }
    }

    public sealed class FieldRVATable : MetadataTable<FieldRVARow>
    {
        public FieldRVATable() : base() { }
        public FieldRVATable(int count) : base(count) { }

        protected override FieldRVARow ReadRow(TableHeap heap)
        {
            return new FieldRVARow(
                heap.ReadUInt32(),
                heap.ReadToken(MetadataTokenType.Field)
            );
        }

        protected override void WriteRow(FieldRVARow row, TableHeap heap)
        {
            heap.WriteUInt32(row.RVA);
            heap.WriteToken(row.Field);
        }
    }
}

