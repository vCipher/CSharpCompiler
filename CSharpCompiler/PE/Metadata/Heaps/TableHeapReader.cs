using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    public sealed class TableHeapReader : PEReader
    {
        private uint _position;
        private MetadataReader _metadata;
        private Func<CodedTokenType, bool> _isLargeToken;

        public TableHeapReader(MetadataReader metadata) : base(metadata.BaseStream)
        {
            _position = 0;
            _metadata = metadata;
            _isLargeToken = Func.Memoize<CodedTokenType, bool>(type => CodedTokenSchema.IsLargeToken(type, metadata.Tables));
        }

        public uint ReadBlobOffset()
        {
            return ReadBySize(_metadata.Blobs.IsLarge);
        }

        public uint ReadGuidOffset()
        {
            return ReadBySize(_metadata.Guids.IsLarge);
        }

        public uint ReadStringOffset()
        {
            return ReadBySize(_metadata.Strings.IsLarge);
        }

        public MetadataToken ReadToken(MetadataTokenType tokenType)
        {
            var tableType = tokenType.ToTableType();
            var rid = ReadBySize(IsLargeTable(tableType));
            return new MetadataToken(tokenType, rid);
        }

        public MetadataToken ReadCodedToken(CodedTokenType tokenType)
        {
            var value = ReadBySize(IsLargeToken(tokenType));
            var codedToken = new CodedToken(value);
            return CodedTokenSchema.GetMetadataToken(codedToken, tokenType);
        }

        public void ReadMetadataTablesContent()
        {
            var prevPosition = (uint)BaseStream.Position;
            MoveTo(_position);

            foreach (var table in _metadata.Tables)
            {
                table.Read(this);
            }

            MoveTo(prevPosition);
        }

        public void ReadMetadataTablesSchema()
        {
            var header = ReadStruct<TableHeapHeader>();
            var valid = header.MaskValid;

            for (int index = 0; index < TableHeap.TABLES_COUNT; index++)
            {
                if (((valid >> index) & 1) == 1)
                {
                    var type = (MetadataTableType)index;
                    var count = ReadInt32();
                    _metadata.Tables.GetOrCreate(type, () => MetadataTableFactory.Create(type, count));
                }
            }

            _position = (uint)BaseStream.Position;
        }

        private bool IsLargeToken(CodedTokenType tokenType)
        {
            return _isLargeToken(tokenType);
        }

        private bool IsLargeTable(MetadataTableType tableType)
        {
            IMetadataTable table;
            return _metadata.Tables.TryGetTable(tableType, out table) && table.Length > 0xffff;
        }

        private uint ReadBySize(bool isLarge)
        {
            return (isLarge) ? ReadUInt32() : ReadUInt16();
        }
    }
}
