using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    public class HeapBuffer : ByteBuffer
    {
        public bool IsLarge
        {
            get { return Length > 65535; }
        }

        public HeapBuffer() : base(Empty<byte>.Array, 4) { }
        public HeapBuffer(byte[] buffer) : base(buffer, 4) { }
        public HeapBuffer(byte[] buffer, int align) : base(buffer, align) { }
    }
}
