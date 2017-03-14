using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    public class StringHeap : HeapBuffer
    {
        protected readonly Dictionary<string, uint> _strings = new Dictionary<string, uint>(StringComparer.Ordinal);
        
        public StringHeap() : base(new byte[] { 0x00 }) { }

        public uint RegisterString(string @string)
        {
            uint index;
            if (_strings.TryGetValue(@string, out index))
                return index;

            index = (uint)Position;
            WriteString(@string);
            _strings.Add(@string, index);

            return index;
        }

        protected virtual void WriteString(string @string)
        {
            WriteBytes(Encoding.UTF8.GetBytes(@string));
            WriteByte(0);
        }
    }
}
