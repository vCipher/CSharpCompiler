using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ClassLayoutRow
    {
        public readonly ushort PackingSize;
        public readonly uint ClassSize;
        public readonly MetadataToken Parent;

        public ClassLayoutRow(ushort packingSize, uint classSize, MetadataToken parent) : this()
        {
            PackingSize = packingSize;
            ClassSize = classSize;
            Parent = parent;
        }
    }

    public sealed class ClassLayoutTable : MetadataTable<ClassLayoutRow>
    {
        public ClassLayoutTable() : base() { }
        public ClassLayoutTable(int count) : base(count) { }

        protected override ClassLayoutRow ReadRow(TableHeap heap)
        {
            return new ClassLayoutRow(
                heap.ReadUInt16(),
                heap.ReadUInt32(),
                heap.ReadToken(MetadataTokenType.TypeDef)
            );
        }

        protected override void WriteRow(ClassLayoutRow row, TableHeap heap)
        {
            heap.WriteUInt16(row.PackingSize);
            heap.WriteUInt32(row.ClassSize);
            heap.WriteToken(row.Parent);
        }
    }
}
