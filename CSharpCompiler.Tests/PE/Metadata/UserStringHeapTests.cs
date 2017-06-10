using CSharpCompiler.Compilation;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using CSharpCompiler.Utility;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.PE.Metadata.Tests
{
    public sealed class UserStringHeapTests : TestCase
    {
        public UserStringHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/UserStringHeapTest.txt")]
        public void UserStringHeapTest(string content, ByteBuffer expected)
        {
            var assemblyDef = Compiler.CompileAssembly(content);
            var metadata = MetadataBuilder.Build(assemblyDef);

            metadata.Heaps.UserStrings.Should().Be(expected, Output);
        }
    }
}
