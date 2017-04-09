using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax;

namespace CSharpCompiler.Tests.Helpers
{
    public static class ParserHelper
    {
        public static ParseNode ExpressionStmt(ParseNode expr)
        {
            return new ParseNode(ParseNodeTag.ExpressionStmt,
                expr,
                new ParseNode(Tokens.SEMICOLON)
            );
        }
        public static ParseNode InvokeMethod(Token method, ParseNode arg)
        {
            return new ParseNode(ParseNodeTag.Expression,
                new ParseNode(ParseNodeTag.InvokeExpression,
                    new ParseNode(method),
                    new ParseNode(Tokens.OPEN_PAREN),
                    new ParseNode(ParseNodeTag.ArgumentList,
                        new ParseNode(ParseNodeTag.Argument, arg)),
                    new ParseNode(Tokens.CLOSE_PAREN)
                )
            );
        }

        public static ParseNode DeclarationStmt(Token typeName, Token id, Token constant)
        {
            return new ParseNode(ParseNodeTag.DeclarationStmt,
                VarDeclaration(typeName, id, constant),
                new ParseNode(Tokens.SEMICOLON)
            );
        }

        private static ParseNode VarDeclaration(Token typeName, Token id, Token constant)
        {
            return new ParseNode(ParseNodeTag.VarDeclaration,
                PrimitiveType(typeName),
                new ParseNode(ParseNodeTag.VarDeclaratorList,
                    new ParseNode(ParseNodeTag.VarDeclarator,
                        new ParseNode(id),
                        new ParseNode(Tokens.ASSIGN),
                        Literal(constant)
                    )
                )
            );
        }

        public static ParseNode PrimitiveType(Token token)
        {
            return new ParseNode(ParseNodeTag.Type, 
                new ParseNode(ParseNodeTag.PrimitiveType,
                    new ParseNode(token)
                )
            );
        }

        public static ParseNode Literal(Token token)
        {
            return new ParseNode(ParseNodeTag.Expression,
                new ParseNode(ParseNodeTag.Literal,
                    new ParseNode(token)
                )
            );
        }

        public static ParseNode Plus(ParseNode leftOp, ParseNode rightOp)
        {
            return new ParseNode(ParseNodeTag.Expression,
                new ParseNode(ParseNodeTag.AdditiveExpression,
                    leftOp,
                    new ParseNode(Tokens.PLUS),
                    rightOp
                )
            );
        }

        public static ParseNode Var(string name)
        {
            return new ParseNode(ParseNodeTag.Expression, 
                new ParseNode(ParseNodeTag.VarAccess,
                    new ParseNode(Tokens.ID(name))
                )
            );
        }
    }
}
