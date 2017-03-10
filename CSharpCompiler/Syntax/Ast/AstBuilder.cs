using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Statements;
using CSharpCompiler.Syntax.Ast.Types;
using System.Linq;
using System;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class AstBuilder
    {
        public static SyntaxTree Build(ParseTree parseTree)
        {
            return new AstBuilder().SyntaxTree(parseTree);
        }

        private SyntaxTree SyntaxTree(ParseTree parseTree)
        {
            var stmts = parseTree.Children
                .EmptyIfNull()
                .Where(child => child.Tag == ParseNodeTag.StmtSeq)
                .SelectMany(child => child.Children)
                .Select(child => Stmt(child));

            return new SyntaxTree(stmts);
        }

        private Stmt Stmt(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.DeclarationStmt:
                    return DeclarationStmt(node);

                case ParseNodeTag.ExpressionStmt:
                    return ExpressionStmt(node);
            }

            throw new SyntaxException(string.Format("Unsupported statement: {0}", node));
        }

        private DeclarationStmt DeclarationStmt(ParseNode node)
        {
            var declaration = VarDeclaration(node.Children[0]);
            return new DeclarationStmt(declaration);
        }

        private VarDeclaration VarDeclaration(ParseNode node)
        {
            var typeNode = node.Children[0];
            var declarators = node.Children
                .Where(child => child.Tag == ParseNodeTag.VarDeclarator)
                .Select(child => VarDeclarator(child));

            if (typeNode.Token == Tokens.VAR)
                return new VarDeclaration(declarators);

            var type = Type(typeNode);
            return new VarDeclaration(type, declarators);
        }

        private VarDeclarator VarDeclarator(ParseNode parse)
        {
            var name = VarLocation(parse.Children[0]);
            var value = Expression(parse.Children[2]);
            return new VarDeclarator(name, value);
        }

        private VarLocation VarLocation(ParseNode node)
        {
            return new VarLocation(node.Children[0].Token.Lexeme);
        }

        private ExpressionStmt ExpressionStmt(ParseNode node)
        {
            var expr = Expression(node.Children[0]);
            return new ExpressionStmt(expr);
        }

        private Expression Expression(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.ArithmeticExpression:
                case ParseNodeTag.FactorExpression:
                case ParseNodeTag.ConditionExpression:
                case ParseNodeTag.RelationExpression:
                    return BinaryOperation(node);

                case ParseNodeTag.UnaryExpression:
                    return UnaryOperation(node);

                case ParseNodeTag.CastExpression:
                    return CastExpression(node);

                case ParseNodeTag.Literal:
                    return Literal(node);

                case ParseNodeTag.VarLocation:
                    return VarLocation(node);

                case ParseNodeTag.InvokeExpression:
                    return InvocationExpression(node);
            }

            if (node.Tag == ParseNodeTag.Expression)
            {
                if (node.Children.Any(child => child.Token == Tokens.AS))
                    return AsOperation(node);

                if (node.Children.Any(child => child.Token == Tokens.IS))
                    return IsOperation(node);

                if (node.Children.Count == 5)
                    return TernaryOperation(node);
            }

            throw new SyntaxException(string.Format("Unsupported expression: {0}", node));
        }

        private InvokeExpression InvocationExpression(ParseNode node)
        {
            var methodName = node.Children[0].Token.Lexeme;
            var args = node.Children
                .Where(child => child.Tag == ParseNodeTag.ArgumentList)
                .SelectMany(child => child.Children)
                .Select(child => Argument(child));

            return new InvokeExpression(methodName, args);
        }

        private Argument Argument(ParseNode node)
        {
            var modifier = node.Children
                .Where(child => child.Token == Tokens.REF || child.Token == Tokens.OUT)
                .Select(child => child.Token)
                .FirstOrDefault();

            var value = Expression(node.Children.Single(child => !child.IsTerminal));
            return new Argument(value);
        }

        private Expression IsOperation(ParseNode node)
        {
            var operand = Expression(node.Children[0]);
            var type = Type(node.Children[2]);
            return new IsOperation(operand, type);
        }

        private Expression AsOperation(ParseNode node)
        {
            var operand = Expression(node.Children[0]);
            var type = Type(node.Children[2]);
            return new AsOperation(operand, type);
        }

        private Expression TernaryOperation(ParseNode node)
        {
            var condition = Expression(node.Children[0]);
            var trueBranch = Expression(node.Children[2]);
            var falseBranch = Expression(node.Children[4]);
            return new TernaryOperation(condition, trueBranch, falseBranch);
        }

        private BinaryOperation BinaryOperation(ParseNode node)
        {
            var leftOperand = Expression(node.Children[0]);
            var @operator = node.Children[1];
            var rightOperand = Expression(node.Children[2]);
            return new BinaryOperation(@operator.Token, leftOperand, rightOperand);
        }

        private UnaryOperation UnaryOperation(ParseNode node)
        {
            var @operator = node.Children[0];
            var operand = Expression(node.Children[1]);
            return new UnaryOperation(@operator.Token, operand);
        }

        private CastExpression CastExpression(ParseNode node)
        {
            var type = Type(node.Children[1]);
            var operand = Expression(node.Children[3]);
            return new CastExpression(type, operand);
        }

        private Literal Literal(ParseNode node)
        {
            return new Literal(node.Children[0].Token);
        }

        private AstType Type(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.PrimitiveType:
                    return PrimitiveType(node);
            }

            throw new SyntaxException(string.Format("Unsupported type: {0}", node));
        }

        private PrimitiveType PrimitiveType(ParseNode node)
        {
            return new PrimitiveType(node.Children[0].Token);
        }
    }
}
