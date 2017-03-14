using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Utility
{
    public sealed class ByteArrayComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] x, byte[] y)
        {
            if (x == null || y == null) return false;
            if (x.Length != y.Length) return false;
            return x.SequenceEqual(y);
        }

        public int GetHashCode(byte[] bytes)
        {
            if (bytes == null) return 0;

            unchecked
            {
                int hash = 1;
                for (int i = 0; i < bytes.Length; i++)
                    hash = (hash * 37) ^ bytes[i];

                return hash;
            }
        }
    }
}
