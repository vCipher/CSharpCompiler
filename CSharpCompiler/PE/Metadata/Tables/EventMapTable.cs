using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct EventMapRow
    {
        public readonly MetadataToken Parent;
        public readonly MetadataToken EventList;

        public EventMapRow(MetadataToken parent, MetadataToken eventList) : this()
        {
            Parent = parent;
            EventList = eventList;
        }
    }

    public sealed class EventMapTable : MetadataTable<EventMapRow>
    {
        public EventMapTable() : base() { }
        public EventMapTable(int count) : base(count) { }

        protected override EventMapRow ReadRow(TableHeapReader heap)
        {
            return new EventMapRow(
                heap.ReadToken(MetadataTokenType.TypeDef),
                heap.ReadToken(MetadataTokenType.Event)
            );
        }

        protected override void WriteRow(EventMapRow row, TableHeapWriter heap)
        {
            heap.WriteToken(row.Parent);
            heap.WriteToken(row.EventList);
        }
    }
}
