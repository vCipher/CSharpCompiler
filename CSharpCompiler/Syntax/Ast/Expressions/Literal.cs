using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Literal : Expression
    {
        public Token Value { get; private set; }

        public Literal(Token value)
        {
            Value = value;
        }

        public override IType InferType()
        {
            return TypeInference.InferType(this);
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Build(this);
        }
    }
}