using CSharpCompiler.Scanners;
using CSharpCompiler.Scanners.Tokens;
using System;
using System.Linq;

namespace CSharpCompiler.Parsers
{
    public sealed class Parser
    {
        public static readonly Parser Default = new Parser(Scanner.Default);
        private TokenIterator _tokens;
        private readonly Scanner _scanner;

        public Parser(Scanner scanner)
        {
            _scanner = scanner;
        }

        public ParseNode Parse(string content)
        {
            var tokens = _scanner.Scan(content);
            _tokens = new TokenIterator(tokens);
            return Program();
        }

        #region grammar methods
        private ParseNode Program()
        {
            var program = new ParseNode(ParseNodeTag.Program);
            program.AddChild(StmtSeq());
            return program;
        }

        /// <summary>
        /// block
        /// : OPEN_CURLY_BRACE statement_seq CLOSE_CURLY_BRACE
        /// | OPEN_CURLY_BRACE CLOSE_CURLY_BRACE
        /// ;
        /// </summary>
        private ParseNode Block()
        {
            var block = new ParseNode(ParseNodeTag.Block);
            block.AddChild(Terminal(TokenTag.OPEN_CURLY_BRACE));
            block.AddChild(StmtSeq());
            block.AddChild(Terminal(TokenTag.OPEN_CURLY_BRACE));
            return block;
        }

        /// <summary>
        /// statement_seq
        /// : statement
        /// | statement statement_seq
        /// ;
        /// </summary>
        private ParseNode StmtSeq()
        {
            var stmtSeq = new ParseNode(ParseNodeTag.StmtSeq);
            
            while (_tokens.Lookahead() != Tokens.EOF)
                stmtSeq.AddChild(Stmt());

            return stmtSeq;
        }

        /// <summary>
        /// statement
        /// : block
        /// | declaration_statement
        /// | expression_statement
        /// ;
        /// </summary>
        private ParseNode Stmt()
        {
            var stmt = new ParseNode(ParseNodeTag.Stmt);
            if (TryAddChild(stmt, Block, TokenTag.OPEN_CURLY_BRACE)) return Unwrap(stmt);
            if (TryAddChild(stmt, DeclarationStmt, TokenTag.VAR)) return Unwrap(stmt);
            if (TryAddChild(stmt, DeclarationStmt, PredictPredefinedType)) return Unwrap(stmt);
            return ExpressionStmt();
        }

        /// <summary>
        /// declaration_statement
        /// : var_declaration SEMICOLON
        /// ;
        /// </summary>
        private ParseNode DeclarationStmt()
        {
            var declrStmt = new ParseNode(ParseNodeTag.DeclarationStmt);
            declrStmt.AddChild(VarDeclaration());
            declrStmt.AddChild(Terminal(TokenTag.SEMICOLON));
            return declrStmt;
        }

        /// <summary>
        /// var_declaration
        /// : type_name ID ASSIGN var_initializer
        /// ;
        /// </summary>
        private ParseNode VarDeclaration()
        {
            var varDeclr = new ParseNode(ParseNodeTag.VarDeclaration);
            varDeclr.AddChild(TypeName());
            varDeclr.AddChild(Terminal(TokenTag.ID));
            varDeclr.AddChild(Terminal(TokenTag.ASSIGN));
            varDeclr.AddChild(VarInitializer());
            return varDeclr;
        }

        /// <summary>
        /// var_initializer
        /// : expression
        /// ;
        /// </summary>
        private ParseNode VarInitializer()
        {
            var varInit = new ParseNode(ParseNodeTag.VarInitializer);
            varInit.AddChild(Expression());
            return varInit;
        }

        /// <summary>
        /// type_name
        /// : predefined_type_name
        /// ;
        /// </summary>
        private ParseNode TypeName()
        {
            var typeName = new ParseNode(ParseNodeTag.TypeName);
            if (TryAddChild(typeName, PredefinedTypeName, PredictPredefinedType))
                return Unwrap(typeName);

            throw new ParseException(string.Format("Not supported type name {0}", _tokens.Lookahead()));
        }

        /// <summary>
        /// predefined_type_name
        /// : BOOL
        /// | BYTE
        /// | CHAR
        /// | DECIMAL
        /// | DOUBLE
        /// | FLOAT
        /// | INT
        /// | LONG
        /// | OBJECT
        /// | SBYTE
        /// | SHORT
        /// | STRING
        /// | UINT
        /// | ULONG
        /// | USHORT
        /// ;
        /// </summary>
        private ParseNode PredefinedTypeName()
        {
            var typeName = new ParseNode(ParseNodeTag.PredefinedTypeName);
            if (TryAddChild(typeName, Terminal, PredictPredefinedType))
                return typeName;

            throw new ParseException(string.Format("Token {0} is not supported as a predefined type name", _tokens.Lookahead()));
        }

        /// <summary>
        /// expression_statement
        /// : expression SEMICOLON
        /// ;
        /// </summary>
        private ParseNode ExpressionStmt()
        {
            var exprStmt = new ParseNode(ParseNodeTag.ExpressionStmt);
            exprStmt.AddChild(Expression());
            exprStmt.AddChild(Terminal(TokenTag.SEMICOLON));
            return exprStmt;
        }

        /// <summary>
        /// expression
        /// : condition_expression
        /// | condition_expression IS type_name
        /// | condition_expression AS type_name
        /// | condition_expression QUESTION expression COLON expression
        /// ;
        /// </summary>
        private ParseNode Expression()
        {
            var expr = new ParseNode(ParseNodeTag.Expression);
            expr.AddChild(ConditionExpression());

            if (TryAddChild(expr, Terminal, TokenTag.AS))
            {
                expr.AddChild(TypeName());
                return expr;
            }

            if (TryAddChild(expr, Terminal, TokenTag.IS))
            {
                expr.AddChild(TypeName());
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

        /// <summary>
        /// condition_expression
        /// : relation_expression
        /// | relation_expression OR condition_expression
        /// | relation_expression AND condition_expression
        /// | relation_expression BIT_OR condition_expression
        /// | relation_expression BIT_XOR condition_expression
        /// | relation_expression BIT_AND condition_expression
        /// ;
        /// </summary>
        private ParseNode ConditionExpression()
        {
            var expr = new ParseNode(ParseNodeTag.ConditionExpression);
            expr.AddChild(RelationExpression());

            if (TryAddChild(expr, Terminal, PredictCondition))
            {
                expr.AddChild(ConditionExpression());
                return expr;
            }

            return Unwrap(expr);
        }

        /// <summary>
        /// relation_expression
        /// : arithmetic_expression
        /// | arithmetic_expression EQUAL relation_expression
        /// | arithmetic_expression NOT_EQUAL relation_expression
        /// | arithmetic_expression LESS relation_expression
        /// | arithmetic_expression GREATER relation_expression
        /// | arithmetic_expression LESS_OR_EQUAL relation_expression
        /// | arithmetic_expression GREATER_OR_EQUAL relation_expression
        /// ;
        /// </summary>
        private ParseNode RelationExpression()
        {
            var expr = new ParseNode(ParseNodeTag.RelationExpression);
            expr.AddChild(ArithmeticExpression());

            if (TryAddChild(expr, Terminal, PredictRelation))
            {
                expr.AddChild(RelationExpression());
                return expr;
            }

            return Unwrap(expr);
        }

        /// <summary>
        /// arithmetic_expression
        /// : factor_expression
        /// | factor_expression PLUS arithmetic_expression
        /// | factor_expression MINUS arithmetic_expression
        /// ;
        /// </summary>
        private ParseNode ArithmeticExpression()
        {
            var expr = new ParseNode(ParseNodeTag.ArithmeticExpression);
            expr.AddChild(FactorExpression());

            if (TryAddChild(expr, Terminal, PredictArithmetic))
            {
                expr.AddChild(ArithmeticExpression());
                return expr;
            }

            return Unwrap(expr);
        }

        /// <summary>
        /// factor_expression
        /// : unary_expression
        /// | unary_expression MULTIPLY factor_expression
        /// | unary_expression DIVIDE factor_expression
        /// | unary_expression MOD factor_expression
        /// | unary_expression LEFT_SHIFT factor_expression
        /// | unary_expression RIGHT_SHIFT factor_expression
        /// ;
        /// </summary>
        private ParseNode FactorExpression()
        {
            var expr = new ParseNode(ParseNodeTag.FactorExpression);
            expr.AddChild(UnaryExpression());

            if (TryAddChild(expr, Terminal, PredictFactor))
            {
                expr.AddChild(FactorExpression());
                return expr;
            }

            return Unwrap(expr);
        }

        /// <summary>
        /// unary_expression
        /// | MINUS primary_expression
        /// | NOT primary_expression
        /// | MULTIPLY primary_expression
        /// | INCREMENT primary_expression
        /// | DECREMENT primary_expression
        /// | primary_expression
        /// ;
        /// </summary>
        private ParseNode UnaryExpression()
        {
            var expr = new ParseNode(ParseNodeTag.UnaryExpression);

            if (TryAddChild(expr, Terminal, PredictUnary))
            {
                expr.AddChild(PrimaryExpression());
                return expr;
            }

            return PrimaryExpression();
        }

        /// <summary>
        /// primary_expression
        /// : literal
        /// | cast_expression
        /// | parenthesis_expression
        /// | invocation_expression
        /// | element_access
        /// | var_access
        /// | object_creation_expression
        /// ;
        /// </summary>
        private ParseNode PrimaryExpression()
        {
            var expr = new ParseNode(ParseNodeTag.PrimaryExpression);
            if (TryAddChild(expr, Literal, PredictLiteral)) return expr;
            if (TryAddChild(expr, CastExpression, PredictCast)) return Unwrap(expr);
            if (TryAddChild(expr, ParenthesisExpression, TokenTag.OPEN_PAREN)) return Unwrap(expr);
            if (TryAddChild(expr, InvocationExpression, PredictInvocation)) return Unwrap(expr);
            if (TryAddChild(expr, ElementAccess, PredictElementAccess)) return Unwrap(expr);
            if (TryAddChild(expr, PostfixIncrement, PredictPostfixIncrement)) return Unwrap(expr);
            if (TryAddChild(expr, PostfixDecrement, PredictPostfixDecrement)) return Unwrap(expr);
            if (TryAddChild(expr, VarAccess, TokenTag.ID)) return Unwrap(expr);
            if (TryAddChild(expr, ObjectCreation, TokenTag.NEW)) return Unwrap(expr);
            return expr;
        }

        /// <summary>
        /// literal
        /// : INT_CONST
        /// | FLOAT_CONST
        /// | DOUBLE_CONST
        /// | TRUE
        /// | FALSE
        /// ;
        /// </summary>
        private ParseNode Literal()
        {
            var literal = new ParseNode(ParseNodeTag.Literal);
            literal.AddChild(Terminal());
            return literal;
        }

        /// <summary>
        /// cast_expression
        /// : OPEN_PAREN type_name CLOSE_PAREN unary_expression
        /// ;
        /// </summary>
        private ParseNode CastExpression()
        {
            var expr = new ParseNode(ParseNodeTag.CastExpression);
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(TypeName());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            expr.AddChild(UnaryExpression());
            return expr;
        }

        /// <summary>
        /// parenthesis_expression
        /// : OPEN_PAREN expression CLOSE_PAREN
        /// ;
        /// </summary>
        private ParseNode ParenthesisExpression()
        {
            var expr = new ParseNode(ParseNodeTag.ParenthesisExpression);
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(Expression());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            return expr;
        }

        /// <summary>
        /// invocation_expression
        /// : ID OPEN_PAREN argument_list CLOSE_PAREN
        /// ;
        /// </summary>
        private ParseNode InvocationExpression()
        {
            var expr = new ParseNode(ParseNodeTag.InvocationExpression);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(ArgumentList());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            return expr;
        }

        /// <summary>
        /// argument_list
        /// : argument
        /// | argument COMMA argument_list
        /// ;
        /// </summary>
        private ParseNode ArgumentList()
        {
            var args = new ParseNode(ParseNodeTag.ArgumentList);

            if (TryAddChild(args, Argument, itt => itt.Lookahead().Tag != TokenTag.CLOSE_PAREN))
            {
                while (TryAddChild(args, Terminal, TokenTag.COMMA))
                    args.AddChild(Argument());
            }

            return args;
        }

        /// <summary>
        /// argument
        /// : REF expression
        /// | OUT expression
        /// | expression
        /// ;
        /// </summary
        private ParseNode Argument()
        {
            var arg = new ParseNode(ParseNodeTag.Argument);
            TryAddChild(arg, Terminal, TokenTag.REF);
            TryAddChild(arg, Terminal, TokenTag.OUT);
            arg.AddChild(Expression());
            return arg;
        }

        /// <summary>
        /// element_access
        /// : ID OPEN_SQUARE_BRACE expression_list CLOSE_SQUARE_BRACE
        /// ;
        /// </summary>
        private ParseNode ElementAccess()
        {
            var expr = new ParseNode(ParseNodeTag.ElementAccess);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.OPEN_SQUARE_BRACE));
            expr.AddChild(ExpressionList());
            expr.AddChild(Terminal(TokenTag.CLOSE_SQUARE_BRACE));
            return expr;
        }

        /// <summary>
        /// expression_list
        /// : expression
        /// | expression COMMA expression_list
        /// ;
        /// </summary>
        private ParseNode ExpressionList()
        {
            var exprs = new ParseNode(ParseNodeTag.ExpressionList);

            if (TryAddChild(exprs, Expression, itt => itt.Lookahead().Tag != TokenTag.CLOSE_SQUARE_BRACE))
            {
                while (TryAddChild(exprs, Terminal, TokenTag.COMMA))
                    exprs.AddChild(Expression());
            }

            return exprs;
        }

        /// <summary>
        /// postfix_increment
        /// : ID INCREMENT
        /// ;
        /// </summary>
        private ParseNode PostfixIncrement()
        {
            var expr = new ParseNode(ParseNodeTag.PostfixIncrement);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.INCREMENT));
            return expr;
        }

        /// <summary>
        /// postfix_decrement
        /// : ID DECREMENT
        /// ;
        /// </summary>
        private ParseNode PostfixDecrement()
        {
            var expr = new ParseNode(ParseNodeTag.PostfixDecrement);
            expr.AddChild(Terminal(TokenTag.ID));
            expr.AddChild(Terminal(TokenTag.DECREMENT));
            return expr;
        }

        /// <summary>
        /// var_access
        /// : ID
        /// ;
        /// </summary>
        private ParseNode VarAccess()
        {
            var expr = new ParseNode(ParseNodeTag.VarAccess);
            expr.AddChild(Terminal(TokenTag.ID));
            return expr;
        }

        /// <summary>
        /// object_creation
        /// : NEW type_name OPEN_PAREN argument_list CLOSE_PAREN
        /// | NEW type_name OPEN_PAREN CLOSE_PAREN
        /// ;
        /// </summary
        private ParseNode ObjectCreation()
        {
            var expr = new ParseNode(ParseNodeTag.ObjectCreation);
            expr.AddChild(Terminal(TokenTag.NEW));
            expr.AddChild(TypeName());
            expr.AddChild(Terminal(TokenTag.OPEN_PAREN));
            expr.AddChild(ArgumentList());
            expr.AddChild(Terminal(TokenTag.CLOSE_PAREN));
            return expr;
        }
        #endregion

        #region helper methods
        /// <summary>
        /// Try add child parse node with one-step prediction
        /// </summary>
        private bool TryAddChild(ParseNode node, Func<ParseNode> production, TokenTag reqTokenTag)
        {
            return TryAddChild(node, production, () => PredictOneStep(_tokens, reqTokenTag));
        }

        /// <summary>
        /// Try add child parse node with multi-step prediction
        /// </summary>
        private bool TryAddChild(ParseNode node, Func<ParseNode> production, params TokenTag[] reqTokenTags)
        {
            return TryAddChild(node, production, () => PredictMultyStep(_tokens, reqTokenTags));
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

        private bool TryAddChild(ParseNode node, Func<ParseNode> production, Func<TokenIterator, bool> prediction)
        {
            if (prediction?.Invoke(_tokens) ?? false)
            {
                node.AddChild(production?.Invoke());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ugly way to implement backtracking. 
        /// TODO: think about it.
        /// UPD: probably it can be better to return productions to invoke 
        /// instead of invoking them directly
        /// </summary>
        //private bool TryAddChild(ParseNode node, Func<ParseNode> production)
        //{
        //    int position = _tokens.Position;
        //    try
        //    {
        //        node.AddChild(production?.Invoke());
        //        return true;
        //    }
        //    catch(ParseException)
        //    {
        //        _tokens.Reset(position);
        //        return false;
        //    }
        //}

        /// <summary>
        /// Derivate terminal production from current token
        /// without any requirements
        /// </summary>
        private ParseNode Terminal()
        {
            if (!_tokens.MoveNext() || _tokens.Current == Tokens.EOF)
                throw new ParseException($"Input stream finished");

            return new ParseNode(_tokens.Current);
        }

        /// <summary>
        /// Derivate factorinal production from current token
        /// with require token tag
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
            if (!_tokens.MoveNext() || _tokens.Current == Tokens.EOF)
                throw new ParseException("Input stream finished");

            var currToken = _tokens.Current;
            if (currToken.Tag != reqTokenTag)
                throw new UnexpectedTokenException(reqTokenTag, currToken);

            return currToken;
        }
        #endregion

        #region predictions
        private static bool PredictOneStep(TokenIterator iterator, TokenTag tokenTag)
        {
            var token = iterator.Lookahead();
            return token.Tag == tokenTag;
        }

        private static bool PredictMultyStep(TokenIterator iterator, params TokenTag[] tokenTags)
        {
            foreach (var tokenTag in tokenTags)
            {
                if (iterator.Lookahead().Tag != tokenTag)
                    return false;
            }
            return true;
        }

        private static bool PredictPredefinedType(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsPredefinedTypeToken();
        }

        private static bool PredictUnary(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsUnaryToken();
        }

        private static bool PredictRelation(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsRelationToken();
        }

        private static bool PredictArithmetic(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsArithmeticToken();
        }

        private static bool PredictFactor(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsFactorToken();
        }

        private static bool PredictLiteral(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsLiteralToken();
        }

        private bool PredictCondition(TokenIterator iterator)
        {
            var token = iterator.Lookahead();
            return token.IsConditionToken();
        }

        private static bool PredictCast(TokenIterator iterator)
        {
            if (iterator.Lookahead(1).Tag != TokenTag.OPEN_PAREN) return false;
            if (!(iterator.Lookahead(2) is TypeToken)) return false;
            if (iterator.Lookahead(3).Tag != TokenTag.CLOSE_PAREN) return false;
            return true;
        }

        private static bool PredictInvocation(TokenIterator iterator)
        {
            if (iterator.Lookahead(1).Tag != TokenTag.ID) return false;
            if (iterator.Lookahead(2).Tag != TokenTag.OPEN_PAREN) return false;
            return true;
        }

        private static bool PredictElementAccess(TokenIterator iterator)
        {
            if (iterator.Lookahead(1).Tag != TokenTag.ID) return false;
            if (iterator.Lookahead(2).Tag != TokenTag.OPEN_SQUARE_BRACE) return false;
            return true;
        }

        private static bool PredictPostfixIncrement(TokenIterator iterator)
        {
            if (iterator.Lookahead(1).Tag != TokenTag.ID) return false;
            if (iterator.Lookahead(2).Tag != TokenTag.INCREMENT) return false;
            return true;
        }

        private static bool PredictPostfixDecrement(TokenIterator iterator)
        {
            if (iterator.Lookahead(1).Tag != TokenTag.ID) return false;
            if (iterator.Lookahead(2).Tag != TokenTag.INCREMENT) return false;
            return true;
        }
        #endregion
    }
}
