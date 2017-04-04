using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Tests.Data;
using CSharpCompiler.Utility;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.CodeGen.Metadata.Tests
{
    public class GuidHeapTests : TestCase
    {
        public GuidHeapTests(ITestOutputHelper output) : base(output)
        { }

        [Theory]
        [FileData("Content/Tests/GuidHeapTest.txt")]
        public void GuidHeapTest(string content, Guid guid, ByteBuffer expected)
        {
            CompilationOptions options = new CompilationOptions { Mvid = guid };
            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree, options);
            MetadataContainer metadata = MetadataBuilder.Build(assemblyDef);
            
            metadata.Guids.Should().Be(expected, Output);
        }
    }
}
