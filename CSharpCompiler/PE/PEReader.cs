using CSharpCompiler.Utility;
using System.IO;

namespace CSharpCompiler.PE
{
    public class PEReader : BinaryReader
    {
        public PEReader(Stream input) : base(input)
        { }

        public ByteBuffer ReadBuffer(int size)
        {
            var bytes = ReadBytes(size);
            return new ByteBuffer(bytes);
        }

        public T ReadStruct<T>() where T : struct
        {
            var size = ByteBuffer.SizeOf<T>();
            var bytes = ReadBytes(size);
            return ByteBuffer.FromBytes<T>(bytes);
        }

        public string ReadZeroTerminatedString(int length)
        {
            int read = 0;
            var buffer = new char[length];
            var bytes = ReadBytes(length);

            while (read < length)
            {
                var current = bytes[read];
                if (current == 0)
                    break;

                buffer[read++] = (char)current;
            }

            return new string(buffer, 0, read);
        }

        public string ReadAlignedString(int length)
        {
            var read = 0;
            var buffer = new char[length];

            while (read < length)
            {
                read++;
                var current = ReadByte();
                if (current == 0)
                    break;

                buffer[read - 1] = (char)current;
            }

            Advance(BitArithmetic.Align(read, 4) - read);
            return new string(buffer, 0, read - 1);
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
