using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class AsOperation : Expression
    {
        public Expression Operand { get; private set; }
        public TypeNode Type { get; private set; }

        public AsOperation(Expression operand, TypeNode type)
        {
            Operand = operand;
            Type = type;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitAsOperation(this);
        }
    }
}
