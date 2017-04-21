using System;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class PostfixIncrement : Expression
    {
        public Expression Operand { get; private set; }
        public bool IsStatementExpression { get; private set; }
        
        public PostfixIncrement(Expression operand, bool isStatementExpression)
        {
            Operand = operand;
            IsStatementExpression = isStatementExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            if (Operand is VarAccess)
            {
                var varAccess = (VarAccess)Operand;
                var declaration = varAccess.Resolve();
                var varDef = builder.GetVarDefinition(declaration);

                builder.Emit(OpCodes.Ldloc, varDef);
                if (!IsStatementExpression) builder.Emit(OpCodes.Dup);
                builder.Emit(OpCodes.Ldc_I4_1);
                builder.Emit(OpCodes.Add);
                builder.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new NotSupportedException("Posfix increment is supported only for variables");
        }

        public override ITypeInfo InferType()
        {
            return Operand.InferType();
        }
    }
}
