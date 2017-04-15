using CSharpCompiler.Lexica.Tokens;
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

            return _parseTree.Children
                .GetAndMove(StmtSequance)
                .Pipe(stmts => new SyntaxTree(stmts));
        }

        #region statements
        private List<Stmt> StmtSequance(ParseNode node)
        {
            return node.Children
                .EmptyIfNull()
                .Select(Stmt)
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
                ? children.GetAndMove(VarDeclarations)
                : new List<VarDeclaration>();

            children.Skip(TokenTag.SEMICOLON);
            var condition = children.Check(child => !child.IsTerminal)
                ? children.GetAndMove(Expression)
                : new EmptyExpression();

            children.Skip(TokenTag.SEMICOLON);
            var postIteration = children.Check(child => !child.IsTerminal)
                ? children.GetAndMove(StmtExpression)
                : new EmptyExpression();

            children.Skip(TokenTag.CLOSE_PAREN);
            var body = children.Check(ParseNodeTag.Block)
                ? children.GetAndMove(Block)
                : children.GetAndMove(ExpressionStmt)
                    .ToSingletonList<Stmt>();
            
            _scopes.Pop();
            
            return new ForStmt(declarations, condition, postIteration, body);
        }

        private List<Stmt> Block(ParseNode node)
        {
            return node.Children
                .Skip(TokenTag.OPEN_CURLY_BRACE)
                .GetAndMove(StmtSequance);
        }

        private DeclarationStmt DeclarationStmt(ParseNode node)
        {
            return node.Children
                .GetAndMove(VarDeclarations)
                .Pipe(declarations => new DeclarationStmt(declarations));
        }

        private List<VarDeclaration> VarDeclarations(ParseNode node)
        {
            var factory = GetVarDeclarationFactory(node);
            var children = node.Children
                .GetAndMove(ParseNodeTag.VarDeclaratorList)
                .Children;

            return children
                .Where(child => !child.IsTerminal)
                .Select(factory)
                .ToList();
        }

        private Func<ParseNode, VarDeclaration> GetVarDeclarationFactory(ParseNode node)
        {
            if (node.Children.Check(TokenTag.VAR))
                return child => ImplicitVarDeclaration(child);

            return node.Children
                .GetAndMove(child => Type(child))
                .Pipe(type => (Func<ParseNode, VarDeclaration>)(child => ExplicitVarDeclaration(type, child)));
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
                return new VarDeclaration(varName, null, GetCurrentScope());
            
            var initializer = children
                .Skip(TokenTag.ASSIGN)
                .GetAndMove(Expression);

            return new VarDeclaration(varName, initializer, GetCurrentScope());
        }

        private VarDeclaration ExplicitVarDeclaration(TypeNode type, ParseNode node)
        {
            var children = node.Children;
            var varName = children.GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;

            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                throw new DublicateVariableDeclarationException(varName);

            if (!children.Check(TokenTag.ASSIGN))
                return new VarDeclaration(type, varName, null, GetCurrentScope());

            var initializer = children
                .Skip(TokenTag.ASSIGN)
                .GetAndMove(Expression);

            return new VarDeclaration(type, varName, initializer, GetCurrentScope());
        }
        #endregion

        #region expressions
        private VarAccess VarAccess(ParseNode node)
        {
            var varName = node.Children
                .GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;
            
            VarDeclaration declaration;
            if (SeekVariable(varName, out declaration))
                return new VarAccess(varName, declaration.Scope);

            throw new UndefinedVariableException(varName);
        }

        private ExpressionStmt ExpressionStmt(ParseNode node)
        {
            return node.Children
                .GetAndMove(StmtExpression)
                .Pipe(expr => new ExpressionStmt(expr));
        }

        private Expression StmtExpression(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.InvokeExpression: return InvocationExpression(node, true);
                case ParseNodeTag.PostfixIncrement: return PostfixIncrement(node, true);
                case ParseNodeTag.PostfixDecrement: return PostfixDecrement(node, true);
                case ParseNodeTag.PrefixIncrement: return PrefixIncrement(node, true);
                case ParseNodeTag.PrefixDecrement: return PrefixDecrement(node, true);
                case ParseNodeTag.Assignment: return Assignment(node, true);
                case ParseNodeTag.ArrayCreation: return ArrayCreation(node, true);
                case ParseNodeTag.ObjectCreation: return ObjectCreation(node, true);
                case ParseNodeTag.ElementStore: return ElementStore(node, true);
            }

            throw new SyntaxException("Only assignment, call, increment, decrement, and new object expressions can be used as a statement");
        }

        private Expression Expression(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.Assignment: return Assignment(node);
                case ParseNodeTag.ConditionalAndExpression: return AndOperation(node);
                case ParseNodeTag.ConditionalOrExpression: return OrOperation(node);
                case ParseNodeTag.RelationalExpression: return RelationalOperation(node);
                case ParseNodeTag.EqualityExpression: return RelationalOperation(node);
                case ParseNodeTag.TernaryExpression: return TernaryOperation(node);
                case ParseNodeTag.MultiplicativeExpression: return ArithmeticOperation(node);
                case ParseNodeTag.AdditiveExpression: return ArithmeticOperation(node);
                case ParseNodeTag.ShiftExpression: return ArithmeticOperation(node);
                case ParseNodeTag.BitAndExpression: return ArithmeticOperation(node);
                case ParseNodeTag.BitXorExpression: return ArithmeticOperation(node);
                case ParseNodeTag.BitOrExpression: return ArithmeticOperation(node);
                case ParseNodeTag.UnaryExpression: return UnaryOperation(node);
                case ParseNodeTag.CastExpression: return CastExpression(node);
                case ParseNodeTag.Literal: return Literal(node);
                case ParseNodeTag.VarAccess: return VarAccess(node);
                case ParseNodeTag.InvokeExpression: return InvocationExpression(node);
                case ParseNodeTag.ParenthesisExpression: return ParenthesisExpression(node);
                case ParseNodeTag.PostfixIncrement: return PostfixIncrement(node);
                case ParseNodeTag.PostfixDecrement: return PostfixDecrement(node);
                case ParseNodeTag.PrefixIncrement: return PrefixIncrement(node);
                case ParseNodeTag.PrefixDecrement: return PrefixDecrement(node);
                case ParseNodeTag.ArrayCreation: return ArrayCreation(node);
                case ParseNodeTag.ObjectCreation: return ObjectCreation(node);
                case ParseNodeTag.ElementAccess: return ElementAccess(node);
                case ParseNodeTag.ElementStore: return ElementStore(node);
            }

            throw new SyntaxException("Unsupported expression: {0}", node);
        }

        private ElementAccess ElementAccess(ParseNode node)
        {
            var array = node.Children
                .GetAndMove(Expression);

            var index = node.Children
                .Skip(TokenTag.OPEN_SQUARE_BRACE)
                .GetAndMove(Expression);

            return new ElementAccess(array, index);
        }

        private ElementStore ElementStore(ParseNode node, bool isStmtExpression = false)
        {
            var array = node.Children
                .GetAndMove(Expression);

            var index = node.Children
                .Skip(TokenTag.OPEN_SQUARE_BRACE)
                .GetAndMove(Expression);

            var value = node.Children
                .Skip(TokenTag.CLOSE_SQUARE_BRACE)
                .Skip(TokenTag.ASSIGN)
                .GetAndMove(Expression);

            return new ElementStore(array, index, value, isStmtExpression);
        }

        private ArrayCreation ArrayCreation(ParseNode node, bool isStmtExpression = false)
        {
            var containedType = node.Children
                .Skip(TokenTag.NEW)
                .GetAndMove(NotArrayType);

            return node.Children
                .Skip(TokenTag.OPEN_SQUARE_BRACE)
                .GetAndMove(Expression)
                .Pipe(initializer => new ArrayCreation(containedType, initializer, isStmtExpression));
        }

        private ObjectCreation ObjectCreation(ParseNode node, bool isStmtExpression = false)
        {
            var type = node.Children
                .Skip(TokenTag.NEW)
                .GetAndMove(Type);

            return node.Children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(Arguments)
                .Pipe(args => new ObjectCreation(type, args, isStmtExpression));
        }

        private PostfixIncrement PostfixIncrement(ParseNode node, bool isStmtExpression = false)
        {
            return node.Children
                .GetAndMove(Expression)
                .Pipe(operand => new PostfixIncrement(operand, isStmtExpression));
        }

        private PostfixDecrement PostfixDecrement(ParseNode node, bool isStmtExpression = false)
        {
            return node.Children
                .GetAndMove(Expression)
                .Pipe(operand => new PostfixDecrement(operand, isStmtExpression));
        }

        private PrefixIncrement PrefixIncrement(ParseNode node, bool isStmtExpression = false)
        {
            return node.Children
                .Skip(TokenTag.INCREMENT)
                .GetAndMove(Expression)
                .Pipe(operand => new PrefixIncrement(operand, isStmtExpression));
        }

        private PrefixDecrement PrefixDecrement(ParseNode node, bool isStmtExpression = false)
        {
            return node.Children
                .GetAndMove(Expression)
                .Pipe(operand => new PrefixDecrement(operand, isStmtExpression));
        }

        private Expression ParenthesisExpression(ParseNode node)
        {
            return node.Children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(Expression);
        }

        private InvokeExpression InvocationExpression(ParseNode node, bool isStmtExpression = false)
        {
            var methodName = node.Children
                .GetAndMove(ParseNodeTag.Terminal)
                .Token
                .Lexeme;

            return node.Children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(Arguments)
                .Pipe(args => new InvokeExpression(methodName, args, isStmtExpression));
        }

        private List<Argument> Arguments(ParseNode node)
        {
            return node.Children
                .Where(child => !child.IsTerminal)
                .Select(Argument)
                .ToList();
        }

        private Argument Argument(ParseNode node)
        {
            var children = node.Children;
            var modifier = (children.Check(TokenTag.REF) || children.Check(TokenTag.OUT))
                ? children.GetAndMove(ParseNodeTag.Terminal).Token
                : default(Token?);

            return children.GetAndMove(Expression)
                .Pipe(value => new Argument(modifier, value));
        }

        private AndOperation AndOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(Expression);
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(Expression);

            return new AndOperation(@operator, leftOperand, rightOperand);
        }

        private OrOperation OrOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(Expression);
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(Expression);

            return new OrOperation(@operator, leftOperand, rightOperand);
        }

        private Assignment Assignment(ParseNode node, bool isStmtExpression = false)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(Expression);
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(Expression);

            return new Assignment(@operator, leftOperand, rightOperand, isStmtExpression);
        }

        private Expression RelationalOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(Expression);

            if (children.Check(TokenTag.IS)) return IsOperation(leftOperand, children);
            if (children.Check(TokenTag.AS)) return AsOperation(leftOperand, children);

            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(Expression);

            return new RelationOperation(@operator, leftOperand, rightOperand);
        }

        private IsOperation IsOperation(Expression operand, ParseNodeCollection children)
        {
            return children
                .Skip(TokenTag.IS)
                .GetAndMove(Type)
                .Pipe(type => new IsOperation(operand, type));
        }

        private AsOperation AsOperation(Expression operand, ParseNodeCollection children)
        {
            return children
                .Skip(TokenTag.AS)
                .GetAndMove(Type)
                .Pipe(type => new AsOperation(operand, type));
        }

        private TernaryOperation TernaryOperation(ParseNode node)
        {
            var children = node.Children;
            var condition = children.GetAndMove(Expression);

            var trueBranch = children
                .Skip(TokenTag.QUESTION)
                .GetAndMove(Expression);

            var falseBranch = children
                .Skip(TokenTag.COLON)
                .GetAndMove(Expression);

            return new TernaryOperation(condition, trueBranch, falseBranch);
        }

        private ArithmeticOperation ArithmeticOperation(ParseNode node)
        {
            var children = node.Children;
            var leftOperand = children.GetAndMove(Expression);
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var rightOperand = children.GetAndMove(Expression);

            return new ArithmeticOperation(@operator, leftOperand, rightOperand);
        }

        private UnaryOperation UnaryOperation(ParseNode node)
        {
            var children = node.Children;
            var @operator = children.GetAndMove(ParseNodeTag.Terminal).Token;
            var operand = children.GetAndMove(Expression);

            return new UnaryOperation(@operator, operand);
        }

        private CastExpression CastExpression(ParseNode node)
        {
            var type = node.Children
                .Skip(TokenTag.OPEN_PAREN)
                .GetAndMove(Type);

            return node.Children
                .Skip(TokenTag.CLOSE_PAREN)
                .GetAndMove(Expression)
                .Pipe(operand => new CastExpression(type, operand));
        }

        private Literal Literal(ParseNode node)
        {
            return node.Children
                .GetAndMove(ParseNodeTag.Terminal)
                .Pipe(terminal => new Literal(terminal.Token));
        }
        #endregion

        private TypeNode Type(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.PrimitiveType: return PrimitiveType(node);
                case ParseNodeTag.ArrayType: return ArrayType(node);
            }

            throw new SyntaxException("Unsupported type: {0}", node);
        }

        private TypeNode NotArrayType(ParseNode node)
        {
            switch (node.Tag)
            {
                case ParseNodeTag.PrimitiveType: return PrimitiveType(node);
            }

            throw new SyntaxException("Unsupported type: {0}", node);
        }
        
        private ArrayTypeNode ArrayType(ParseNode node)
        {
            var ofType = node.Children.GetAndMove(NotArrayType);
            var rank = node.Children.GetAndMove(ArrayRank);

            return new ArrayTypeNode(ofType, rank);
        }

        private int ArrayRank(ParseNode node)
        {
            return node.Children.Count();
        }

        private PrimitiveTypeNode PrimitiveType(ParseNode node)
        {
            return node.Children
                .GetAndMove(ParseNodeTag.Terminal)
                .Pipe(terminal => new PrimitiveTypeNode(terminal.Token));
        }

        private bool SeekVariable(string varName, out VarDeclaration declaration)
        {
            if (_scopes.Peek().TryResolve(varName, out declaration))
                return true; // hot path

            var copy = new Queue<VarScope>(_scopes);
            copy.Dequeue();

            while (copy.Any())
            {
                if (copy.Dequeue().TryResolve(varName, out declaration))
                    return true;
            }

            return false;
        }
        
        private VarScope GetCurrentScope()
        {
            return _scopes.Peek();
        }
    }
}
