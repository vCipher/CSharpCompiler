using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class TernaryOperation : Expression
    {
        public Expression Condition { get; private set; }

        public Expression TrueBranch { get; private set; }

        public Expression FalseBranch { get; private set; }

        public TernaryOperation(Expression condition, Expression trueBranch, Expression falseBranch)
        {
            Condition = condition;
            TrueBranch = trueBranch;
            FalseBranch = falseBranch;
        }

        public override IType InferType()
        {
            var trueType = TrueBranch.InferType();
            var falseType = FalseBranch.InferType();
            return TypeInference.InferType(trueType, falseType);
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Build(this);
        }
    }
}
