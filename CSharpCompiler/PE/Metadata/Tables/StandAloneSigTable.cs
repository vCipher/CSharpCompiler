using CSharpCompiler.PE.Metadata.Heaps;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct StandAloneSigRow
    {
        public readonly uint Signature;

        public StandAloneSigRow(uint signature)
        {
            Signature = signature;
        }
    }

    public sealed class StandAloneSigTable : MetadataTable<StandAloneSigRow>
    {
        public StandAloneSigTable() : base() { }
        public StandAloneSigTable(int count) : base(count) { }
        
        protected override void WriteRow(StandAloneSigRow row, TableHeap heap)
        {
            heap.WriteBlob(row.Signature);
        }

        protected override StandAloneSigRow ReadRow(TableHeap heap)
        {
            return new StandAloneSigRow(heap.ReadBlob());
        }
    }
}
