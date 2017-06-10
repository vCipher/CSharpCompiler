namespace CSharpCompiler.PE.Metadata.Heaps
{
    public class UserStringHeap : StringHeap
    {
        public UserStringHeap() : base() { }
        public UserStringHeap(byte[] buffer) : base(buffer) { }

        protected override void InnerWriteString(string @string)
        {
            WriteCompressedUInt32((uint)@string.Length * 2 + 1);

            byte trailing = 0x00;

            for (int i = 0; i < @string.Length; i++)
            {
                char @char = @string[i];
                WriteUInt16(@char);

                if (trailing == 1)
                    continue;

                trailing = ComputeTrailingByte(@char);
            }

            WriteByte(trailing);
        }

        protected override string InnerReadString(uint index)
        {
            MoveTo((int)index);

            uint length = (uint)(ReadCompressedUInt32() & ~1);
            if (length < 1)
                return string.Empty;

            var start = Position;
            var chars = new char[length / 2];

            for (int i = start, j = 0; i < start + length; i += 2)
            {
                chars[j++] = (char)ReadUInt16();
            }

            return new string(chars);
        }

        private static byte ComputeTrailingByte(char @char)
        {
            return (byte)(char.IsControl(@char) ? 0x01 : 0x00);
        }
    }
}
