using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    public sealed class TableHeapWriter : HeapBuffer
    {
        private const int TABLES_COUNT = 45;
        private Func<CodedTokenType, bool> _isLargeToken;

        private TableHeap _heap;
        private MetadataHeaps _heaps;
        private IMetadataTable[] _tables;

        public TableHeapWriter(MetadataHeaps heaps) : base(Empty<byte>.Array, 0x08)
        {
            _heap = new TableHeap();
            _heaps = heaps;
            _tables = new IMetadataTable[TABLES_COUNT];
            _isLargeToken = Func.Memoize<CodedTokenType, bool>(type => CodedTokenSchema.IsLargeToken(type, _heap));
        }

        public TTable GetOrCreate<TTable>(MetadataTableType type) where TTable : IMetadataTable
        {
            return _heap.GetOrCreate<TTable>(type);
        }

        public void WriteBlob(uint rva)
        {
            WriteBySize(rva, _heaps.Blobs.IsLarge);
        }

        public void WriteGuid(uint rva)
        {
            WriteBySize(rva, _heaps.Guids.IsLarge);
        }

        public void WriteString(uint rva)
        {
            WriteBySize(rva, _heaps.Strings.IsLarge);
        }

        public void WriteToken(MetadataToken token)
        {
            var tableType = token.Type.ToTableType();
            WriteBySize(token.RID, IsLargeTable(tableType));
        }

        public void WriteCodedToken(MetadataToken token, CodedTokenType tokenType)
        {
            var codedToken = CodedTokenSchema.GetCodedToken(token, tokenType);
            WriteBySize(codedToken.Value, IsLargeToken(tokenType));
        }

        public void Write()
        {
            WriteStruct(GetTableHeapHeader());
            WriteRowCount();
            WriteTables();
        }

        private void WriteRowCount()
        {
            foreach (var table in _tables)
            {
                if (table != null)
                    WriteUInt32((uint)table.Length);
            }
        }

        private void WriteTables()
        {
            foreach (var table in _tables)
            {
                if (table != null)
                    table.Write(this);
            }
        }

        private TableHeapHeader GetTableHeapHeader()
        {
            return new TableHeapHeader()
            {
                MajorVersion = 0x02,
                MinorVersion = 0x00,
                HeapFlags = _heaps.GetHeapFlags(),
                Rid = 0x01,
                MaskValid = GetValidMask(),
                MaskSorted = GetSortedMask()
            };
        }

        private ulong GetValidMask()
        {
            ulong valid = 0;
            for (int index = 0; index < TABLES_COUNT; index++)
            {
                if (_tables[index] != null)
                    valid |= (1ul << index);
            }

            return valid;
        }

        private ulong GetSortedMask()
        {
            ulong sorted = 0;
            sorted |= 1ul << (int)MetadataTableType.InterfaceImpl;
            sorted |= 1ul << (int)MetadataTableType.Constant;
            sorted |= 1ul << (int)MetadataTableType.CustomAttribute;
            sorted |= 1ul << (int)MetadataTableType.FieldMarshal;
            sorted |= 1ul << (int)MetadataTableType.DeclSecurity;
            sorted |= 1ul << (int)MetadataTableType.ClassLayout;
            sorted |= 1ul << (int)MetadataTableType.FieldLayout;
            sorted |= 1ul << (int)MetadataTableType.EventMap;
            sorted |= 1ul << (int)MetadataTableType.PropertyMap;
            sorted |= 1ul << (int)MetadataTableType.MethodSemantics;
            sorted |= 1ul << (int)MetadataTableType.MethodImpl;
            sorted |= 1ul << (int)MetadataTableType.ImplMap;
            sorted |= 1ul << (int)MetadataTableType.FieldRVA;
            sorted |= 1ul << (int)MetadataTableType.NestedClass;
            sorted |= 1ul << (int)MetadataTableType.GenericParam;
            sorted |= 1ul << (int)MetadataTableType.GenericParamConstraint;

            return sorted;
        }

        private bool IsLargeToken(CodedTokenType tokenType)
        {
            return _isLargeToken(tokenType);
        }

        private bool IsLargeTable(MetadataTableType tableType)
        {
            var table = _tables[(int)tableType];
            return table != null && table.Length > 0xffff;
        }

        private IMetadataTable[] GetMetadataTables()
        {
            var tables = new IMetadataTable[TABLES_COUNT];
            var header = ReadStruct<TableHeapHeader>();
            var valid = header.MaskValid;

            for (int index = 0; index < TABLES_COUNT; index++)
            {
                if (((valid >> index) & 1) == 1)
                {
                    var type = (MetadataTableType)index;
                    var count = ReadInt32();
                    tables[(int)type] = MetadataTableFactory.Create(type, count);
                }
            }

            return tables;
        }

        private void WriteBySize(uint value, bool isLarge)
        {
            if (isLarge)
                WriteUInt32(value);
            else
                WriteUInt16((ushort)value);
        }
    }
}
