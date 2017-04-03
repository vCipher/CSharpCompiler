using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class CastExpression : Expression
    {
        public Expression Operand { get; private set; }
        public TypeNode Type { get; private set; }

        public CastExpression(TypeNode type, Expression operand)
        {
            Operand = operand;
            Type = type;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitCastExpression(this);
        }
    }
}