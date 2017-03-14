using System;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    public sealed class GuidHeap : HeapBuffer
    {
        private Dictionary<Guid, uint> _guids = new Dictionary<Guid, uint>();

        public GuidHeap(): base() { }

        public uint RegisterGuid(Guid guid)
        {
            uint index;
            if (_guids.TryGetValue(guid, out index))
                return index;

            // undocumented behavior, but microsoft c# compiler 
            // generate offset with '+1' shift
            index = (uint)Position + 1;
            WriteBytes(guid.ToByteArray());
            _guids.Add(guid, index);

            return index;
        }
    }
}
