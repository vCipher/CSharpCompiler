using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ParamRow
    {
        public readonly ParameterAttributes Attributes;
        public readonly ushort Sequence;
        public readonly uint Name;

        public ParamRow(ParameterAttributes attributes, ushort sequence, uint name) : this()
        {
            Attributes = attributes;
            Sequence = sequence;
            Name = name;
        }
    }

    public sealed class ParamTable : MetadataTable<ParamRow>
    {
        public ParamTable() : base() { }
        public ParamTable(int count) : base(count) { }

        protected override void WriteRow(ParamRow row, TableHeapWriter heap)
        {
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteUInt16(row.Sequence);
            heap.WriteString(row.Name);
        }

        protected override ParamRow ReadRow(TableHeapReader heap)
        {
            return new ParamRow(
                (ParameterAttributes)heap.ReadUInt16(),
                heap.ReadUInt16(),
                heap.ReadStringOffset()
            );
        }
    }
}
