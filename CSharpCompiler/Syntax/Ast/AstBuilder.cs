﻿using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Statements;
using CSharpCompiler.Syntax.Ast.Types;
using System.Linq;
using System.Collections.Generic;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class AstBuilder
    {
        private ParseTree _parseTree;
        private Stack<VarScope> _scopes;

        private VarScope CurrentScope
        {
            get { return _scopes.Peek(); }
        }

        public AstBuilder(ParseTree parseTree)
        {
            _parseTree = parseTree;
            _scopes = new Stack<VarScope>();
        }

        public static SyntaxTree Build(ParseTree parseTree)
        {
            return new AstBuilder(parseTree).SyntaxTree();
        }

        private SyntaxTree SyntaxTree()
        {
            _scopes.Push(new VarScope());

            var stmts = _parseTree.Children
                .GetAndMove(ParseNodeTag.StmtSeq, child => StmtSequance(child));
            
            return new SyntaxTree(stmts);
        }

        private List<Stmt> StmtSequance(ParseNode node)
        {
            return node.Children
                .EmptyIfNull()
                .Select(child => Stmt(child))
                .ToList();
        }

        private Stmt Stmt(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.DeclarationStmt: return DeclarationStmt(node);
                case ParseNodeTag.ExpressionStmt: return ExpressionStmt(node);
                case ParseNodeTag.ForStmt: return ForStmt(node);
            }

            throw new SyntaxException("Unsupported statement: {0}", node);
        }

        private ForStmt ForStmt(ParseNode node)
        {
            _scopes.Push(new VarScope());

            var children = node.Children;

            children.Skip(TokenTag.FOR);
            children.Skip(TokenTag.OPEN_PAREN);
            var declarations = children.Check(ParseNodeTag.VarDeclaration)
                ? children.GetAndMove(ParseNodeTag.VarDeclaration, child => VarDeclarations(child))
                : new List<VarDeclaration>();

            children.Skip(TokenTag.SEMICOLON);
            var condition = children.Check(ParseNodeTag.Expression)
                ? children.GetAndMove(ParseNodeTag.Expression, child => Expression(child))
                : new EmptyExpression();

            children.Skip(TokenTag.SEMICOLON);
            var postIteration = children.Check(ParseNodeTag.Expression)
                ? children.GetAndMove(ParseNodeTag.Expression, child => Expression(child))
                : new EmptyExpression();

            children.Skip(TokenTag.CLOSE_PAREN);
            var body = children.Check(ParseNodeTag.Block)
                ? children.GetAndMove(ParseNodeTag.Block, child => Block(child))
                : children.GetAndMove(ParseNodeTag.ExpressionStmt, child => new List<Stmt>() { ExpressionStmt(child) });
            
            _scopes.Pop();
            
            return new ForStmt(declarations, condition, postIteration, body);
        }

        private List<Stmt> Block(ParseNode node)
        {
            var children = node.Children;
            var stmts = children
                .Skip(TokenTag.OPEN_CURLY_BRACE)
                .GetAndMove(ParseNodeTag.StmtSeq, child => StmtSequance(child));

            return stmts;
        }

        private DeclarationStmt DeclarationStmt(ParseNode node)
        {
            var declarations = node.Children
                .GetAndMove(ParseNodeTag.VarDeclaration, child => VarDeclarations(child));

            return new DeclarationStmt(declarations);
        }

        private List<VarDeclaration> VarDeclarations(ParseNode node)
        {
            var factory = GetVarDeclarationFactory(node);
            var children = node.Children
                .GetAndMove(ParseNodeTag.VarDeclaratorList)
                .Children;

            return children
                .Where(child => !child.IsTerminal)
                .Select(child => factory(child))
                .ToList();
        }

        private Func<ParseNode, VarDeclaration> GetVarDeclarationFactory(ParseNode node)
        {
            var children = node.Children;
            if (children.Check(TokenTag.VAR))
                return child => ImplicitVarDeclaration(child);

            var type = children.GetAndMove(ParseNodeTag.Type, child => Type(child));
            return child => ExplicitVarDeclaration(type, child);
        }

        private VarDeclaration ImplicitVarDeclaration(ParseNode node)
        {
            var children = node.Children;
            var varName = children.GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;

            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                throw new DublicateVariableDeclarationException(varName);

            if (!children.Check(TokenTag.ASSIGN))
                return new VarDeclaration(varName, null, CurrentScope);
            
            var initializer = children
                .Skip(TokenTag.ASSIGN)
                .GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new VarDeclaration(varName, initializer, CurrentScope);
        }

        private VarDeclaration ExplicitVarDeclaration(AstType type, ParseNode node)
        {
            var children = node.Children;
            var varName = children.GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;

            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                throw new DublicateVariableDeclarationException(varName);

            if (!children.Check(TokenTag.ASSIGN))
                return new VarDeclaration(type, varName, null, CurrentScope);

            var initializer = children
                .Skip(TokenTag.ASSIGN)
                .GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new VarDeclaration(type, varName, initializer, CurrentScope);
        }

        private VarAccess VarAccess(ParseNode node)
        {
            var varName = node.Children.GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;
            
            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                return new VarAccess(varName, CurrentScope);

            throw new UndefinedVariableException(varName);
        }

        private ExpressionStmt ExpressionStmt(ParseNode node)
        {
            var expr = node.Children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            return new ExpressionStmt(expr);
        }

        private Expression Expression(ParseNode node)
        {
            var child = node.Children.First();
            switch (child.Tag)
            {
                case ParseNodeTag.Assignment: return Assignment(child);
                case ParseNodeTag.ConditionalAndExpression: return AndOperation(child);
                case ParseNodeTag.ConditionalOrExpression: return OrOperation(child);
                case ParseNodeTag.RelationalExpression: return RelationalOperation(child);
                case ParseNodeTag.EqualityExpression: return RelationalOperation(child);
                case ParseNodeTag.TernaryExpression: return TernaryOperation(child);
                case ParseNodeTag.MultiplicativeExpression: return ArithmeticOperation(child);
                case ParseNodeTag.AdditiveExpression: return ArithmeticOperation(child);
                case ParseNodeTag.ShiftExpression: return ArithmeticOperation(child);
                case ParseNodeTag.BitAndExpression: return ArithmeticOperation(child);
                case ParseNodeTag.BitXorExpression: return ArithmeticOperation(child);
                case ParseNodeTag.BitOrExpression: return ArithmeticOperation(child);
                case ParseNodeTag.UnaryExpression: return UnaryOperation(child);
                case ParseNodeTag.CastExpression: return CastExpression(child);
                case ParseNodeTag.Literal: return Literal(child);
                case ParseNodeTag.VarAccess: return VarAccess(child);
                case ParseNodeTag.InvokeExpression: return InvocationExpression(child);
                case ParseNodeTag.ParenthesisExpression: return ParenthesisExpression(child);
                case ParseNodeTag.PostfixIncrement: return PostfixIncrement(child);
                case ParseNodeTag.PostfixDecrement: return PostfixDecrement(child);
            }

            throw new SyntaxException("Unsupported expression: {0}", child);
        }

        private PostfixDecrement PostfixDecrement(ParseNode node)
        {
            var operand = node.Children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            return new PostfixDecrement(operand);
        }

        private PostfixIncrement PostfixIncrement(ParseNode node)
        {
            var operand = node.Children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            return new PostfixIncrement(operand);
        }

        private Expression ParenthesisExpression(ParseNode node)
        {
            return node.Children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(ParseNodeTag.Expression, child => Expression(child));
        }

        private InvokeExpression InvocationExpression(ParseNode node)
        {
            var children = node.Children;
            var methodName = children.GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;
            var args = children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(ParseNodeTag.ArgumentList, child => Arguments(child));

            return new InvokeExpression(methodName, args);
        }

        private List<Argument> Arguments(ParseNode node)
        {
            return node.Children
                .Where(child => !child.IsTerminal)
                .Select(child => Argument(child))
                .ToList();
        }

        private Argument Argument(ParseNode node)
        {
            var children = node.Children;
            var modifier = (children.Check(TokenTag.REF) || children.Check(TokenTag.OUT))
                ? children.GetAndMove(ParseNodeTag.Terminal).Token
                : default(Token?);
            var value = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new Argument(modifier, value);
        }

        private AndOperation AndOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new AndOperation(@operator, leftOperand, rightOperand);
        }

        private OrOperation OrOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new OrOperation(@operator, leftOperand, rightOperand);
        }

        private Assignment Assignment(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new Assignment(@operator, leftOperand, rightOperand);
        }

        private Expression RelationalOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            if (children.Check(TokenTag.IS)) return IsOperation(leftOperand, children);
            if (children.Check(TokenTag.AS)) return AsOperation(leftOperand, children);

            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new RelationOperation(@operator, leftOperand, rightOperand);
        }

        private Expression IsOperation(Expression operand, ParseNodeCollection children)
        {
            var type = children
                .Skip(TokenTag.IS)
                .GetAndMove(ParseNodeTag.Type, child => Type(child));

            return new IsOperation(operand, type);
        }

        private Expression AsOperation(Expression operand, ParseNodeCollection children)
        {
            var type = children
                .Skip(TokenTag.AS)
                .GetAndMove(ParseNodeTag.Type, child => Type(child));

            return new AsOperation(operand, type);
        }

        private Expression TernaryOperation(ParseNode node)
        {
            var children = node.Children;
            var condition = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            var trueBranch = children
                .Skip(TokenTag.QUESTION)
                .GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            var falseBranch = children
                .Skip(TokenTag.COLON)
                .GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new TernaryOperation(condition, trueBranch, falseBranch);
        }

        private ArithmeticOperation ArithmeticOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new ArithmeticOperation(@operator, leftOperand, rightOperand);
        }

        private UnaryOperation UnaryOperation(ParseNode node)
        {
            var children = node.Children;
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var operand = children.GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new UnaryOperation(@operator, operand);
        }

        private CastExpression CastExpression(ParseNode node)
        {
            var children = node.Children;
            var type = children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(ParseNodeTag.Type, child => Type(child));

            var operand = children
                .Skip(TokenTag.CLOSE_PAREN)
                .GetAndMove(ParseNodeTag.Expression, child => Expression(child));

            return new CastExpression(type, operand);
        }

        private Literal Literal(ParseNode node)
        {
            var token = node.Children
                .GetAndMove(ParseNodeTag.Terminal)
                .Token;

            return new Literal(token);
        }

        private AstType Type(ParseNode node)
        {
            var child = node.Children.First();
            switch (child.Tag)
            {
                case ParseNodeTag.PrimitiveType: return PrimitiveType(child);
            }

            throw new SyntaxException("Unsupported type: {0}", child);
        }

        private PrimitiveType PrimitiveType(ParseNode node)
        {
            var token = node.Children
                .GetAndMove(ParseNodeTag.Terminal)
                .Token;

            return new PrimitiveType(token);
        }

        private bool SeekVariable(string varName, out VarDeclaration declaration)
        {
            // todo: implement scope backward seek
            return _scopes.Peek().TryResolve(varName, out declaration);
        }
    }
}
