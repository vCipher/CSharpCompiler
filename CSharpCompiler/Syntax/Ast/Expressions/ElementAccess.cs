namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ElementAccess : Expression
    {
        public Expression Array { get; private set; }
        public Expression Index { get; private set; }

        public ElementAccess(Expression array, Expression index)
        {
            Array = array;
            Index = index;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitElementAccess(this);
        }
    }
}
