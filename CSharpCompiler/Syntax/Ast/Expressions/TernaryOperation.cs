using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using System;

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
            if (!KnownType.Boolean.Equals(Condition.InferType()))
                throw new TypeInferenceException("Condition of a ternary operation must have a boolean type");

            var trueBranchType = TrueBranch.InferType();
            var falseBranchType = FalseBranch.InferType();

            if (trueBranchType.Equals(falseBranchType))
                return trueBranchType;

            throw new TypeInferenceException("Can't infer a type for: {0} and for: {1}", trueBranchType, falseBranchType);
        }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
