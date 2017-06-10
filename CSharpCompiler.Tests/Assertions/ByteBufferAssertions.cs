using CSharpCompiler.PE;
using CSharpCompiler.Utility;
using System;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class ByteBufferAssertions : ObjectAssertions<ByteBuffer>
    {
        public ByteBufferAssertions(ByteBuffer actual) : base(actual)
        { }

        public override void Be(ByteBuffer expected, ITestOutputHelper output)
        {
            byte[] expectedArray = expected.ToByteArray();
            byte[] actualArray = actual.ToByteArray();

            if (!expectedArray.SequenceEqual(actualArray))
            {
                output?.WriteLine("Expected:\n {0}\n", BitConverter.ToString(expectedArray).Replace("-", "\n"));
                output?.WriteLine("Actual:\n {0}\n", BitConverter.ToString(actualArray).Replace("-", "\n"));
                throw new EqualException(expected, actual);
            }
        }
    }
}
