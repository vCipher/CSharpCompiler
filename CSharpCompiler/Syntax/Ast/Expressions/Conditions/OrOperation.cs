using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions.Conditions
{
    public sealed class OrOperation : BinaryOperation
    {
        public OrOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitOrOperation(this);
        }
    }
}
