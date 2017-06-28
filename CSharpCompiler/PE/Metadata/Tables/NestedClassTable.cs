using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct NestedClassRow
    {
        public readonly MetadataToken NestedClass;
        public readonly MetadataToken EnclosingClass;

        public NestedClassRow(MetadataToken nestedClass, MetadataToken enclosingClass) : this()
        {
            NestedClass = nestedClass;
            EnclosingClass = enclosingClass;
        }
    }

    public sealed class NestedClassTable : MetadataTable<NestedClassRow>
    {
        public NestedClassTable() : base() { }
        public NestedClassTable(int count) : base(count) { }

        protected override NestedClassRow ReadRow(TableHeapReader heap)
        {
            return new NestedClassRow(
                heap.ReadToken(MetadataTokenType.TypeDef),
                heap.ReadToken(MetadataTokenType.TypeDef)
            );
        }

        protected override void WriteRow(NestedClassRow row, TableHeapWriter heap)
        {
            heap.WriteToken(row.NestedClass);
            heap.WriteToken(row.EnclosingClass);
        }
    }
}
