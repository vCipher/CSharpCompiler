using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Tests;
using System.Collections.Generic;
using Xunit;
using static CSharpCompiler.Lexica.Tokens.Tokens;

namespace CSharpCompiler.Syntax.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ParseTest()
        {
            string content =
                @"int a = 1;
                  int b = 1;
                  writeLine(a + b);";

            ParseNode expected = new ParseNode(ParseNodeTag.Program,
                new ParseNode(ParseNodeTag.StmtSeq,
                    DeclarationStmt(INT, ID("a"), INT_CONST("1")),
                    DeclarationStmt(INT, ID("b"), INT_CONST("1")),
                    InvokeMethod(ID("writeLine"), Plus(Var("a"), Var("b")))
                )
            );

            List<Token> tokens = Scanner.Scan(content);
            ParseNode actual = Parser.Parse(tokens);

            Assert.Equal(actual, expected, new JsonEqualityComparer<ParseNode>());
        }

        private static ParseNode InvokeMethod(Token method, ParseNode arg)
        {
            return new ParseNode(ParseNodeTag.ExpressionStmt,
                new ParseNode(ParseNodeTag.InvocationExpression,
                    new ParseNode(method),
                    new ParseNode(OPEN_PAREN),
                    new ParseNode(ParseNodeTag.ArgumentList, 
                        new ParseNode(ParseNodeTag.Argument, arg)),
                    new ParseNode(CLOSE_PAREN)
                ),
                new ParseNode(SEMICOLON)
            );
        }

        private static ParseNode DeclarationStmt(Token typeName, Token id, Token constant)
        {
            return new ParseNode(ParseNodeTag.DeclarationStmt,
                new ParseNode(ParseNodeTag.VarDeclaration,
                    PredefinedTypeName(typeName),
                    new ParseNode(id),
                    new ParseNode(ASSIGN),
                    new ParseNode(ParseNodeTag.VarInitializer,
                        Literal(constant)
                    )
                ),
                new ParseNode(SEMICOLON)
            );
        }

        private static ParseNode PredefinedTypeName(Token token)
        {
            return new ParseNode(ParseNodeTag.PredefinedTypeName,
                new ParseNode(token)
            );
        }

        private static ParseNode Literal(Token token)
        {
            return new ParseNode(ParseNodeTag.PrimaryExpression,
                new ParseNode(ParseNodeTag.Literal,
                    new ParseNode(token)
                )
            );
        }

        private static ParseNode Plus(ParseNode leftOp, ParseNode rightOp)
        {
            return new ParseNode(ParseNodeTag.ArithmeticExpression,
                leftOp,
                new ParseNode(PLUS),
                rightOp
            );
        }

        private static ParseNode Var(string name)
        {
            return new ParseNode(ParseNodeTag.VarAccess,
                new ParseNode(ID(name))
            );
        }
    }
}
