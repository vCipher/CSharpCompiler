using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct PropertyMapRow
    {
        public readonly MetadataToken Parent;
        public readonly MetadataToken PropertyList;

        public PropertyMapRow(MetadataToken parent, MetadataToken propertyList) : this()
        {
            Parent = parent;
            PropertyList = propertyList;
        }
    }

    public sealed class PropertyMapTable : MetadataTable<PropertyMapRow>
    {
        public PropertyMapTable() : base() { }
        public PropertyMapTable(int count) : base(count) { }

        protected override PropertyMapRow ReadRow(TableHeap heap)
        {
            return new PropertyMapRow(
                heap.ReadToken(MetadataTokenType.TypeDef),
                heap.ReadToken(MetadataTokenType.Property)
            );
        }

        protected override void WriteRow(PropertyMapRow row, TableHeap heap)
        {
            heap.WriteToken(row.Parent);
            heap.WriteToken(row.PropertyList);
        }
    }
}
