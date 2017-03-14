using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (Scope == null)
                throw new UndefinedVariableException(VarName);

            return Scope.Resolve(VarName);
        }

        public override IType InferType()
        {
            return TypeInference.InferType(this);
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Build(this);
        }
    }
}
