using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct MethodSemanticsRow
    {
        public readonly MethodSemanticsAttributes Attributes;
        public readonly MetadataToken Method;
        public readonly MetadataToken Association;

        public MethodSemanticsRow(MethodSemanticsAttributes attributes, MetadataToken method, MetadataToken association) : this()
        {
            Attributes = attributes;
            Method = method;
            Association = association;
        }
    }

    public sealed class MethodSemanticsTable : MetadataTable<MethodSemanticsRow>
    {
        public MethodSemanticsTable() : base() { }
        public MethodSemanticsTable(int count) : base(count) { }

        protected override MethodSemanticsRow ReadRow(TableHeapReader heap)
        {
            return new MethodSemanticsRow(
                (MethodSemanticsAttributes)heap.ReadUInt16(),
                heap.ReadToken(MetadataTokenType.Method),
                heap.ReadCodedToken(CodedTokenType.HasSemantics)
            );
        }

        protected override void WriteRow(MethodSemanticsRow row, TableHeapWriter heap)
        {
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteToken(row.Method);
            heap.WriteCodedToken(row.Association, CodedTokenType.HasSemantics);
        }
    }
}

