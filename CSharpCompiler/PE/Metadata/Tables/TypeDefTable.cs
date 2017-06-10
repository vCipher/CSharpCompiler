using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct TypeDefRow
    {
        public readonly TypeAttributes Attributes;
        public readonly uint Name;
        public readonly uint Namespace;
        public readonly MetadataToken Extends;
        public readonly ushort FieldList;
        public readonly ushort MethodList;

        public TypeDefRow(
            TypeAttributes attributes,
            uint name,
            uint @namespace,
            MetadataToken extends,
            ushort fieldList,
            ushort methodList) : this()
        {
            Attributes = attributes;
            Name = name;
            Namespace = @namespace;
            Extends = extends;
            FieldList = fieldList;
            MethodList = methodList;
        }
    }

    public sealed class TypeDefTable : MetadataTable<TypeDefRow>
    {
        public TypeDefTable() : base() { }
        public TypeDefTable(int count) : base(count) { }

        protected override TypeDefRow ReadRow(TableHeap heap)
        {
            return new TypeDefRow(
                (TypeAttributes)heap.ReadUInt32(),
                heap.ReadString(),
                heap.ReadString(),
                heap.ReadCodedToken(CodedTokenType.TypeDefOrRef),
                heap.ReadUInt16(),
                heap.ReadUInt16()
            );
        }

        protected override void WriteRow(TypeDefRow row, TableHeap heap)
        {
            heap.WriteUInt32((uint)row.Attributes);
            heap.WriteString(row.Name);
            heap.WriteString(row.Namespace);
            heap.WriteCodedToken(row.Extends, CodedTokenType.TypeDefOrRef);
            heap.WriteUInt16(row.FieldList);
            heap.WriteUInt16(row.MethodList);
        }
    }
}
