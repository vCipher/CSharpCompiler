using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast.Expressions;

namespace CSharpCompiler.Syntax.Ast
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
    }
}