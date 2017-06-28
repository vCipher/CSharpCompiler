using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ImplMapRow
    {
        public readonly MethodImplAttributes Attributes;
        public readonly MetadataToken MemberForwarded;
        public readonly uint ImportName;
        public readonly MetadataToken ImportScope;

        public ImplMapRow(MethodImplAttributes attributes, MetadataToken memberForwarded, uint importName, MetadataToken importScope) : this()
        {
            Attributes = attributes;
            MemberForwarded = memberForwarded;
            ImportName = importName;
            ImportScope = importScope;
        }
    }

    public sealed class ImplMapTable : MetadataTable<ImplMapRow>
    {
        public ImplMapTable() : base() { }
        public ImplMapTable(int count) : base(count) { }

        protected override ImplMapRow ReadRow(TableHeapReader heap)
        {
            return new ImplMapRow(
                (MethodImplAttributes)heap.ReadUInt16(),
                heap.ReadCodedToken(CodedTokenType.MemberForwarded),
                heap.ReadStringOffset(),
                heap.ReadToken(MetadataTokenType.ModuleRef)
            );
        }

        protected override void WriteRow(ImplMapRow row, TableHeapWriter heap)
        {
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteCodedToken(row.MemberForwarded, CodedTokenType.MemberForwarded);
            heap.WriteString(row.ImportName);
            heap.WriteToken(row.ImportScope);
        }
    }
}
