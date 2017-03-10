using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class CastExpression : Expression
    {
        public Expression Operand { get; private set; }

        public AstType Type { get; private set; }

        public CastExpression(AstType type, Expression operand)
        {
            Operand = operand;
            Type = type;
        }
    }
}