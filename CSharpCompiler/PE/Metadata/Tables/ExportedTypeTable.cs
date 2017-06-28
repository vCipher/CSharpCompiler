using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ExportedTypeRow
    {
        public readonly TypeAttributes Attributes;
        public readonly uint TypeDefId;
        public readonly uint TypeName;
        public readonly uint TypeNamespace;
        public readonly MetadataToken Implementation;

        public ExportedTypeRow(TypeAttributes attributes, uint typeDefId, uint typeName, uint typeNamespace, MetadataToken implementation) : this()
        {
            Attributes = attributes;
            TypeDefId = typeDefId;
            TypeName = typeName;
            TypeNamespace = typeNamespace;
            Implementation = implementation;
        }
    }

    public sealed class ExportedTypeTable : MetadataTable<ExportedTypeRow>
    {
        public ExportedTypeTable() : base() { }
        public ExportedTypeTable(int count) : base(count) { }

        protected override ExportedTypeRow ReadRow(TableHeapReader heap)
        {
            return new ExportedTypeRow(
                (TypeAttributes)heap.ReadUInt32(),
                heap.ReadUInt32(),
                heap.ReadStringOffset(),
                heap.ReadStringOffset(),
                heap.ReadCodedToken(CodedTokenType.Implementation)
            );
        }

        protected override void WriteRow(ExportedTypeRow row, TableHeapWriter heap)
        {
            heap.WriteUInt32((uint)row.Attributes);
            heap.WriteUInt32(row.TypeDefId);
            heap.WriteString(row.TypeName);
            heap.WriteString(row.TypeNamespace);
            heap.WriteCodedToken(row.Implementation, CodedTokenType.Implementation);
        }
    }
}
