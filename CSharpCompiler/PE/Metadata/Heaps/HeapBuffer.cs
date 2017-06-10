using CSharpCompiler.Utility;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    public class HeapBuffer : ByteBuffer
    {
        public bool IsLarge
        {
            get { return Length > 0xffff; }
        }

        public HeapBuffer() : this(Empty<byte>.Array, 4) { }
        public HeapBuffer(byte[] buffer) : this(buffer, 4) { }
        public HeapBuffer(byte[] buffer, int align) : base(buffer, align) { }
    }
}
