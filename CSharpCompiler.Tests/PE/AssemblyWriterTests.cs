using CSharpCompiler.Compilation;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.PE.Tests
{
    public class AssemblyWriterTests : TestCase
    {
        public AssemblyWriterTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/AssemblyWriter/hello_world.txt")]
        public void WriteHelloWorld(string content, string expectedFile, CompilationOptions options) => WriteAssembly(content, expectedFile, options);

        [Theory]
        [FileData("Content/Tests/AssemblyWriter/expression.txt")]
        public void WriteExpression(string content, string expectedFile, CompilationOptions options) => WriteAssembly(content, expectedFile, options);

        [Theory]
        [FileData("Content/Tests/AssemblyWriter/for_loop.txt")]
        public void WriteForLoop(string content, string expectedFile, CompilationOptions options) => WriteAssembly(content, expectedFile, options);

        [Theory]
        [FileData("Content/Tests/AssemblyWriter/array.txt")]
        public void WriteArray(string content, string expectedFile, CompilationOptions options) => WriteAssembly(content, expectedFile, options);

        private void WriteAssembly(string content, string expectedFile, CompilationOptions options)
        {
            var assemblyDef = Compiler.CompileAssembly(content, options);

            using (Stream expected = File.Open(Path.Combine(AppContext.BaseDirectory, expectedFile), FileMode.Open))
            using (Stream actual = new MemoryStream())
            using (AssemblyWriter writer = new AssemblyWriter(assemblyDef, actual, options))
            {
                writer.WriteAssembly();
                writer.BaseStream.Should().Be(expected, Output);
            }
        }
    }
}
