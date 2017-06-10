using CSharpCompiler.Compilation;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using CSharpCompiler.Utility;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.PE.Metadata.Tests
{
    public class GuidHeapTests : TestCase
    {
        public GuidHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/GuidHeapTest.txt")]
        public void GuidHeapTest(string content, Guid guid, ByteBuffer expected)
        {
            var options = new CompilationOptions { Mvid = guid };
            var assemblyDef = Compiler.CompileAssembly(content, options);
            var metadata = MetadataBuilder.Build(assemblyDef);

            metadata.Heaps.Guids.Should().Be(expected, Output);
        }
    }
}
