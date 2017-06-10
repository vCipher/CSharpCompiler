using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct AssemblyRefRow
    {
        public readonly ushort MajorVersion;
        public readonly ushort MinorVersion;
        public readonly ushort BuildNumber;
        public readonly ushort RevisionNumber;
        public readonly AssemblyAttributes Attributes;
        public readonly uint PublicKeyOrToken;
        public readonly uint Name;
        public readonly uint Culture;
        public readonly uint HashValue;

        public AssemblyRefRow(
            ushort majorVersion, 
            ushort minorVersion, 
            ushort buildNumber, 
            ushort revisionNumber, 
            AssemblyAttributes attributes, 
            uint publicKeyOrToken, 
            uint name, 
            uint culture, 
            uint hashValue) : this()
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            BuildNumber = buildNumber;
            RevisionNumber = revisionNumber;
            Attributes = attributes;
            PublicKeyOrToken = publicKeyOrToken;
            Name = name;
            Culture = culture;
            HashValue = hashValue;
        }
    }

    public sealed class AssemblyRefTable : MetadataTable<AssemblyRefRow>
    {
        public AssemblyRefTable() : base() { }
        public AssemblyRefTable(int count) : base(count) { }
        
        protected override AssemblyRefRow ReadRow(TableHeap heap)
        {
            return new AssemblyRefRow(
                heap.ReadUInt16(),
                heap.ReadUInt16(),
                heap.ReadUInt16(),
                heap.ReadUInt16(),
                (AssemblyAttributes)heap.ReadUInt32(),
                heap.ReadBlob(),
                heap.ReadString(),
                heap.ReadString(),
                heap.ReadBlob()
            );
        }

        protected override void WriteRow(AssemblyRefRow row, TableHeap heap)
        {
            heap.WriteUInt16(row.MajorVersion);
            heap.WriteUInt16(row.MinorVersion);
            heap.WriteUInt16(row.BuildNumber);
            heap.WriteUInt16(row.RevisionNumber);
            heap.WriteUInt32((uint)row.Attributes);
            heap.WriteBlob(row.PublicKeyOrToken);
            heap.WriteString(row.Name);
            heap.WriteString(row.Culture);
            heap.WriteBlob(row.HashValue);
        }
    }
}
