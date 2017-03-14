using System.Collections.Generic;

namespace CSharpCompiler.CodeGen
{
    public sealed class ByteBufferComparer : IEqualityComparer<ByteBuffer>
    {
        public bool Equals(ByteBuffer x, ByteBuffer y)
        {
            if (x == null || y == null) return false;
            if (x.Length != y.Length) return false;

            byte[] xBuffer = x.Buffer;
            byte[] yBuffer = y.Buffer;

            for (int i = 0; i < x.Length; i++)
            {
                if (xBuffer[i] != yBuffer[i])
                    return false;
            }

            return true;
        }

        public int GetHashCode(ByteBuffer buffer)
        {
            if (buffer == null) return 0;

            unchecked
            {
                int hash = 1;
                byte[] bytes = buffer.Buffer;
                for (int i = 0; i < buffer.Length; i++)
                    hash = (hash * 37) ^ bytes[i];

                return hash;
            }
        }
    }
}
