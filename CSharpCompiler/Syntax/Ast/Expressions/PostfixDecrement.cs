using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using System;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class PostfixDecrement : Expression
    {
        public Expression Operand { get; private set; }
        public bool IsStatementExpression { get; private set; }
        
        public PostfixDecrement(Expression operand, bool isStatementExpression)
        {
            Operand = operand;
            IsStatementExpression = isStatementExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            if (Operand is VarAccess)
            {
                var varAccess = (VarAccess)Operand;
                var varDef = builder.GetVarDefinition(varAccess);

                builder.Emit(OpCodes.Ldloc, varDef);
                if (!IsStatementExpression) builder.Emit(OpCodes.Dup);
                builder.Emit(OpCodes.Ldc_I4_1);
                builder.Emit(OpCodes.Sub);
                builder.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new NotSupportedException("Posfix decrement is supported only for variables");
        }

        public override ITypeInfo InferType()
        {
            return Operand.InferType();
        }
    }
}
