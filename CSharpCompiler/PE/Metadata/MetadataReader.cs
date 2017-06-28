using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Utility;
using System;
using System.IO;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataReader : PEReader
    {
        private Func<uint, ByteBuffer> _resolveBlob;
        private Func<uint, Guid> _resolveGuid;
        private Func<uint, string> _resolveString;
        private Func<uint, string> _resolveUserString;
        
        public TableHeapReader TableReader { get; private set; }
        public BlobHeap Blobs { get; private set; }
        public GuidHeap Guids { get; private set; }
        public StringHeap Strings { get; private set; }
        public UserStringHeap UserStrings { get; private set; }
        public TableHeap Tables { get; private set; }
        public MetadataToken EntryPoint { get; private set; }

        public MetadataReader(Stream stream, MetadataToken entryPoint) : base(stream)
        {
            EntryPoint = entryPoint;
            TableReader = new TableHeapReader(this);
            Blobs = new BlobHeap();
            Guids = new GuidHeap();
            Strings = new StringHeap();
            UserStrings = new UserStringHeap();
            Tables = new TableHeap();
            _resolveBlob = Func.Memoize<uint, ByteBuffer>(Blobs.ReadBlob);
            _resolveGuid = Func.Memoize<uint, Guid>(Guids.ReadGuid);
            _resolveString = Func.Memoize<uint, string>(Strings.ReadString);
            _resolveUserString = Func.Memoize<uint, string>(UserStrings.ReadString);
        }

        public ByteBuffer ResolveBlob(uint index)
        {
            if (index == 0)
                return new ByteBuffer();

            return _resolveBlob(index);
        }

        public byte[] ResolveBytes(uint index)
        {
            if (index == 0)
                return Empty<byte>.Array;

            return _resolveBlob(index).Buffer;
        }

        public Guid ResolveGuid(uint index)
        {
            return _resolveGuid(index);
        }

        public string ResolveString(uint index)
        {
            if (index == 0)
                return string.Empty;

            return _resolveString(index);
        }

        public string ResolveUserString(uint index)
        {
            if (index == 0)
                return string.Empty;

            return _resolveUserString(index);
        }
    }
}
