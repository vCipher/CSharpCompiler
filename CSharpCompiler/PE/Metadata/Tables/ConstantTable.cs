using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ConstantRow
    {
        public readonly ElementType Type;
        public readonly MetadataToken Parent;
        public readonly uint Value;

        public ConstantRow(ElementType type, MetadataToken parent, uint value) : this()
        {
            Type = type;
            Parent = parent;
            Value = value;
        }
    }

    public sealed class ConstantTable : MetadataTable<ConstantRow>
    {
        public ConstantTable() : base() { }
        public ConstantTable(int count) : base(count) { }

        protected override ConstantRow ReadRow(TableHeapReader heap)
        {
            return new ConstantRow(
                (ElementType)heap.ReadUInt16(),
                heap.ReadCodedToken(CodedTokenType.HasConstant),
                heap.ReadBlobOffset()
            );
        }

        protected override void WriteRow(ConstantRow row, TableHeapWriter heap)
        {
            heap.WriteUInt16((ushort)row.Type);
            heap.WriteCodedToken(row.Parent, CodedTokenType.HasConstant);
            heap.WriteBlob(row.Value);
        }
    }
}
