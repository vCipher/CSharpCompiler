using CSharpCompiler.PE.Metadata.Heaps;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct ModuleRefRow
    {
        public readonly uint Name;

        public ModuleRefRow(uint name) : this()
        {
            Name = name;
        }
    }

    public sealed class ModuleRefTable : MetadataTable<ModuleRefRow>
    {
        public ModuleRefTable() : base() { }
        public ModuleRefTable(int count) : base(count) { }

        protected override ModuleRefRow ReadRow(TableHeapReader heap)
        {
            return new ModuleRefRow(heap.ReadStringOffset());
        }

        protected override void WriteRow(ModuleRefRow row, TableHeapWriter heap)
        {
            heap.WriteString(row.Name);
        }
    }
}

