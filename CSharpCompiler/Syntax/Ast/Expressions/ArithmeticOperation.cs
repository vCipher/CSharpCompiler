using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using System;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ArithmeticOperation : BinaryOperation
    {
        public ArithmeticOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Build(MethodBuilder builder)
        {
            LeftOperand.Build(builder);
            RightOperand.Build(builder);

            switch (Operator.Tag)
            {
                case TokenTag.PLUS: builder.Emit(OpCodes.Add); return;
                case TokenTag.MINUS: builder.Emit(OpCodes.Sub); return;
                case TokenTag.MULTIPLY: builder.Emit(OpCodes.Mul); return;
                case TokenTag.DIVIDE: builder.Emit(OpCodes.Div); return;
                case TokenTag.MOD: builder.Emit(OpCodes.Rem); return;
                case TokenTag.LEFT_SHIFT: builder.Emit(OpCodes.Shl); return;
                case TokenTag.RIGHT_SHIFT: builder.Emit(OpCodes.Shr); return;
                case TokenTag.BIT_OR: builder.Emit(OpCodes.Or); return;
                case TokenTag.BIT_AND: builder.Emit(OpCodes.And); return;
                case TokenTag.BIT_XOR: builder.Emit(OpCodes.Xor); return;
            }

            throw new NotSupportedException(string.Format("Not supported operator: {0}", Operator));
        }

        public override IType InferType()
        {
            var leftType = LeftOperand.InferType();
            var rightType = RightOperand.InferType();
            if (leftType.Equals(rightType))
                return leftType;

            throw new TypeInferenceException("Can't inference type for: {0} and for: {1}", leftType, rightType);
        }
    }
}
