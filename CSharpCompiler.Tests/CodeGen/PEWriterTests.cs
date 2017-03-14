using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
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
        [FileData("Content/Tests/PEWriter/Simple.txt")]
        public void WriteTest_Simple(string content, string expectedFile, CompilationOptions options)
        {
            WriteTest(content, expectedFile, options);
        }

        [Theory]
        [FileData("Content/Tests/PEWriter/Expressions.txt")]
        public void WriteTest_Expressions(string content, string expectedFile, CompilationOptions options)
        {
            WriteTest(content, expectedFile, options);
        }

        [Theory]
        [FileData("Content/Tests/PEWriter/Strings.txt")]
        public void WriteTest_Strings(string content, string expectedFile, CompilationOptions options)
        {
            WriteTest(content, expectedFile, options);
        }

        private void WriteTest(string content, string expectedFile, CompilationOptions options)
        {
            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree, options);

            using (Stream expected = new FileStream(expectedFile, FileMode.Open))
            using (Stream actual = new FileStream("WriteTest_Actual.exe", FileMode.Create))
            using (PEWriter pe = new PEWriter(assemblyDef, actual, options))
            {
                pe.Write();
                pe.BaseStream.Should().Be(expected, Output);
            }
        }
    }
}
