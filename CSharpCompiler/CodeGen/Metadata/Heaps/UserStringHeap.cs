namespace CSharpCompiler.CodeGen.Metadata.Heaps
{
    public class UserStringHeap : StringHeap
    {
        public UserStringHeap() : base() { }

        protected override void WriteString(string @string)
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

        private static byte ComputeTrailingByte(char @char)
        {
            return (byte)(char.IsControl(@char) ? 0x01 : 0x00);
        }
    }
}
