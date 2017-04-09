using System;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class PostfixIncrement : Expression
    {
        public Expression Operand { get; private set; }

        public PostfixIncrement(Expression operand)
        {
            Operand = operand;
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
                builder.Emit(OpCodes.Add);
                builder.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new NotSupportedException("Posfix increment is supported only for variables");
        }

        public override IType InferType()
        {
            return Operand.InferType();
        }
    }
}
