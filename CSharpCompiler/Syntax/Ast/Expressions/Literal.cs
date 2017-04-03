using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Literal : Expression
    {
        public Token Value { get; private set; }

        public Literal(Token value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitLiteral(this);
        }
    }
}