using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions.Relations
{
    public sealed class NotEqualOperation : BinaryOperation
    {
        public NotEqualOperation(Token @operator, Expression leftOperand, Expression rightOperand)
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitNotEqualOperation(this);
        }
    }
}
