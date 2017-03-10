using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Tests.Helpers
{
    public static class ParserHelper
    {
        public static ParseNode InvokeMethod(Token method, ParseNode arg)
        {
            return new ParseNode(ParseNodeTag.ExpressionStmt,
                new ParseNode(ParseNodeTag.InvokeExpression,
                    new ParseNode(method),
                    new ParseNode(Tokens.OPEN_PAREN),
                    new ParseNode(ParseNodeTag.ArgumentList,
                        new ParseNode(ParseNodeTag.Argument, arg)),
                    new ParseNode(Tokens.CLOSE_PAREN)
                ),
                new ParseNode(Tokens.SEMICOLON)
            );
        }

        public static ParseNode DeclarationStmt(Token typeName, Token id, Token constant)
        {
            return new ParseNode(ParseNodeTag.DeclarationStmt,
                new ParseNode(ParseNodeTag.VarDeclaration,
                    PrimitiveType(typeName),
                    new ParseNode(ParseNodeTag.VarDeclarator,
                        new ParseNode(ParseNodeTag.VarLocation,
                            new ParseNode(id)
                        ),
                        new ParseNode(Tokens.ASSIGN),
                        Literal(constant)
                    )
                ),
                new ParseNode(Tokens.SEMICOLON)
            );
        }

        public static ParseNode PrimitiveType(Token token)
        {
            return new ParseNode(ParseNodeTag.PrimitiveType,
                new ParseNode(token)
            );
        }

        public static ParseNode Literal(Token token)
        {
            return new ParseNode(ParseNodeTag.Literal,
                new ParseNode(token)
            );
        }

        public static ParseNode Plus(ParseNode leftOp, ParseNode rightOp)
        {
            return new ParseNode(ParseNodeTag.ArithmeticExpression,
                leftOp,
                new ParseNode(Tokens.PLUS),
                rightOp
            );
        }

        public static ParseNode Var(string name)
        {
            return new ParseNode(ParseNodeTag.VarLocation,
                new ParseNode(Tokens.ID(name))
            );
        }
    }
}
