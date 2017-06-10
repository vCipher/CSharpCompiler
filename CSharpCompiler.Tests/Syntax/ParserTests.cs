using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
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
                  System.Console.WriteLine(a + b);";

            ParseTree expected = new ParseTree(
                new ParseNode(ParseNodeTag.StatementSeq,
                    DeclarationStatement(INT, ID("a"), INT_LITERAL("1")),
                    DeclarationStatement(INT, ID("b"), INT_LITERAL("1")),
                    ExpressionStatement(InvokeMethod(
                        ID_LIST("System", "Console", "WriteLine"), 
                        Plus(Var("a"), Var("b"))
                    ))
                )
            );

            TokenEnumerable tokens = Scanner.Scan(content);
            ParseNode actual = Parser.Parse(tokens);
            
            actual.Should().Be(expected, Output);
        }
    }
}
