using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using CSharpCompiler.Utility;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.CodeGen.Metadata.Tests
{
    public class TableHeapTests : TestCase
    {
        public TableHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/TableHeapTest.txt")]
        public void TableHeapTest(string content, ByteBuffer expected)
        {
            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree);
            MetadataContainer metadata = MetadataBuilder.Build(assemblyDef);

            metadata.Tables.Should().Be(expected, Output);
        }
    }
}
