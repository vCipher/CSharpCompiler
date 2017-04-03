using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Argument : AstNode
    {
        public Token? Modifier { get; private set; }
        public Expression Value { get; private set; }

        public Argument(Expression value) : this(null, value)
        { }

        public Argument(Token? modifier, Expression value)
        {
            Modifier = modifier;
            Value = value;
        }

        public void Accept(IArgumentVisitor visitor)
        {
            visitor.VisitArgument(this);
        }
    }
}