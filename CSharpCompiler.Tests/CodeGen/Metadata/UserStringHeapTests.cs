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
    public sealed class UserStringHeapTests : TestCase
    {
        public UserStringHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/UserStringHeap/Strings.txt")]
        public void UserStringHeapTest(string content, ByteBuffer expected)
        {
            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree);
            MetadataContainer metadata = MetadataBuilder.Build(assemblyDef);

            metadata.UserStrings.Should().Be(expected, Output);
        }
    }
}
