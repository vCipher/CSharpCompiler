using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct FieldRow
    {
        public readonly FieldAttributes Attributes;
        public readonly uint Name;
        public readonly uint Signature;

        public FieldRow(FieldAttributes attributes, uint name, uint signature) : this()
        {
            Attributes = attributes;
            Name = name;
            Signature = signature;
        }
    }

    public sealed class FieldTable : MetadataTable<FieldRow>
    {
        public FieldTable() : base() { }
        public FieldTable(int count) : base(count) { }

        protected override FieldRow ReadRow(TableHeap heap)
        {
            return new FieldRow(
                (FieldAttributes)heap.ReadUInt16(),
                heap.ReadString(),
                heap.ReadBlob()
            );
        }

        protected override void WriteRow(FieldRow row, TableHeap heap)
        {
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteString(row.Name);
            heap.WriteBlob(row.Signature);
        }
    }
}
