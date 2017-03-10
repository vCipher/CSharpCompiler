using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class UnaryOperation : Expression
    {
        public Token Operator { get; private set; }

        public Expression Operand { get; private set; }

        public UnaryOperation(Token @operator, Expression operand)
        {
            Operator = @operator;
            Operand = operand;
        }
    }
}
