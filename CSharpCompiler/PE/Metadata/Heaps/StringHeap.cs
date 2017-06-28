using System.Text;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    public class StringHeap : HeapBuffer
    {
        public StringHeap() : base() { }
        public StringHeap(byte[] buffer) : base(buffer) { }

        public uint WriteString(string @string)
        {
            var index = (uint)Position;
            InnerWriteString(@string);

            return index;
        }

        public string ReadString(uint index)
        {
            return (index > 0 && index <= Length - 1) ? InnerReadString(index) : string.Empty;
        }

        protected virtual void InnerWriteString(string @string)
        {
            WriteBytes(Encoding.UTF8.GetBytes(@string));
            WriteByte(0);
        }

        protected virtual string InnerReadString(uint index)
        {
            if (index == 0)
                return string.Empty;

            MoveTo((int)index);

            int length = 0;
            while (ReadByte() != 0)
            {
                length++;
            }

            return Encoding.UTF8.GetString(Buffer, (int)index, length);
        }
    }
}
