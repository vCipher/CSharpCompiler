using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.CodeGen.Metadata.Tests
{
    public class BlobHeapTests : TestCase
    {
        public BlobHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/BlobHeap/Strings.txt")]
        public void BlobHeapTest(string content, ByteBuffer expected)
        {
            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree);
            MetadataContainer metadata = MetadataBuilder.Build(assemblyDef);

            metadata.Blobs.Should().Be(expected, Output);
        }
    }
}
