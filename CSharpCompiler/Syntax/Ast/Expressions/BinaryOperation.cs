using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class BinaryOperation : Expression
    {
        public Token Operator { get; private set; }

        public Expression LeftOperand { get; private set; }
        
        public Expression RightOperand { get; private set; }

        public BinaryOperation(Token @operator, Expression leftOperand, Expression rightOperand)
        {
            Operator = @operator;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }
    }
}
