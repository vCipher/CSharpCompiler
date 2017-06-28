using CSharpCompiler.Utility;
using System.IO;

namespace CSharpCompiler.PE
{
    public class PEWriter : BinaryWriter
    {
        public PEWriter(Stream output) : base(output)
        { }

        public void WriteUInt32(uint value)
        {
            Write(value);
        }

        public void WriteUInt16(ushort value)
        {
            Write(value);
        }

        public void WriteUInt8(byte value)
        {
            Write(value);
        }

        public void WriteBytes(byte[] value)
        {
            Write(value);
        }

        public void WriteBuffer(ByteBuffer buffer)
        {
            Write(buffer.Buffer, 0, buffer.Length);
        }

        public void WriteStruct<T>(T value) where T : struct
        {
            Write(ByteBuffer.ToBytes(value));
        }

        public void MoveTo(uint raw)
        {
            BaseStream.Seek(raw, SeekOrigin.Begin);
        }

        public void Advance(int bytes)
        {
            BaseStream.Seek(bytes, SeekOrigin.Current);
        }
    }
}
