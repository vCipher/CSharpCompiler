using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Statements;
using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Syntax.Ast.Variables;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Syntax.Ast.Tests
{
    public class AstBuilderTests : TestCase
    {
        public AstBuilderTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void BuildTest()
        {
            string content =
                @"int a = 1;
                  int b = 1;
                  writeLine(a + b);";

            TokenEnumerable tokens = Scanner.Scan(content);
            ParseTree parseTree = Parser.Parse(tokens);

            SyntaxTree expected = GetExpectedSyntaxTree();
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);

            syntaxTree.Should().Be(expected, Output);
        }

        private static SyntaxTree GetExpectedSyntaxTree()
        {
            VarScope scope = new VarScope();
            SyntaxTree expected = new SyntaxTree(
                new DeclarationStatement(
                    new VarDeclaration(
                        new PrimitiveTypeNode(Tokens.INT),
                        "a",
                        new Literal(Tokens.INT_LITERAL("1")),
                        scope
                    )
                ),
                new DeclarationStatement(
                    new VarDeclaration(
                        new PrimitiveTypeNode(Tokens.INT),
                        "b",
                        new Literal(Tokens.INT_LITERAL("1")),
                        scope
                    )
                ),
                new ExpressionStatement(
                    new InvokeExpression(
                        "writeLine",
                        new List<Argument>
                        {
                            new Argument(
                                new ArithmeticOperation(
                                    Tokens.PLUS,
                                    new VarAccess("a", scope),
                                    new VarAccess("b", scope)
                                )
                            )
                        },
                        true
                    )
                )
            );
            return expected;
        }
    }
}
