using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

using static CSharpCompiler.Tests.Helpers.ParserHelper;
using static CSharpCompiler.Lexica.Tokens.Tokens;

namespace CSharpCompiler.Syntax.Tests
{
    public class ParserTests : TestCase
    {
        public ParserTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void ParseTest()
        {
            string content =
                @"int a = 1;
                  int b = 1;
                  writeLine(a + b);";

            ParseTree expected = new ParseTree(
                new ParseNode(ParseNodeTag.StmtSeq,
                    DeclarationStmt(INT, ID("a"), INT_CONST("1")),
                    DeclarationStmt(INT, ID("b"), INT_CONST("1")),
                    InvokeMethod(ID("writeLine"), Plus(Var("a"), Var("b")))
                )
            );

            List<Token> tokens = Scanner.Scan(content);
            ParseNode actual = Parser.Parse(tokens);

            actual.Should().Be(expected, Output);
        }
    }
}
