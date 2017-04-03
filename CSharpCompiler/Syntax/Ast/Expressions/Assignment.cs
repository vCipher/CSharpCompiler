using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Assignment : BinaryOperation
    {
        public bool IsStatementExpression { get; private set; }

        public Assignment(Token @operator, Expression leftOperand, Expression rightOperand, bool isStatementExpression) 
            : base(@operator, leftOperand, rightOperand)
        {
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitAssignment(this);
        }
    }
}
