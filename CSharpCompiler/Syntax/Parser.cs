using CSharpCompiler.Lexica.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Syntax
{
    public sealed class Parser
    {
        private TokenEnumerator _enumerator;

        public Parser(IReadOnlyList<Token> tokens)
        {
            _enumerator = new TokenEnumerator(tokens);
        }

        public static ParseTree Parse(IReadOnlyList<Token> tokens)
        {
            return new Parser(tokens).Parse();
        }

        #region grammar methods
        private ParseTree Parse()
        {
            var parseTree = new ParseTree();
            parseTree.AddChild(StmtSeq());
            return parseTree;
        }
        
        private ParseNode Block()
        {
            var block = new ParseNode(ParseNodeTag.Block);
            block.AddChild(Terminal(TokenTag.OPEN_CURLY_BRACE));
            block.AddChild(StmtSeq());
            block.AddChild(Terminal(TokenTag.OPEN_CURLY_BRACE));
            return block;
        }
        
        private ParseNode StmtSeq()
        {
            var stmtSeq = new ParseNode(ParseNodeTag.StmtSeq);
            
            // todo: create solution in general
            while (!CheckToken(TokenTag.UNKNOWN))
                stmtSeq.AddChild(Stmt());

            return stmtSeq;
        }
        
        private ParseNode Stmt()
        {
            var stmt = new ParseNode(ParseNodeTag.Stmt);
            if (TryAddChild(stmt, Block, TokenTag.OPEN_CURLY_BRACE)) return Unwrap(stmt);
            if (TryAddChild(stmt, DeclarationStmt, TokenTag.VAR)) return Unwrap(stmt);
            if (TryAddChild(stmt, DeclarationStmt, IsPrimitiveType)) return Unwrap(stmt);
            return ExpressionStmt();
        }
        
        private ParseNode DeclarationStmt()
        {
            var declrStmt = new ParseNode(ParseNodeTag.DeclarationStmt);
            declrStmt.AddChild(VarDeclaration());
            declrStmt.AddChild(Terminal(TokenTag.SEMICOLON));
            return declrStmt;
        }
        
        private ParseNode VarDeclaration()
        {
            // todo: implement multiple declarators
            var varDeclr = new ParseNode(ParseNodeTag.VarDeclaration);
            varDeclr.AddChild(Type());
            varDeclr.AddChild(VarDeclarator());
            return varDeclr;
        }

        private ParseNode VarDeclarator()
        {
            var varDeclr = new ParseNode(ParseNodeTag.VarDeclarator);
            varDeclr.AddChild(VarLocation());
            varDeclr.AddChild(Terminal(TokenTag.ASSIGN));
            varDeclr.AddChild(Expression());
            return varDeclr;
        }

        private ParseNode Type()
        {
            var type = new ParseNode(ParseNodeTag.Type);
            if (TryAddChild(type, PrimitiveType, IsPrimitiveType))
                return Unwrap(type);

            throw new SyntaxException(string.Format("Not supported type {0}", _enumerator.Lookahead()));
        }
        
        private ParseNode PrimitiveType()
        {
            var type = new ParseNode(ParseNodeTag.PrimitiveType);
            type.AddChild(Terminal());
            return type;
        }
        
        private ParseNode ExpressionStmt()
        {
            var exprStmt = new ParseNode(ParseNodeTag.ExpressionStmt);
            exprStmt.AddChild(Expression());
            exprStmt.AddChild(Terminal(TokenTag.SEMICOLON));
            return exprStmt;
        }
        
        private ParseNode Expression()
        {
            var expr = new ParseNode(ParseNodeTag.Expression);
            expr.AddChild(ConditionExpression());

            if (TryAddChild(expr, Terminal, TokenTag.AS))
            {
                expr.AddChild(Type());
                return expr;
            }

            if (TryAddChild(expr, Terminal, TokenTag.IS))
            {
                expr.AddChild(Type());
                return expr;
            }

            if (TryAddChild(expr, Terminal, TokenTag.QUESTION))
            {
                expr.AddChild(Expression());
                expr.AddChild(Terminal(TokenTag.COLON));
                expr.AddChild(Expression());
                return expr;
            }
            
            return Unwrap(expr);
        }
        
        private ParseNode ConditionExpression()
        {
            var expr = new ParseNode(ParseNodeTag.ConditionExpression);
            expr.AddChild(RelationExpression());

            if (TryAddChild(expr, Terminal, IsConditionExpression))
            {
                expr.AddChild(ConditionExpression());
                return expr;
            }

            return Unwrap(expr);
        }

        private ParseNode RelationExpression()
        {
            var expr = new ParseNode(ParseNodeTag.RelationExpression);
            expr.AddChild(ArithmeticExpression());

            if (TryAddChild(expr, Terminal, IsRelationExpression))
            {
                expr.AddChild(RelationExpression());
                return expr;
            }

            return Unwrap(expr);
        }
        
        private ParseNode ArithmeticExpression()
        {
            var expr = new ParseNode(ParseNodeTag.ArithmeticExpression);
            expr.AddChild(FactorExpression());

            if (TryAddChild(expr, Terminal, IsArithmeticExpression))
            {
                expr.AddChild(ArithmeticExpression());
                return expr;
            }

            return Unwrap(expr);
        }
        
        private ParseNode FactorExpression()
        {
            var expr = new ParseNode(ParseNodeTag.FactorExpression);
            expr.AddChild(UnaryExpression());

            if (TryAddChild(expr, Terminal, IsFactorExpression))
            {
                expr.AddChild(FactorExpression());
                return expr;
            }

            return Unwrap(expr);
        }
        
        private ParseNode UnaryExpression()
        {
            var expr = new ParseNode(ParseNodeTag.UnaryExpression);

            if (TryAddChild(expr, Terminal, IsUnaryExpression))
            {
                expr.AddChild(PrimaryExpression());
                return expr;
            }

            return PrimaryExpression();
        }
        
        private ParseNode PrimaryExpression()
        {
            var expr = new ParseNode(ParseNodeTag.PrimaryExpression);
            if (TryAddChild(expr, Literal, IsLiteral)) return Unwrap(expr);
            if (TryAddChild(expr, CastExpression, IsCastExpression)) return Unwrap(expr);
            if (TryAddChild(expr, ParenthesisExpression, TokenTag.OPEN_PAREN)) return Unwrap(expr);
            if (TryAddChild(expr, InvokeExpression, IsInvokeExpression)) return Unwrap(expr);
            if (TryAddChild(expr, ElementAccess, IsElementAccess)) return Unwrap(expr);
            if (TryAddChild(expr, PostfixIncrement, IsPostfixIncrement)) return Unwrap(expr);
            if (TryAddChild(expr, PostfixDecrement, IsPostfixDecrement)) return Unwrap(expr);
            if (TryAddChild(expr, VarLocation, TokenTag.ID)) return Unwrap(expr);
            if (TryAddChild(expr, ObjectCreation, TokenTag.NEW)) return Unwrap(expr);
            return expr;
        }
        
        private ParseNode Literal()
        {
            var literal = new ParseNode(ParseNodeTag.Literal);
            literal.AddChild(Terminal());
            return literal;
        }
        
        private ParseNode CastExpression()
        {
            var expr = new ParseNode(ParseNodeTag.CastExpression);
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(Type());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            expr.AddChild(UnaryExpression());
            return expr;
        }
        
        private ParseNode ParenthesisExpression()
        {
            var expr = new ParseNode(ParseNodeTag.ParenthesisExpression);
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(Expression());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            return expr;
        }
        
        private ParseNode InvokeExpression()
        {
            var expr = new ParseNode(ParseNodeTag.InvokeExpression);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(ArgumentList());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            return expr;
        }
        
        private ParseNode ArgumentList()
        {
            var args = new ParseNode(ParseNodeTag.ArgumentList);

            if (TryAddChild(args, Argument, () => !CheckToken(TokenTag.CLOSE_PAREN)))
            {
                while (TryAddChild(args, Terminal, TokenTag.COMMA))
                    args.AddChild(Argument());
            }

            return args;
        }
        
        private ParseNode Argument()
        {
            var arg = new ParseNode(ParseNodeTag.Argument);
            
            if (TryAddChild(arg, Terminal, TokenTag.REF))
            {
                arg.AddChild(Expression());
                return arg;
            }
                
            if (TryAddChild(arg, Terminal, TokenTag.OUT))
            {
                arg.AddChild(Expression());
                return arg;
            }

            arg.AddChild(Expression());
            return arg;
        }
        
        private ParseNode ElementAccess()
        {
            var expr = new ParseNode(ParseNodeTag.ElementAccess);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE));
            expr.AddChild(ExpressionList());
            expr.AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE));
            return expr;
        }
        
        private ParseNode ExpressionList()
        {
            var exprs = new ParseNode(ParseNodeTag.ExpressionList);

            if (TryAddChild(exprs, Expression, () => !CheckToken(TokenTag.CLOSE_SQUARE_BRACE)))
            {
                while (TryAddChild(exprs, Terminal, TokenTag.COMMA))
                    exprs.AddChild(Expression());
            }

            return exprs;
        }
        
        private ParseNode PostfixIncrement()
        {
            var expr = new ParseNode(ParseNodeTag.PostfixIncrement);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.INCREMENT));
            return expr;
        }
        
        private ParseNode PostfixDecrement()
        {
            var expr = new ParseNode(ParseNodeTag.PostfixDecrement);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.DECREMENT));
            return expr;
        }
        
        private ParseNode VarLocation()
        {
            var expr = new ParseNode(ParseNodeTag.VarLocation);
            expr.AddChild(Terminal(TokenTag.ID));
            return expr;
        }
        
        private ParseNode ObjectCreation()
        {
            var expr = new ParseNode(ParseNodeTag.ObjectCreation);
            expr.AddChild(Terminal(TokenTag.NEW));
            expr.AddChild(Type());
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(ArgumentList());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            return expr;
        }
        #endregion

        #region helper methods
        private bool TryAddChild(ParseNode node, Func<ParseNode> production, TokenTag reqTokenTag)
        {
            return TryAddChild(node, production, () => _enumerator.Lookahead().Tag == reqTokenTag);
        }

        private bool TryAddChild(ParseNode node, Func<ParseNode> production, Func<bool> condition)
        {
            if (condition?.Invoke() ?? false)
            {
                node.AddChild(production?.Invoke());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Derivate terminal production from current token without any requirements
        /// </summary>
        private ParseNode Terminal()
        {
            if (!_enumerator.MoveNext())
                throw new SyntaxException($"Input stream finished");

            return new ParseNode(_enumerator.Current);
        }

        /// <summary>
        /// Derivate terminal production from current token with require token tag
        /// </summary>
        private ParseNode Terminal(TokenTag reqTokenTag)
        {
            var token = RequireToken(reqTokenTag);
            return new ParseNode(token);
        }

        private ParseNode Unwrap(ParseNode node)
        {
            return node.Children.Single();
        }

        private Token RequireToken(TokenTag reqTokenTag)
        {
            if (!_enumerator.MoveNext())
                throw new SyntaxException("Input stream finished");

            var currToken = _enumerator.Current;
            if (currToken.Tag != reqTokenTag)
                throw new UnexpectedTokenException(reqTokenTag, currToken);

            return currToken;
        }
        #endregion

        #region check methods
        private bool CheckTokenSequence(params TokenTag[] tokenTags)
        {
            for (int index = 0; index < tokenTags.Length; index++)
            {
                int shift = index + 1;
                TokenTag tokenTag = tokenTags[index];

                if (!CheckToken(tokenTag, shift))
                    return false;
            }

            return true;
        }

        private bool CheckToken(TokenTag tokenTag)
        {
            return _enumerator.Lookahead().Tag == tokenTag;
        }

        private bool CheckToken(TokenTag tokenTag, int shift)
        {
            return _enumerator.Lookahead(shift).Tag == tokenTag;
        }

        private bool IsPrimitiveType()
        {
            return _enumerator.Lookahead()
                .IsPrimitiveTypeToken();
        }

        private bool IsUnaryExpression()
        {
            return _enumerator.Lookahead()
                .IsUnaryToken();
        }

        private bool IsRelationExpression()
        {
            return _enumerator.Lookahead()
                .IsRelationToken();
        }

        private bool IsArithmeticExpression()
        {
            return _enumerator.Lookahead()
                .IsArithmeticToken();
        }

        private bool IsFactorExpression()
        {
            return _enumerator.Lookahead()
                .IsFactorToken();
        }

        private bool IsLiteral()
        {
            return _enumerator.Lookahead()
                .IsLiteralToken();
        }

        private bool IsConditionExpression()
        {
            return _enumerator.Lookahead()
                .IsConditionToken();
        }

        private bool IsCastExpression()
        {
            // todo: implement checking for general type names 
            // instead of known types
            if (_enumerator.Lookahead(1).Tag != TokenTag.OPEN_PAREN) return false;
            if (!_enumerator.Lookahead(2).IsPrimitiveTypeToken()) return false;
            if (_enumerator.Lookahead(3).Tag != TokenTag.CLOSE_PAREN) return false;
            return true;
        }

        private bool IsInvokeExpression()
        {
            // todo: implement checking for general expressions 
            // instead of simple TokenTag.ID
            return CheckTokenSequence(TokenTag.ID, TokenTag.OPEN_PAREN);
        }

        private bool IsElementAccess()
        {
            // todo: implement checking for general expressions 
            // instead of simple TokenTag.ID
            return CheckTokenSequence(TokenTag.ID, TokenTag.OPEN_SQUARE_BRACE);
        }

        private bool IsPostfixIncrement()
        {
            // todo: implement checking for general expressions 
            // instead of simple TokenTag.ID
            return CheckTokenSequence(TokenTag.ID, TokenTag.INCREMENT);
        }

        private bool IsPostfixDecrement()
        {
            // todo: implement checking for general expressions 
            // instead of simple TokenTag.ID
            return CheckTokenSequence(TokenTag.ID, TokenTag.DECREMENT);
        }
        #endregion
    }
}
