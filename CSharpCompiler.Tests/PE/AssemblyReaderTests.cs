using CSharpCompiler.Tests;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.PE.Tests
{
    public sealed class AssemblyReaderTests : TestCase
    {
        public AssemblyReaderTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void ReadAssembly()
        {
            var asm = typeof(object).GetTypeInfo().Assembly;

            using (var stream = File.OpenRead(asm.Location))
            using (var reader = new AssemblyReader(stream))
            {
                var result = reader.ReadAssembly();
                //reader.BaseStream.Should().Be(expected, Output);
            }
        }
    }
}
