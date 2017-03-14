using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class StreamAssertions : ObjectAssertions<Stream>
    {
        public StreamAssertions(Stream actual) : base(actual)
        { }

        public override void Be(Stream expected, ITestOutputHelper output)
        {
            Assert.Equal(expected, actual, new StreamComparer());
        }
    }
}
