using CSharpCompiler.Compilation;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using CSharpCompiler.Utility;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.PE.Metadata.Tests
{
    public class BlobHeapTests : TestCase
    {
        public BlobHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/BlobHeapTest.txt")]
        public void BlobHeapTest(string content, ByteBuffer expected)
        {
            var assemblyDef = Compiler.CompileAssembly(content);
            var metadata = MetadataBuilder.Build(assemblyDef);

            metadata.Heaps.Blobs.Should().Be(expected, Output);
        }
    }
}
