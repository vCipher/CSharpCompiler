using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
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
        [FileData("Content/Tests/PEWriterTest.txt")]
        public void WriteTest(string content, string expectedFile, CompilationOptions options)
        {
            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree, options);

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
