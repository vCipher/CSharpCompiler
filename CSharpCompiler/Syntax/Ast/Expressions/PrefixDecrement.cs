using System;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class PrefixDecrement : Expression
    {
        public Expression Operand { get; private set; }
        public bool IsStmtExpression { get; private set; }
        
        public PrefixDecrement(Expression operand, bool isStmtExpression)
        {
            Operand = operand;
            IsStmtExpression = isStmtExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            if (Operand is VarAccess)
            {
                var varAccess = (VarAccess)Operand;
                var declaration = varAccess.Resolve();
                var varDef = builder.GetVarDefinition(declaration);

                builder.Emit(OpCodes.Ldloc, varDef);
                builder.Emit(OpCodes.Ldc_I4_1);
                builder.Emit(OpCodes.Sub);
                if (!IsStmtExpression) builder.Emit(OpCodes.Dup);
                builder.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new NotSupportedException("Prefix decrement is supported only for variables");
        }

        public override IType InferType()
        {
            return Operand.InferType();
        }
    }
}
