using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct CustomAttributeRow
    {
        public readonly MetadataToken Parent;
        public readonly MetadataToken Type;
        public readonly uint Value;

        public CustomAttributeRow(MetadataToken parent, MetadataToken type, uint value) : this()
        {
            Parent = parent;
            Type = type;
            Value = value;
        }
    }

    public sealed class CustomAttributeTable : MetadataTable<CustomAttributeRow>
    {
        public CustomAttributeTable() : base() { }
        public CustomAttributeTable(int count) : base(count) { }
        
        protected override CustomAttributeRow ReadRow(TableHeap heap)
        {
            return new CustomAttributeRow(
                heap.ReadCodedToken(CodedTokenType.HasCustomAttribute),
                heap.ReadCodedToken(CodedTokenType.CustomAttributeType),
                heap.ReadBlob()
            );
        }

        protected override void WriteRow(CustomAttributeRow row, TableHeap heap)
        {
            heap.WriteCodedToken(row.Parent, CodedTokenType.HasCustomAttribute);
            heap.WriteCodedToken(row.Type, CodedTokenType.CustomAttributeType);
            heap.WriteBlob(row.Value);
        }
    }
}
