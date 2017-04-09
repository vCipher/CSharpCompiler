using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using System;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class VarAccess : Expression
    {
        public string VarName { get; private set; }
        public VarScope Scope { get; private set; }

        public VarAccess(string varName, VarScope scope)
        {
            VarName = varName;
            Scope = scope;
        }

        public VarDeclaration Resolve()
        {
            if (Scope == null) throw new UndefinedVariableException(VarName);
            return Scope.Resolve(VarName);
        }

        public override IType InferType()
        {
            return Resolve().Type.ToType();
        }

        public override void Build(MethodBuilder builder)
        {
            var varDef = builder.GetVarDefinition(this);
            builder.Emit(OpCodes.Ldloc, varDef);
        }
    }
}
