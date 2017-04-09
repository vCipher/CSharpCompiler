using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using System;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class RelationOperation : BinaryOperation
    {
        public RelationOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Build(MethodBuilder builder)
        {
            LeftOperand.Build(builder);
            RightOperand.Build(builder);

            switch (Operator.Tag)
            {
                case TokenTag.LESS: builder.Emit(OpCodes.Clt); return;
                case TokenTag.GREATER: builder.Emit(OpCodes.Cgt); return;
            }

            throw new NotSupportedException(string.Format("Not supported operator: {0}", Operator));
        }

        public override IType InferType()
        {
            var leftType = LeftOperand.InferType();
            var rightType = RightOperand.InferType();

            if (KnownType.Boolean.Equals(leftType) && KnownType.Boolean.Equals(rightType))
                return KnownType.Boolean;

            throw new TypeInferenceException("Can't inference type for: {0} and for: {1}", leftType, rightType);
        }
    }
}
