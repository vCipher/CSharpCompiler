using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct DeclSecurityRow
    {
        public readonly ushort Action;
        public readonly MetadataToken Parent;
        public readonly uint PermissionSet;

        public DeclSecurityRow(ushort action, MetadataToken parent, uint permissionSet) : this()
        {
            Action = action;
            Parent = parent;
            PermissionSet = permissionSet;
        }
    }

    public sealed class DeclSecurityTable : MetadataTable<DeclSecurityRow>
    {
        public DeclSecurityTable() : base() { }
        public DeclSecurityTable(int count) : base(count) { }

        protected override DeclSecurityRow ReadRow(TableHeapReader heap)
        {
            return new DeclSecurityRow(
                heap.ReadUInt16(),
                heap.ReadCodedToken(CodedTokenType.HasDeclSecurity),
                heap.ReadBlobOffset()
            );
        }

        protected override void WriteRow(DeclSecurityRow row, TableHeapWriter heap)
        {
            heap.WriteUInt16(row.Action);
            heap.WriteCodedToken(row.Parent, CodedTokenType.HasDeclSecurity);
            heap.WriteBlob(row.PermissionSet);
        }
    }
}
