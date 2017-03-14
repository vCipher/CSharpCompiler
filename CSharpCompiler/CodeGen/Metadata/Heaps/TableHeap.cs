using CSharpCompiler.CodeGen.Metadata.Tables;
using CSharpCompiler.Utility;
using System;
using System.Linq;

namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    public sealed class TableHeap : HeapBuffer
    {
        private MetadataContainer _metadata;
        private IMetadataTable[] _tables;
        
        public TableHeap(MetadataContainer metadata) : base(Empty<byte>.Array, 0x08)
        {
            _metadata = metadata;
            _tables = new IMetadataTable[45];
        }
        
        public void Write()
        {
            WriteStruct(GetTableHeapHeader());
            WriteRowCount();
            WriteTables();
        }

        public TTable GetOrCreate<TTable>(MetadataTableType type, Func<TTable> factory) where TTable : IMetadataTable
        {
            IMetadataTable table = _tables[(byte)type];

            if (table == null)
                table = factory();

            _tables[(byte)type] = table;
            return (TTable)table;
        }

        private TableHeapHeader GetTableHeapHeader()
        {
            TableHeapHeader header = new TableHeapHeader();
            header.MajorVersion = 0x02;
            header.MinorVersion = 0x00;
            header.HeapOffsetSizes = GetHeapSizes();
            header.Reserved2 = 0x01;
            header.MaskValid = GetValid();
            header.MaskSorted = GetSorted();

            return header;
        }

        private void WriteRowCount()
        {
            foreach (IMetadataTable table in _tables.Where(table => !table.IsNullOrEmpty()))
                WriteUInt32((uint)table.Length);
        }

        private void WriteTables()
        {
            foreach (IMetadataTable table in _tables.Where(table => table != null))
                table.Write(this);
        }

        private byte GetHeapSizes()
        {
            byte heap_sizes = 0;
            if (_metadata.Strings.IsLarge) heap_sizes |= 0x01;
            if (_metadata.Guids.IsLarge) heap_sizes |= 0x02;
            if (_metadata.Blobs.IsLarge) heap_sizes |= 0x04;

            return heap_sizes;
        }

        private ulong GetValid()
        {
            ulong valid = 0;

            for (int index = 0; index < _tables.Length; index++)
            {
                var table = _tables[index];
                if (table.IsNullOrEmpty())
                    continue;
                
                valid |= (1UL << index);
            }

            return valid;
        }

        private ulong GetSorted()
        {
            ulong sorted = 0;
            sorted |= 1UL << (int)MetadataTableType.InterfaceImpl;
            sorted |= 1UL << (int)MetadataTableType.Constant;
            sorted |= 1UL << (int)MetadataTableType.CustomAttribute;
            sorted |= 1UL << (int)MetadataTableType.FieldMarshal;
            sorted |= 1UL << (int)MetadataTableType.DeclSecurity;
            sorted |= 1UL << (int)MetadataTableType.ClassLayout;
            sorted |= 1UL << (int)MetadataTableType.FieldLayout;
            sorted |= 1UL << (int)MetadataTableType.EventMap;
            sorted |= 1UL << (int)MetadataTableType.PropertyMap;
            sorted |= 1UL << (int)MetadataTableType.MethodSemantics;
            sorted |= 1UL << (int)MetadataTableType.MethodImpl;
            sorted |= 1UL << (int)MetadataTableType.ImplMap;
            sorted |= 1UL << (int)MetadataTableType.FieldRVA;
            sorted |= 1UL << (int)MetadataTableType.NestedClass;
            sorted |= 1UL << (int)MetadataTableType.GenericParam;
            sorted |= 1UL << (int)MetadataTableType.GenericParamConstraint;

            return sorted;
        }
    }
}
