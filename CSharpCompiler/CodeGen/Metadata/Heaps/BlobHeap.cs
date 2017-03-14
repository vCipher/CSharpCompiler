using CSharpCompiler.Utility;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    public sealed class BlobHeap : HeapBuffer
    {
        private Dictionary<ByteBuffer, uint> _blobs = new Dictionary<ByteBuffer, uint>(new ByteBufferComparer());

        public BlobHeap() : base(new byte[] { 0x00 }) { }
        
        public uint RegisterBlob(ByteBuffer blob)
        {
            if (blob.Length == 0)
                return 0;

            uint index;
            if (_blobs.TryGetValue(blob, out index))
                return index;

            index = (uint)Position;
            WriteCompressedUInt32((uint)blob.Length);
            WriteBuffer(blob);
            _blobs.Add(blob, index);

            return index;
        }

        public uint RegisterBlob(byte[] blob)
        {
            if (blob.IsNullOrEmpty())
                return 0;

            return RegisterBlob(new ByteBuffer(blob));
        }
    }
}
