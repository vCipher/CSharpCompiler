using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Statements;
using CSharpCompiler.Syntax.Ast.Types;
using System.Linq;
using System.Collections.Generic;
using CSharpCompiler.Utility;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class AstBuilder
    {
        private ParseTree _parseTree;
        private Stack<VarScope> _scopes;

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
                .EmptyIfNull()
                .Where(child => child.Tag == ParseNodeTag.StmtSeq)
                .SelectMany(child => child.Children)
                .Select(child => Stmt(child))
                .ToList();

            //return new SyntaxTree(_scopes.Pop(), stmts);
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

            throw new SyntaxException("Unsupported statement: {0}", node);
        }

        private DeclarationStmt DeclarationStmt(ParseNode node)
        {
            var scope = _scopes.Peek();
            var declarations = VarDeclarations(node.Children[0]);

            foreach (var declaration in declarations)
                scope.Register(declaration.VarName, declaration);

            return new DeclarationStmt(declarations);
        }

        private List<VarDeclaration> VarDeclarations(ParseNode node)
        {
            var typeNode = node.Children[0];
            if (typeNode.Token == Tokens.VAR)
            {
                return node.Children
                    .Where(child => child.Tag == ParseNodeTag.VarDeclarator)
                    .Select(child => VarDeclaration(child))
                    .ToList();
            }

            var type = Type(typeNode);
            return node.Children
                .Where(child => child.Tag == ParseNodeTag.VarDeclarator)
                .Select(child => VarDeclaration(type, child))
                .ToList();
        }

        private VarDeclaration VarDeclaration(ParseNode node)
        {
            var varName = node.Children[0].Token.Lexeme;

            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                throw new DublicateVariableDeclarationException(varName);

            // todo: implement uninitialized variables
            var initializer = Expression(node.Children[2]);
            var scope = _scopes.Peek();
            return new VarDeclaration(varName, initializer, scope);
        }

        private VarDeclaration VarDeclaration(AstType type, ParseNode node)
        {
            var varName = node.Children[0].Token.Lexeme;

            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                throw new DublicateVariableDeclarationException(varName);

            // todo: implement uninitialized variables
            var initializer = Expression(node.Children[2]);
            var scope = _scopes.Peek();
            return new VarDeclaration(type, varName, initializer, scope);
        }

        private VarAccess VarAccess(ParseNode node)
        {
            var varName = node.Children[0].Token.Lexeme;
            var currentScope = _scopes.Peek();
            
            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
            {
                return new VarAccess(varName, currentScope);
            }

            throw new UndefinedVariableException(varName);
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
                case ParseNodeTag.RelationExpression:
                    return BinaryOperation(node);

                // todo: implement
                //case ParseNodeTag.ConditionExpression:
                //    return ConditionExpression(node);

                case ParseNodeTag.UnaryExpression:
                    return UnaryOperation(node);

                case ParseNodeTag.CastExpression:
                    return CastExpression(node);

                case ParseNodeTag.Literal:
                    return Literal(node);

                case ParseNodeTag.VarAccess:
                    return VarAccess(node);

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

            if (node.Tag == ParseNodeTag.ParenthesisExpression)
                return Expression(node.Children[1]);

            throw new SyntaxException("Unsupported expression: {0}", node);
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

            throw new SyntaxException("Unsupported type: {0}", node);
        }

        private PrimitiveType PrimitiveType(ParseNode node)
        {
            return new PrimitiveType(node.Children[0].Token);
        }

        private bool SeekVariable(string varName, out VarDeclaration declaration)
        {
            // todo: implement scope backward seek
            return _scopes.Peek().TryResolve(varName, out declaration);
        }
    }
}
