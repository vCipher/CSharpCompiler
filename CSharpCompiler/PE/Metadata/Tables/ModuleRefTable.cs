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

        protected override ModuleRefRow ReadRow(TableHeap heap)
        {
            return new ModuleRefRow(heap.ReadString());
        }

        protected override void WriteRow(ModuleRefRow row, TableHeap heap)
        {
            heap.WriteString(row.Name);
        }
    }
}

