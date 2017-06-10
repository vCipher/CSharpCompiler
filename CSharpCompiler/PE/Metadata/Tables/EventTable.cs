using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct EventRow
    {
        public readonly EventAttributes Attributes;
        public readonly uint Name;
        public readonly MetadataToken EventType;

        public EventRow(EventAttributes attributes, uint name, MetadataToken eventType) : this()
        {
            Attributes = attributes;
            Name = name;
            EventType = eventType;
        }
    }

    public sealed class EventTable : MetadataTable<EventRow>
    {
        public EventTable() : base() { }
        public EventTable(int count) : base(count) { }

        protected override EventRow ReadRow(TableHeap heap)
        {
            return new EventRow(
                (EventAttributes)heap.ReadUInt16(),
                heap.ReadString(),
                heap.ReadCodedToken(CodedTokenType.TypeDefOrRef)
            );
        }

        protected override void WriteRow(EventRow row, TableHeap heap)
        {
            heap.WriteUInt16((ushort)row.Attributes);
            heap.WriteString(row.Name);
            heap.WriteCodedToken(row.EventType, CodedTokenType.TypeDefOrRef);
        }
    }
}
