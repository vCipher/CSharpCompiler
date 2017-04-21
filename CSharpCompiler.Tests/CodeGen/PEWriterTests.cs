using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.CodeGen.Tests
{
    public class PEWriterTests : TestCase
    {
        public PEWriterTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/PEWriter/hello_world.txt")]
        public void WriteHelloWorld(string content, string expectedFile, CompilationOptions options) => Write(content, expectedFile, options);

        [Theory]
        [FileData("Content/Tests/PEWriter/expression.txt")]
        public void WriteExpression(string content, string expectedFile, CompilationOptions options) => Write(content, expectedFile, options);

        [Theory]
        [FileData("Content/Tests/PEWriter/for_loop.txt")]
        public void WriteForLoop(string content, string expectedFile, CompilationOptions options) => Write(content, expectedFile, options);

        [Theory]
        [FileData("Content/Tests/PEWriter/array.txt")]
        public void WriteArray(string content, string expectedFile, CompilationOptions options) => Write(content, expectedFile, options);

        private void Write(string content, string expectedFile, CompilationOptions options)
        {
            var assemblyDef = Compiler.CompileAssembly(content, options);

            using (Stream expected = File.Open(Path.Combine(AppContext.BaseDirectory, expectedFile), FileMode.Open))
            using (Stream actual = new MemoryStream())
            using (PEWriter pe = new PEWriter(assemblyDef, actual, options))
            {
                pe.Write();
                pe.BaseStream.Should().Be(expected, Output);
            }
        }
    }
}
