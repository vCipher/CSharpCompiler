using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct MethodRow
    {
        public readonly uint RVA;
        public readonly MethodImplAttributes ImplAttributes;
        public readonly MethodAttributes Attributes;
        public readonly uint Name;
        public readonly uint Signature;
        public readonly ushort ParamList;

        public MethodRow(
            uint rva,
            MethodImplAttributes implAttributes,
            MethodAttributes attributes,
            uint name,
            uint signature,
            ushort paramList) : this()
        {
            RVA = rva;
            ImplAttributes = implAttributes;
            Attributes = attributes;
            Name = name;
            Signature = signature;
            ParamList = paramList;
        }
    }

    public sealed class MethodTable : MetadataTable<MethodRow>
    {
        public MethodTable() : base() { }
        public MethodTable(int count) : base(count) { }

        protected override MethodRow ReadRow(TableHeapReader heap)
        {
            return new MethodRow(
                heap.ReadUInt32(),
                (MethodImplAttributes)heap.ReadUInt16(),
                (MethodAttributes)heap.ReadUInt16(),
                heap.ReadStringOffset(),
                heap.ReadBlobOffset(),
                heap.ReadUInt16()
            );
        }

        protected override void WriteRow(MethodRow row, TableHeapWriter heap)
        {
            heap.WriteUInt32(row.RVA);
            heap.WriteUInt16((ushort)row.ImplAttributes);
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteString(row.Name);
            heap.WriteBlob(row.Signature);
            heap.WriteUInt16(row.ParamList);
        }
    }
}
