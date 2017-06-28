using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct AssemblyRow
    {
        public readonly AssemblyHashAlgorithm HashAlgId;
        public readonly ushort MajorVersion;
        public readonly ushort MinorVersion;
        public readonly ushort BuildNumber;
        public readonly ushort RevisionNumber;
        public readonly AssemblyAttributes Attributes;
        public readonly uint PublicKey;
        public readonly uint Name;
        public readonly uint Culture;

        public AssemblyRow(
            AssemblyHashAlgorithm hashAlgId,
            ushort majorVersion,
            ushort minorVersion,
            ushort buildNumber,
            ushort revisionNumber,
            AssemblyAttributes attributes,
            uint publicKey,
            uint name,
            uint culture) : this()
        {
            HashAlgId = hashAlgId;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            BuildNumber = buildNumber;
            RevisionNumber = revisionNumber;
            Attributes = attributes;
            PublicKey = publicKey;
            Name = name;
            Culture = culture;
        }
    }

    public sealed class AssemblyTable : OneRowMetadataTable<AssemblyRow>
    {
        public AssemblyTable() : base() { }
        public AssemblyTable(int count) : base(count) { }
        
        protected override AssemblyRow ReadRow(TableHeapReader heap)
        {
            return new AssemblyRow(
                (AssemblyHashAlgorithm)heap.ReadUInt32(),
                heap.ReadUInt16(),
                heap.ReadUInt16(),
                heap.ReadUInt16(),
                heap.ReadUInt16(),
                (AssemblyAttributes)heap.ReadUInt32(),
                heap.ReadBlobOffset(),
                heap.ReadStringOffset(),
                heap.ReadStringOffset()
            );
        }

        protected override void WriteRow(AssemblyRow row, TableHeapWriter heap)
        {
            heap.WriteUInt32((uint)row.HashAlgId);
            heap.WriteUInt16(row.MajorVersion);
            heap.WriteUInt16(row.MinorVersion);
            heap.WriteUInt16(row.BuildNumber);
            heap.WriteUInt16(row.RevisionNumber);
            heap.WriteUInt32((uint)row.Attributes);
            heap.WriteBlob(row.PublicKey);
            heap.WriteString(row.Name);
            heap.WriteString(row.Culture);
        }
    }
}
