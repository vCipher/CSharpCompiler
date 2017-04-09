using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class EmptyExpression : Expression
    {
        public override void Build(MethodBuilder builder)
        { }

        public override IType InferType()
        {
            return KnownType.Void;
        }
    }
}
