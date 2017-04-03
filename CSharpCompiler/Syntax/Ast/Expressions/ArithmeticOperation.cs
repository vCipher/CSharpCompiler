using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ArithmeticOperation : BinaryOperation
    {
        public ArithmeticOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitArithmeticOperation(this);
        }
    }
}
