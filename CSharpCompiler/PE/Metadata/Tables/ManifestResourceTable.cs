using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ManifestResourceRow
    {
        public readonly uint Offset;
        public readonly ManifestResourceAttributes Attributes;
        public readonly uint Name;
        public readonly MetadataToken Implementation;

        public ManifestResourceRow(uint offset, ManifestResourceAttributes attributes, uint name, MetadataToken implementation) : this()
        {
            Offset = offset;
            Attributes = attributes;
            Name = name;
            Implementation = implementation;
        }
    }

    public sealed class ManifestResourceTable : MetadataTable<ManifestResourceRow>
    {
        public ManifestResourceTable() : base() { }
        public ManifestResourceTable(int count) : base(count) { }

        protected override ManifestResourceRow ReadRow(TableHeapReader heap)
        {
            return new ManifestResourceRow(
                heap.ReadUInt32(),
                (ManifestResourceAttributes)heap.ReadUInt32(),
                heap.ReadStringOffset(),
                heap.ReadCodedToken(CodedTokenType.Implementation)
            );
        }

        protected override void WriteRow(ManifestResourceRow row, TableHeapWriter heap)
        {
            heap.WriteUInt32(row.Offset);
            heap.WriteUInt32((uint)row.Attributes);
            heap.WriteString(row.Name);
            heap.WriteCodedToken(row.Implementation, CodedTokenType.Implementation);
        }
    }
}
