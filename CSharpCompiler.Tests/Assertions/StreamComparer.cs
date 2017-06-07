using System.Collections.Generic;
using System.IO;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class StreamComparer : IEqualityComparer<Stream>
    {
        public bool Equals(Stream x, Stream y)
        {
            using (x)
            using (y)
            {
                if (x.Length != y.Length)
                    return false;

                x.Seek(0, SeekOrigin.Begin);
                y.Seek(0, SeekOrigin.Begin);

                int xByte;
                int yByte;

                do
                {
                    xByte = x.ReadByte();
                    yByte = y.ReadByte();
                }
                while ((xByte == yByte) && (xByte != -1));
                
                return ((xByte - yByte) == 0);
            }
        }

        public int GetHashCode(Stream obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}
