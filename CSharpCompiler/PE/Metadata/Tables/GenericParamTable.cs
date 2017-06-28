using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct GenericParamRow
    {
        public readonly ushort Number;
        public readonly GenericParameterAttributes Attributes;
        public readonly MetadataToken Owner;
        public readonly uint Name;

        public GenericParamRow(ushort number, GenericParameterAttributes attributes, MetadataToken owner, uint name) : this()
        {
            Number = number;
            Attributes = attributes;
            Owner = owner;
            Name = name;
        }
    }

    public sealed class GenericParamTable : MetadataTable<GenericParamRow>
    {
        public GenericParamTable() : base() { }
        public GenericParamTable(int count) : base(count) { }

        protected override GenericParamRow ReadRow(TableHeapReader heap)
        {
            return new GenericParamRow(
                heap.ReadUInt16(),
                (GenericParameterAttributes)heap.ReadUInt16(),
                heap.ReadCodedToken(CodedTokenType.TypeOrMethodDef),
                heap.ReadStringOffset()
            );
        }

        protected override void WriteRow(GenericParamRow row, TableHeapWriter heap)
        {
            heap.WriteUInt16(row.Number);
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteCodedToken(row.Owner, CodedTokenType.TypeOrMethodDef);
            heap.WriteString(row.Name);
        }
    }
}