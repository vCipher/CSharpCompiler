using System;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    public sealed class GuidHeap : HeapBuffer
    {
        public GuidHeap() : base() { }
        public GuidHeap(byte[] buffer) : base(buffer) { MoveTo(START_POSITION); }

        public uint WriteGuid(Guid guid)
        {
            // undocumented behavior, but microsoft c# compiler 
            // generate offset with '+1' shift
            var index = (uint)Position + 1;
            WriteBytes(guid.ToByteArray());

            return index;
        }

        public Guid ReadGuid(uint index)
        {
            MoveTo((int)index - 1);
            var bytes = ReadBytes(16);
            return new Guid(bytes);
        }
    }
}
