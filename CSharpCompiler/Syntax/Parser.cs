using CSharpCompiler.Lexica.Tokens;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax
{
    public sealed class Parser
    {
        private TokenEnumerator _enumerator;

        private Parser(TokenEnumerator enumerator)
        {
            _enumerator = enumerator;

            IdentifierList = GetFlattenLeftRecurtion(
                ParseNodeTag.IdentifierList,
                () => Terminal(TokenTag.ID),
                () => ExpectAndSkip(TokenTag.DOT));

            StatementSeq = GetFlattenLeftRecurtion(
                ParseNodeTag.StatementSeq,
                Statement,
                () => !ExpectOneOf(TokenTag.CLOSE_CURLY_BRACE, TokenTag.UNKNOWN));

            MultiplicativeExpression = GetBinaryOperation(
                ParseNodeTag.MultiplicativeExpression,
                UnaryExpression,
                () => ExpectOneOf(TokenTag.MULTIPLY, TokenTag.DIVIDE, TokenTag.MOD));

            AdditiveExpression = GetBinaryOperation(
                ParseNodeTag.AdditiveExpression,
                MultiplicativeExpression,
                () => ExpectOneOf(TokenTag.PLUS, TokenTag.MINUS));

            ShiftExpression = GetBinaryOperation(
                ParseNodeTag.ShiftExpression,
                AdditiveExpression,
                () => ExpectOneOf(TokenTag.LEFT_SHIFT, TokenTag.RIGHT_SHIFT));

            RelationalExpression = GetBinaryOperation(
                ParseNodeTag.RelationalExpression,
                ShiftExpression,
                () => ExpectOneOf(TokenTag.LESS, TokenTag.LESS_OR_EQUAL, TokenTag.GREATER, TokenTag.GREATER_OR_EQUAL, TokenTag.IS, TokenTag.AS));

            EqualityExpression = GetBinaryOperation(
                ParseNodeTag.EqualityExpression,
                RelationalExpression,
                () => ExpectOneOf(TokenTag.EQUAL, TokenTag.NOT_EQUAL));

            BitAndExpression = GetBinaryOperation(
                ParseNodeTag.BitAndExpression,
                EqualityExpression,
                () => Expect(TokenTag.BIT_AND));

            BitXorExpression = GetBinaryOperation(
                ParseNodeTag.BitXorExpression,
                BitAndExpression,
                () => Expect(TokenTag.BIT_XOR));

            BitOrExpression = GetBinaryOperation(
                ParseNodeTag.BitOrExpression,
                BitXorExpression,
                () => Expect(TokenTag.BIT_OR));

            ConditionalAndExpression = GetBinaryOperation(
                ParseNodeTag.ConditionalAndExpression,
                BitOrExpression,
                () => Expect(TokenTag.AND));

            ConditionalOrExpression = GetBinaryOperation(
                ParseNodeTag.ConditionalOrExpression,
                ConditionalAndExpression,
                () => Expect(TokenTag.OR));

            ArrayRank = GetFlattenLeftRecurtion(
                ParseNodeTag.RankSpecifier,
                RankSpecifier,
                () => Expect(TokenTag.OPEN_SQUARE_BRACE));

            ArgumentList = GetFlattenLeftRecurtion(
                ParseNodeTag.ArgumentList,
                Argument,
                () => Expect(TokenTag.COMMA));
        }

        public static ParseTree Parse(TokenEnumerable tokens)
        {
            return new Parser((TokenEnumerator)tokens.GetEnumerator()).Parse();
        }

        public static ParseTree Parse(IReadOnlyList<Token> tokens)
        {
            return new Parser(new TokenEnumerator(tokens)).Parse();
        }

        private ParseTree Parse()
        {
            return new ParseTree(StatementSeq());
        }

        #region common
        private Func<ParseNode> IdentifierList;
        #endregion

        #region statements
        private Func<ParseNode> StatementSeq;

        private ParseNode Block()
        {
            return new ParseNode(ParseNodeTag.Block)
                .AddChild(Terminal(TokenTag.OPEN_CURLY_BRACE))
                .AddChild(StatementSeq())
                .AddChild(Terminal(TokenTag.CLOSE_CURLY_BRACE));
        }

        private ParseNode Statement()
        {
            if (Expect(TokenTag.OPEN_CURLY_BRACE)) return Block();
            if (Expect(TokenTag.FOR)) return ForStatement();
            if (Expect(TokenTag.IF)) return IfStatement();
            if (Expect(TokenTag.BREAK)) return BreakStatement();
            if (IsVarDeclaration()) return DeclarationStatement();

            return ExpressionStatement();
        }

        private ParseNode BreakStatement()
        {
            return new ParseNode(ParseNodeTag.BreakStatement)
                .AddChild(Terminal(TokenTag.BREAK))
                .AddChild(Terminal(TokenTag.SEMICOLON));
        }

        private ParseNode IfStatement()
        {
            return new ParseNode(ParseNodeTag.IfStatement)
                .AddChild(Terminal(TokenTag.IF))
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN))
                .AddChild(Statement());
        }

        private ParseNode ForStatement()
        {
            var forStatement = new ParseNode(ParseNodeTag.ForStatement);
            forStatement.AddChild(Terminal(TokenTag.FOR));
            forStatement.AddChild(Terminal(TokenTag.OPEN_PAREN));

            TryAddChild(forStatement, VarDeclaration, IsVarDeclaration);
            forStatement.AddChild(Terminal(TokenTag.SEMICOLON));

            TryAddChild(forStatement, Expression, () => !Expect(TokenTag.SEMICOLON));
            forStatement.AddChild(Terminal(TokenTag.SEMICOLON));

            TryAddChild(forStatement, Expression, () => !Expect(TokenTag.SEMICOLON));
            forStatement.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            forStatement.AddChild(Statement());

            return forStatement;
        }

        private ParseNode DeclarationStatement()
        {
            return new ParseNode(ParseNodeTag.DeclarationStatement)
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

            if (Expect(TokenTag.ASSIGN))
            {
                varDeclr.AddChild(Terminal())
                    .AddChild(Expression());
            }

            return varDeclr;
        }

        private ParseNode ExpressionStatement()
        {
            return new ParseNode(ParseNodeTag.ExpressionStatement)
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.SEMICOLON));
        }
        #endregion

        #region types
        private Func<ParseNode> ArrayRank;

        private ParseNode Type()
        {
            if (IsArrayType()) return ArrayType();
            if (IsPrimitiveType()) return PrimitiveType();
            // todo: implement other types: array, user class and etc.

            throw new SyntaxException("Not supported type {0}", Lookahead());
        }

        private ParseNode NotArrayType()
        {
            if (IsPrimitiveType()) return PrimitiveType();
            // todo: implement other types: array, user class and etc.

            throw new SyntaxException("Not supported type {0}", Lookahead());
        }

        private ParseNode PrimitiveType()
        {
            return new ParseNode(ParseNodeTag.PrimitiveType)
                .AddChild(Terminal());
        }

        private ParseNode ArrayType()
        {
            return new ParseNode(ParseNodeTag.ArrayType)
                .AddChild(NotArrayType())
                .AddChild(ArrayRank());
        }

        private ParseNode RankSpecifier()
        {
            return new ParseNode(ParseNodeTag.RankSpecifier)
                .AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE))
                .AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE));
        }
        #endregion

        #region expressions
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
        private Func<ParseNode> ArgumentList;

        private ParseNode Expression()
        {
            var expr = ConditionExpression();

            return Expect(IsAssingOperator)
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
                .AddChild(Expression());
        }

        private ParseNode ConditionExpression()
        {
            var expr = ConditionalOrExpression();

            return Expect(TokenTag.QUESTION)
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
                .AddChild(Expression());
        }

        private ParseNode UnaryExpression()
        {
            var expr = PrimaryExpression();
            if (!IsUnaryExpression()) return expr;

            return new ParseNode(ParseNodeTag.UnaryExpression)
                .AddChild(Terminal())
                .AddChild(PrimaryExpression());
        }

        private ParseNode PrimaryExpression()
        {
            if (IsElementStore()) return ElementStore();
            if (IsElementAccess()) return ElementAccess();

            return NotArrayInteropExpression();
        }

        private ParseNode NotArrayInteropExpression()
        {
            if (IsLiteral()) return Literal();
            if (IsCastExpression()) return CastExpression();
            if (IsInvokeExpression()) return InvokeExpression();
            if (IsPostfixIncrement()) return PostfixIncrement();
            if (IsPostfixDecrement()) return PostfixDecrement();
            if (IsArrayCreation()) return ArrayCreation();
            if (Expect(TokenTag.OPEN_PAREN)) return ParenthesisExpression();
            if (Expect(TokenTag.INCREMENT)) return PrefixIncrement();
            if (Expect(TokenTag.DECREMENT)) return PrefixDecrement();
            if (Expect(TokenTag.ID)) return VarAccess();
            if (Expect(TokenTag.NEW)) return ObjectCreation();

            throw new SyntaxException("Not supported expression {0}", Lookahead());
        }

        private ParseNode Literal()
        {
            return new ParseNode(ParseNodeTag.Literal)
                .AddChild(Terminal());
        }

        private ParseNode CastExpression()
        {
            return new ParseNode(ParseNodeTag.CastExpression)
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(Type())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN))
                .AddChild(UnaryExpression());
        }

        private ParseNode ParenthesisExpression()
        {
            return new ParseNode(ParseNodeTag.ParenthesisExpression)
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN));
        }

        private ParseNode InvokeExpression()
        {
            return new ParseNode(ParseNodeTag.InvokeExpression)
                .AddChild(IdentifierList())
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(ArgumentList())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN));
        }

        private ParseNode Argument()
        {
            if (Expect(TokenTag.REF) || Expect(TokenTag.OUT))
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
                .AddChild(NotArrayInteropExpression())
                .AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE));
        }

        private ParseNode ElementStore()
        {
            return new ParseNode(ParseNodeTag.ElementStore)
                .AddChild(NotArrayInteropExpression())
                .AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE))
                .AddChild(Terminal(TokenTag.ASSIGN))
                .AddChild(Expression());
        }

        private ParseNode PostfixIncrement()
        {
            return new ParseNode(ParseNodeTag.PostfixIncrement)
                .AddChild(VarAccess())
                .AddChild(Terminal(TokenTag.INCREMENT));
        }

        private ParseNode PostfixDecrement()
        {
            return new ParseNode(ParseNodeTag.PostfixDecrement)
                .AddChild(VarAccess())
                .AddChild(Terminal(TokenTag.DECREMENT));
        }

        private ParseNode PrefixIncrement()
        {
            return new ParseNode(ParseNodeTag.PrefixIncrement)
                .AddChild(Terminal(TokenTag.INCREMENT))
                .AddChild(VarAccess());
        }

        private ParseNode PrefixDecrement()
        {
            return new ParseNode(ParseNodeTag.PrefixDecrement)
                .AddChild(Terminal(TokenTag.DECREMENT))
                .AddChild(VarAccess());
        }

        private ParseNode VarAccess()
        {
            return new ParseNode(ParseNodeTag.VarAccess)
                .AddChild(Terminal(TokenTag.ID));
        }

        private ParseNode ArrayCreation()
        {
            return new ParseNode(ParseNodeTag.ArrayCreation)
                .AddChild(Terminal(TokenTag.NEW))
                .AddChild(NotArrayType())
                .AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE))
                .AddChild(Expression())
                .AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE));
        }

        private ParseNode ObjectCreation()
        {
            return new ParseNode(ParseNodeTag.ObjectCreation)
                .AddChild(Terminal(TokenTag.NEW))
                .AddChild(Type())
                .AddChild(Terminal(TokenTag.OPEN_PAREN))
                .AddChild(ArgumentList())
                .AddChild(Terminal(TokenTag.CLOSE_PAREN));
        }
        #endregion

        #region helper methods
        private Token Lookahead()
        {
            return _enumerator.Lookahead();
        }

        private bool TryAddChild(ParseNode node, Func<ParseNode> production, TokenTag reqTokenTag)
        {
            return TryAddChild(node, production, () => Expect(reqTokenTag));
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

        private ParseNode Terminal()
        {
            if (!_enumerator.MoveNext())
                throw new SyntaxException("Input stream finished");

            return new ParseNode(_enumerator.Current);
        }

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

        private Func<ParseNode> GetLeftRecurtion(
            Func<ParseNode, ParseNode, ParseNode> nodeFactory,
            Func<ParseNode> leftChildFactory,
            Func<ParseNode> rightChildFactory,
            Func<bool> continueCondition)
        {
            Func<ParseNode, ParseNode> recursiveCall = null;
            recursiveCall = leftChild =>
            {
                var rightChild = rightChildFactory();
                if (continueCondition())
                {
                    var node = nodeFactory(leftChild, rightChild);
                    return recursiveCall(node);
                }

                return nodeFactory(leftChild, rightChild);
            };

            return () =>
            {
                var leftChild = leftChildFactory();
                if (continueCondition()) return recursiveCall(leftChild);
                return leftChild;
            };
        }

        private Func<ParseNode> GetFlattenLeftRecurtion(
            ParseNodeTag nodeTag,
            Func<ParseNode> childFactory,
            Func<bool> continueCondition)
        {
            return () =>
            {
                var root = new ParseNode(nodeTag);

                do
                {
                    root.AddChild(childFactory());
                }
                while (continueCondition());

                return root;
            };
        }

        private Func<ParseNode> GetBinaryOperation(
            ParseNodeTag nodeTag,
            Func<ParseNode> childFactory,
            Func<bool> breakCondition)
        {
            Func<ParseNode, ParseNode, ParseNode> nodeFactory = (leftOp, rightOp) =>
            {
                return new ParseNode(nodeTag)
                    .AddChild(leftOp)
                    .AddChild(rightOp.Children.GetAndMove())
                    .AddChild(rightOp.Children.GetAndMove());
            };

            Func<ParseNode> rightChildFactory = () =>
            {
                return new ParseNode(nodeTag)
                    .AddChild(Terminal())
                    .AddChild(childFactory());
            };

            return GetLeftRecurtion(nodeFactory, childFactory, rightChildFactory, breakCondition);
        }
        #endregion

        #region check methods
        private bool IsType(TokenPredictor predictor)
        {
            // todo: implement checking for general type names
            var snapshot = new TokenPredictor(predictor);
            if (predictor.Expect(IsPrimitiveType).Result) return true;
            if (predictor.Restore(snapshot).Expect(IsArrayType).Result) return true;

            return false;
        }

        private bool IsNotArrayType(TokenPredictor predictor)
        {
            // todo: implement checking for general type names
            var snapshot = new TokenPredictor(predictor);
            if (predictor.Expect(IsPrimitiveType).Result) return true;

            return false;
        }

        private bool IsPrimitiveType()
        {
            return Expect(IsPrimitiveType);
        }

        private bool IsPrimitiveType(TokenTag tag)
        {
            return tag == TokenTag.OBJECT
                || tag == TokenTag.BOOL
                || tag == TokenTag.CHAR
                || tag == TokenTag.SBYTE
                || tag == TokenTag.BYTE
                || tag == TokenTag.USHORT
                || tag == TokenTag.SHORT
                || tag == TokenTag.UINT
                || tag == TokenTag.INT
                || tag == TokenTag.ULONG
                || tag == TokenTag.LONG
                || tag == TokenTag.FLOAT
                || tag == TokenTag.DOUBLE
                || tag == TokenTag.DECIMAL
                || tag == TokenTag.STRING
                || tag == TokenTag.VOID;
        }

        private bool IsArrayType()
        {
            return IsArrayType(new TokenPredictor(_enumerator));
        }

        private bool IsArrayType(TokenPredictor predictor)
        {
            return predictor
                .Expect(IsNotArrayType)
                .Expect(TokenTag.OPEN_SQUARE_BRACE)
                .Result;
        }

        private bool IsArrayCreation()
        {
            return new TokenPredictor(_enumerator)
                .Expect(TokenTag.NEW)
                .Expect(IsNotArrayType)
                .Expect(TokenTag.OPEN_SQUARE_BRACE)
                .Result;
        }

        private bool IsAssingExpression()
        {
            return new TokenPredictor(_enumerator)
                .Expect(IsIdentifierList)
                .Expect(IsAssingOperator)
                .Result;
        }

        private bool IsAssingOperator(TokenTag tag)
        {
            return tag == TokenTag.ASSIGN 
                || tag == TokenTag.PLUS_ASSIGN 
                || tag == TokenTag.MINUS_ASSIGN 
                || tag == TokenTag.MULTIPLY_ASSIGN 
                || tag == TokenTag.MOD_ASSIGN 
                || tag == TokenTag.BIT_AND_ASSIGN 
                || tag == TokenTag.BIT_OR_ASSIGN 
                || tag == TokenTag.BIT_XOR_ASSIGN 
                || tag == TokenTag.LEFT_SHIFT_ASSIGN 
                || tag == TokenTag.RIGHT_SHIFT_ASSIGN;
        }

        private bool IsUnaryExpression()
        {
            return ExpectOneOf(TokenTag.MINUS, TokenTag.NOT);
        }

        private bool IsLiteral()
        {
            return ExpectOneOf(
                TokenTag.INT_LITERAL,
                TokenTag.FLOAT_LITERAL,
                TokenTag.DOUBLE_LITERAL,
                TokenTag.STRING_LITERAL,
                TokenTag.TRUE,
                TokenTag.FALSE);
        }

        private bool IsCastExpression()
        {
            return new TokenPredictor(_enumerator)
                .Expect(TokenTag.OPEN_PAREN)
                .Expect(IsType)
                .Expect(TokenTag.CLOSE_PAREN)
                .Result;
        }

        private bool IsInvokeExpression()
        {
            return new TokenPredictor(_enumerator)
                .Expect(IsIdentifierList)
                .Expect(TokenTag.OPEN_PAREN)
                .Result;
        }

        private bool IsElementAccess()
        {
            return new TokenPredictor(_enumerator)
                .Expect(IsIdentifierList)
                .Expect(TokenTag.OPEN_SQUARE_BRACE)
                .Result;
        }

        private bool IsElementStore()
        {
            return new TokenPredictor(_enumerator)
                .Expect(IsIdentifierList)
                .Expect(TokenTag.OPEN_SQUARE_BRACE)
                .SkipUntil(TokenTag.CLOSE_SQUARE_BRACE)
                .Expect(TokenTag.ASSIGN)
                .Result;
        }

        private bool IsPostfixIncrement()
        {
            return new TokenPredictor(_enumerator)
                .Expect(IsIdentifierList)
                .Expect(TokenTag.INCREMENT)
                .Result;
        }

        private bool IsPostfixDecrement()
        {
            return new TokenPredictor(_enumerator)
                .Expect(IsIdentifierList)
                .Expect(TokenTag.DECREMENT)
                .Result;
        }

        private bool IsVarDeclaration()
        {
            if (Expect(TokenTag.VAR))
                return true;

            return new TokenPredictor(_enumerator)
                .Expect(IsNotArrayType)
                .Skip(TokenTag.OPEN_SQUARE_BRACE, TokenTag.COMMA, TokenTag.CLOSE_SQUARE_BRACE)
                .Expect(TokenTag.ID)
                .Result;
        }

        private bool IsIdentifierList(TokenPredictor predictor)
        {
            return predictor
                .Expect(TokenTag.ID)
                .Skip(TokenTag.ID, TokenTag.DOT)
                .Result;
        }

        private bool Expect(TokenTag tag)
        {
            return new TokenPredictor(_enumerator)
                .Expect(tag)
                .Result;
        }

        private bool ExpectAndSkip(TokenTag tag)
        {
            return Expect(tag) && _enumerator.MoveNext();
        }

        private bool ExpectOneOf(params TokenTag[] tags)
        {
            return new TokenPredictor(_enumerator)
                .ExpectOneOf(tags)
                .Result;
        }

        private bool Expect(Func<TokenPredictor, bool> condition)
        {
            return new TokenPredictor(_enumerator)
                .Expect(condition)
                .Result;
        }

        private bool Expect(Func<TokenTag, bool> condition)
        {
            return new TokenPredictor(_enumerator)
                .Expect(condition)
                .Result;
        }
        #endregion
    }
}
