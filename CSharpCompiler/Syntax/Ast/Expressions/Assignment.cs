using System;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Assignment : BinaryOperation
    {
        public Assignment(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Build(MethodBuilder builder)
        {
            if (LeftOperand is VarAccess)
            {
                var varDef = builder.GetVarDefinition((VarAccess)LeftOperand);
                RightOperand.Build(builder);
                builder.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new NotSupportedException("Assignment is supported only for variables");
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
