using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Syntax
{
    public sealed class Parser
    {
        private TokenEnumerator _enumerator;

        private Func<ParseNode> MultiplicativeExpression;
        private Func<ParseNode> AdditiveExpression;
        private Func<ParseNode> ShiftExpression;
        private Func<ParseNode> RelationalExpression;
        private Func<ParseNode> EqualityExpression;
        private Func<ParseNode> BitAndExpression;
        private Func<ParseNode> BitXorExpression;
        private Func<ParseNode> BitOrExpression;
        private Func<ParseNode> ConditionalAndExpression;
        private Func<ParseNode> ConditionalOrExpression;

        private Parser(TokenEnumerator enumerator)
        {
            _enumerator = enumerator;

            MultiplicativeExpression = EliminateLeftRecurtion(
                UnaryExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.MultiplicativeExpression),
                () => !CheckTokenSet(TokenTag.MULTIPLY, TokenTag.DIVIDE, TokenTag.MOD));

            AdditiveExpression = EliminateLeftRecurtion(
                MultiplicativeExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.AdditiveExpression),
                () => !CheckTokenSet(TokenTag.PLUS, TokenTag.MINUS));

            ShiftExpression = EliminateLeftRecurtion(
                AdditiveExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.ShiftExpression),
                () => !CheckTokenSet(TokenTag.LEFT_SHIFT, TokenTag.RIGHT_SHIFT));

            RelationalExpression = EliminateLeftRecurtion(
                ShiftExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.RelationalExpression),
                () => !CheckTokenSet(TokenTag.LESS, TokenTag.LESS_OR_EQUAL, TokenTag.GREATER, TokenTag.GREATER_OR_EQUAL, TokenTag.IS, TokenTag.AS));

            EqualityExpression = EliminateLeftRecurtion(
                RelationalExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.EqualityExpression),
                () => !CheckTokenSet(TokenTag.EQUAL, TokenTag.NOT_EQUAL));

            BitAndExpression = EliminateLeftRecurtion(
                EqualityExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.BitAndExpression),
                () => !CheckToken(TokenTag.BIT_AND));

            BitXorExpression = EliminateLeftRecurtion(
                BitAndExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.BitXorExpression),
                () => !CheckToken(TokenTag.BIT_XOR));

            BitOrExpression = EliminateLeftRecurtion(
                BitXorExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.BitOrExpression),
                () => !CheckToken(TokenTag.BIT_OR));

            ConditionalAndExpression = EliminateLeftRecurtion(
                BitOrExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.ConditionalAndExpression),
                () => !CheckToken(TokenTag.AND));

            ConditionalOrExpression = EliminateLeftRecurtion(
                ConditionalAndExpression,
                Terminal,
                GetBinaryOperationFactory(ParseNodeTag.ConditionalOrExpression),
                () => !CheckToken(TokenTag.OR));
        }

        public static ParseTree Parse(TokenEnumerable tokens)
        {
            return new Parser((TokenEnumerator)tokens.GetEnumerator()).Parse();
        }

        public static ParseTree Parse(IReadOnlyList<Token> tokens)
        {
            return new Parser(new TokenEnumerator(tokens)).Parse();
        }

        #region grammar methods
        private ParseTree Parse()
        {
            return new ParseTree(StmtSeq());
        }
        
        private ParseNode Block()
        {
            return new ParseNode(ParseNodeTag.Block)
                .AddChild(Terminal(TokenTag.OPEN_CURLY_BRACE))
                .AddChild(StmtSeq())
                .AddChild(Terminal(TokenTag.CLOSE_CURLY_BRACE));
        }

        private ParseNode StmtSeq()
        {
            var stmtSeq = new ParseNode(ParseNodeTag.StmtSeq);

            while (!CheckToken(TokenTag.CLOSE_CURLY_BRACE) && !CheckToken(TokenTag.UNKNOWN))
            {
                stmtSeq.AddChild(Stmt());
            }

            return stmtSeq;
        }

        private ParseNode Stmt()
        {
            if (CheckToken(TokenTag.OPEN_CURLY_BRACE)) return Block();
            if (IsVarDeclaration()) return DeclarationStmt();
            if (CheckToken(TokenTag.FOR)) return ForStmt();

            return ExpressionStmt();
        }

        private ParseNode ForStmt()
        {
            var forStmt = new ParseNode(ParseNodeTag.ForStmt);
            forStmt.AddChild(Terminal(TokenTag.FOR));
            forStmt.AddChild(Terminal(TokenTag.OPEN_PAREN));

            TryAddChild(forStmt, VarDeclaration, IsVarDeclaration);
            forStmt.AddChild(Terminal(TokenTag.SEMICOLON));

            TryAddChild(forStmt, Expression, () => !CheckToken(TokenTag.SEMICOLON));
            forStmt.AddChild(Terminal(TokenTag.SEMICOLON));

            TryAddChild(forStmt, StmtExpression, () => !CheckToken(TokenTag.SEMICOLON));
            forStmt.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            forStmt.AddChild(Stmt());

            return forStmt;
        }

        private ParseNode DeclarationStmt()
        {
            return new ParseNode(ParseNodeTag.DeclarationStmt)
                .AddChild(VarDeclaration())
                .AddChild(Terminal(TokenTag.SEMICOLON));
        }
        
        private ParseNode VarDeclaration()
        {
            return new ParseNode(ParseNodeTag.VarDeclaration)
                .AddChild(Type())
                .AddChild(VarDeclaratorList());
        }

        private ParseNode VarDeclaratorList()
        {
            var list = new ParseNode(ParseNodeTag.VarDeclaratorList);

            while (
                TryAddChild(list, VarDeclarator, TokenTag.ID) &&
                TryAddChild(list, Terminal, TokenTag.COMMA))
            { }

            return list;
        }

        private ParseNode VarDeclarator()
        {
            var varDeclr = new ParseNode(ParseNodeTag.VarDeclarator)
                .AddChild(Terminal(TokenTag.ID));

            if (CheckToken(TokenTag.ASSIGN))
            {
                varDeclr.AddChild(Terminal())
                    .AddChild(Expression());
            }

            return varDeclr;
        }

        private ParseNode Type()
        {
            if (IsPrimitiveType()) return PrimitiveType().Wrap(ParseNodeTag.Type);
            // todo: implement other types: array, user class and etc.

            throw new SyntaxException("Not supported type {0}", _enumerator.Lookahead());
        }
        
        private ParseNode PrimitiveType()
        {
            return new ParseNode(ParseNodeTag.PrimitiveType)
                .AddChild(Terminal());
        }
        
        private ParseNode ExpressionStmt()
        {
            return new ParseNode(ParseNodeTag.ExpressionStmt)
                .AddChild(StmtExpression())
                .AddChild(Terminal(TokenTag.SEMICOLON));
        }

        private ParseNode StmtExpression()
        {
            if (IsInvokeExpression()) return InvokeExpression();
            if (IsPostfixIncrement()) return PostfixIncrement();
            if (IsPostfixDecrement()) return PostfixDecrement();
            if (IsAssingExpression()) return Assignment();
            if (CheckToken(TokenTag.INCREMENT)) return PrefixIncrement();
            if (CheckToken(TokenTag.DECREMENT)) return PrefixDecrement();
            if (CheckToken(TokenTag.NEW)) return ObjectCreation();

            throw new SyntaxException("Only assignment, call, increment, decrement, and new object expressions can be used as a statement");
        }
        
        private ParseNode Expression()
        {
            var expr = ConditionExpression();

            return CheckToken(IsAssingOperator)
                ? Assignment(expr)
                : expr;
        }

        private ParseNode Assignment()
        {
            return Assignment(VarAccess());
        }

        private ParseNode Assignment(ParseNode node)
        {
            return new ParseNode(ParseNodeTag.Assignment)
                .AddChild(node)
                .AddChild(Terminal())
                .AddChild(Expression())
                .Wrap(ParseNodeTag.Expression);
        }

        private ParseNode ConditionExpression()
        {
            var expr = ConditionalOrExpression();

            return CheckToken(TokenTag.QUESTION) 
                ? TernaryExpression(expr)
                : expr;
        }

        private ParseNode TernaryExpression(ParseNode conditionExpr)
        {
            return new ParseNode(ParseNodeTag.TernaryExpression)
                .AddChild(conditionExpr)
                .AddChild(Terminal(TokenTag.QUESTION))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.COLON))
                .AddChild(Expression())
                .Wrap(ParseNodeTag.Expression);
        }

        private ParseNode UnaryExpression()
        {
            var expr = PrimaryExpression();
            if (!IsUnaryExpression()) return expr;

            return new ParseNode(ParseNodeTag.UnaryExpression)
                .AddChild(Terminal())
                .AddChild(PrimaryExpression())
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode PrimaryExpression()
        {
            if (IsLiteral()) return Literal();
            if (IsCastExpression()) return CastExpression();
            if (IsInvokeExpression()) return InvokeExpression();
            if (IsElementAccess()) return ElementAccess();
            if (IsPostfixIncrement()) return PostfixIncrement();
            if (IsPostfixDecrement()) return PostfixDecrement();
            if (CheckToken(TokenTag.OPEN_PAREN)) return ParenthesisExpression();
            if (CheckToken(TokenTag.INCREMENT)) return PrefixIncrement();
            if (CheckToken(TokenTag.DECREMENT)) return PrefixDecrement();
            if (CheckToken(TokenTag.ID)) return VarAccess();
            if (CheckToken(TokenTag.NEW)) return ObjectCreation();

            throw new SyntaxException("Not supported expression {0}", _enumerator.Lookahead());
        }
        
        private ParseNode Literal()
        {
            return new ParseNode(ParseNodeTag.Literal)
                .AddChild(Terminal())
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode CastExpression()
        {
            return new ParseNode(ParseNodeTag.CastExpression)
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(Type())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN))
                .AddChild(UnaryExpression())
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode ParenthesisExpression()
        {
            return new ParseNode(ParseNodeTag.ParenthesisExpression)
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN))
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode InvokeExpression()
        {
            return new ParseNode(ParseNodeTag.InvokeExpression)
                .AddChild(Terminal(TokenTag.ID))
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(ArgumentList())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN))
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode ArgumentList()
        {
            if (CheckToken(TokenTag.CLOSE_PAREN)) return new ParseNode(ParseNodeTag.ArgumentList);

            var args = new ParseNode(ParseNodeTag.ArgumentList)
                .AddChild(Argument());

            while (CheckToken(TokenTag.COMMA))
            {
                args.AddChild(Terminal())
                    .AddChild(Argument());
            }

            return args;
        }
        
        private ParseNode Argument()
        {
            if (CheckToken(TokenTag.REF) || CheckToken(TokenTag.OUT))
            {
                return new ParseNode(ParseNodeTag.Argument)
                    .AddChild(Terminal())
                    .AddChild(Expression());
            }

            return new ParseNode(ParseNodeTag.Argument)
                .AddChild(Expression());
        }
        
        private ParseNode ElementAccess()
        {
            return new ParseNode(ParseNodeTag.ElementAccess)
                .AddChild(Terminal(TokenTag.ID))
                .AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE))
                .AddChild(ExpressionList())
                .AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE))
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode ExpressionList()
        {
            if (CheckToken(TokenTag.CLOSE_SQUARE_BRACE)) return new ParseNode(ParseNodeTag.ExpressionList);

            var exprs = new ParseNode(ParseNodeTag.ExpressionList)
                .AddChild(Expression());

            while (CheckToken(TokenTag.COMMA))
            {
                exprs.AddChild(Terminal())
                    .AddChild(Expression());
            }

            return exprs;
        }
        
        private ParseNode PostfixIncrement()
        {
            return new ParseNode(ParseNodeTag.PostfixIncrement)
                .AddChild(VarAccess())
                .AddChild(Terminal(TokenTag.INCREMENT))
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode PostfixDecrement()
        {
            return new ParseNode(ParseNodeTag.PostfixDecrement)
                .AddChild(VarAccess())
                .AddChild(Terminal(TokenTag.DECREMENT))
                .Wrap(ParseNodeTag.Expression);
        }

        private ParseNode PrefixIncrement()
        {
            return new ParseNode(ParseNodeTag.PrefixIncrement)
                .AddChild(Terminal(TokenTag.INCREMENT))
                .AddChild(VarAccess())                
                .Wrap(ParseNodeTag.Expression);
        }

        private ParseNode PrefixDecrement()
        {
            return new ParseNode(ParseNodeTag.PrefixDecrement)
                .AddChild(Terminal(TokenTag.DECREMENT))
                .AddChild(VarAccess())                
                .Wrap(ParseNodeTag.Expression);
        }

        private ParseNode VarAccess()
        {
            return new ParseNode(ParseNodeTag.VarAccess)
                .AddChild(Terminal(TokenTag.ID))
                .Wrap(ParseNodeTag.Expression);
        }
        
        private ParseNode ObjectCreation()
        {
            return new ParseNode(ParseNodeTag.ObjectCreation)
                .AddChild(Terminal(TokenTag.NEW))
                .AddChild(Type())
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(ArgumentList())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN))
                .Wrap(ParseNodeTag.Expression);
        }
        #endregion

        #region helper methods
        private bool TryAddChild(ParseNode node, Func<ParseNode> production, TokenTag reqTokenTag)
        {
            return TryAddChild(node, production, () => CheckToken(reqTokenTag));
        }

        private bool TryAddChild(ParseNode node, Func<ParseNode> production, Func<bool> condition)
        {
            if (condition())
            {
                node.AddChild(production());
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
                throw new SyntaxException("Input stream finished");

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

        private Token RequireToken(TokenTag reqTokenTag)
        {
            if (!_enumerator.MoveNext())
                throw new SyntaxException("Input stream finished");

            var currToken = _enumerator.Current;
            if (currToken.Tag != reqTokenTag)
                throw new SyntaxException("Expected {0} tokens. But actual is {1}", reqTokenTag, currToken);

            return currToken;
        }

        private Func<ParseNode> EliminateLeftRecurtion(
            Func<ParseNode> leftOpFactory,
            Func<ParseNode> operatorFactory,
            Func<ParseNode, ParseNode, ParseNode, ParseNode> rightOpFactory,
            Func<bool> breakCondition)
        {
            Func<ParseNode, ParseNode, ParseNode> recursiveCall = null;
            recursiveCall = (leftOp, @operator) =>
            {
                var rightOp = leftOpFactory();
                if (breakCondition()) return rightOpFactory(leftOp, @operator, rightOp);

                var op = rightOpFactory(leftOp, @operator, rightOp);
                return recursiveCall(op, operatorFactory());
            };

            return () =>
            {
                var leftOp = leftOpFactory();
                if (breakCondition()) return leftOp;
                return recursiveCall(leftOp, operatorFactory());
            };
        }

        private Func<ParseNode, ParseNode, ParseNode, ParseNode> GetBinaryOperationFactory(ParseNodeTag tag)
        {
            return (leftOp, @operator, rightOp) =>
            {
                return new ParseNode(tag)
                    .AddChild(leftOp)
                    .AddChild(@operator)
                    .AddChild(rightOp)
                    .Wrap(ParseNodeTag.Expression);
            };
        }
        #endregion

        #region check methods
        private bool CheckTokenSequence(params TokenTag[] tokenTags)
        {
            for (int index = 0; index < tokenTags.Length; index++)
            {
                int shift = index + 1;
                TokenTag tokenTag = tokenTags[index];

                if (!CheckToken(shift, tokenTag))
                    return false;
            }

            return true;
        }

        private bool CheckTokenSet(params TokenTag[] tokenTags)
        {
            return tokenTags.Contains(_enumerator.Lookahead().Tag);
        }

        private bool CheckToken(TokenTag tokenTag)
        {
            return _enumerator.Lookahead().Tag == tokenTag;
        }

        private bool CheckToken(Func<TokenTag, bool> condition)
        {
            return condition(_enumerator.Lookahead().Tag);
        }

        private bool CheckToken(int shift, TokenTag tokenTag)
        {
            return _enumerator.Lookahead(shift).Tag == tokenTag;
        }

        private bool CheckToken(int shift, Func<TokenTag, bool> condition)
        {
            return condition(_enumerator.Lookahead(shift).Tag);
        }

        private bool IsPrimitiveType()
        {
            return _enumerator.Lookahead()
                .Tag
                .IsPrimitiveType();
        }

        private bool IsAssingExpression()
        {
            if (!CheckToken(1, TokenTag.ID)) return false;
            if (!CheckToken(2, IsAssingOperator)) return false;
            return true;
        }

        private bool IsAssingOperator(TokenTag tag)
        {
            return tag == TokenTag.ASSIGN ||
                tag == TokenTag.PLUS_ASSIGN ||
                tag == TokenTag.MINUS_ASSIGN ||
                tag == TokenTag.MULTIPLY_ASSIGN ||
                tag == TokenTag.MOD_ASSIGN ||
                tag == TokenTag.BIT_AND_ASSIGN ||
                tag == TokenTag.BIT_OR_ASSIGN ||
                tag == TokenTag.BIT_XOR_ASSIGN ||
                tag == TokenTag.LEFT_SHIFT_ASSIGN ||
                tag == TokenTag.RIGHT_SHIFT_ASSIGN;
        }

        private bool IsUnaryExpression()
        {
            return CheckTokenSet(TokenTag.MINUS, TokenTag.NOT);
        }

        private bool IsLiteral()
        {
            return CheckTokenSet(
                TokenTag.INT_LITERAL,
                TokenTag.FLOAT_LITERAL,
                TokenTag.DOUBLE_LITERAL,
                TokenTag.STRING_LITERAL,
                TokenTag.TRUE,
                TokenTag.FALSE);
        }

        private bool IsCastExpression()
        {
            // todo: implement checking for general type names 
            // instead of known types
            if (!CheckToken(1, TokenTag.OPEN_PAREN)) return false;
            if (!CheckToken(2, tag => tag.IsPrimitiveType())) return false;
            if (!CheckToken(3, TokenTag.CLOSE_PAREN)) return false;
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

        private bool IsVarDeclaration()
        {
            // todo: implement checking for general type names 
            // instead of known types
            if (!CheckToken(1, tag => tag == TokenTag.VAR || tag.IsPrimitiveType())) return false;
            if (!CheckToken(2, TokenTag.ID)) return false;
            return true;
        }
        #endregion
    }
}
