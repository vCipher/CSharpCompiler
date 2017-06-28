using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct MemberRefRow
    {
        public readonly MetadataToken Class;
        public readonly uint Name;
        public readonly uint Signature;

        public MemberRefRow(MetadataToken @class, uint name, uint signature) : this()
        {
            Class = @class;
            Name = name;
            Signature = signature;
        }
    }

    public sealed class MemberRefTable : MetadataTable<MemberRefRow>
    {
        public MemberRefTable() : base() { }
        public MemberRefTable(int count) : base(count) { }

        protected override MemberRefRow ReadRow(TableHeapReader heap)
        {
            return new MemberRefRow(
                heap.ReadCodedToken(CodedTokenType.MemberRefParent),
                heap.ReadStringOffset(),
                heap.ReadBlobOffset()
            );
        }

        protected override void WriteRow(MemberRefRow row, TableHeapWriter heap)
        {
            heap.WriteCodedToken(row.Class, CodedTokenType.MemberRefParent);
            heap.WriteString(row.Name);
            heap.WriteBlob(row.Signature);
        }
    }
}
