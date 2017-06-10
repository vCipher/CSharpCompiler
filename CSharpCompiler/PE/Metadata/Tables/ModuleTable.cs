using CSharpCompiler.PE.Metadata.Heaps;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ModuleRow
    {
        public readonly ushort Generation;
        public readonly uint Name;
        public readonly uint Mvid;
        public readonly ushort EncId;
        public readonly ushort EncBaseId;

        public ModuleRow(ushort generation, uint name, uint mvid, ushort encId, ushort encBaseId) : this()
        {
            Generation = generation;
            Name = name;
            Mvid = mvid;
            EncId = encId;
            EncBaseId = encBaseId;
        }
    }

    public sealed class ModuleTable : OneRowMetadataTable<ModuleRow>
    {
        public ModuleTable() : base() { }
        public ModuleTable(int count) : base(count) { }
        
        protected override void WriteRow(ModuleRow row, TableHeap heap)
        {
            heap.WriteUInt16(row.Generation);
            heap.WriteString(row.Name);
            heap.WriteGuid(row.Mvid);
            heap.WriteUInt16(row.EncId);
            heap.WriteUInt16(row.EncBaseId);
        }

        protected override ModuleRow ReadRow(TableHeap heap)
        {
            return new ModuleRow(
                heap.ReadUInt16(),
                heap.ReadString(),
                heap.ReadGuid(),
                heap.ReadUInt16(),
                heap.ReadUInt16()
            );
        }
    }
}
