using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class IsOperation : Expression
    {
        public Expression Operand { get; private set; }
        public TypeNode Type { get; private set; }

        public IsOperation(Expression operand, TypeNode type)
        {
            Operand = operand;
            Type = type;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitIsOperation(this);
        }
    }
}