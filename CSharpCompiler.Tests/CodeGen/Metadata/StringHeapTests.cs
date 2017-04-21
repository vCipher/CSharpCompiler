using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using CSharpCompiler.Utility;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.CodeGen.Metadata.Tests
{
    public sealed class StringHeapTests : TestCase
    {
        public StringHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/StringHeapTest.txt")]
        public void StringHeapTest(string content, ByteBuffer expected)
        {
            var assemblyDef = Compiler.CompileAssembly(content);
            var metadata = MetadataBuilder.Build(assemblyDef);

            metadata.Strings.Should().Be(expected, Output);
        }
    }
}
