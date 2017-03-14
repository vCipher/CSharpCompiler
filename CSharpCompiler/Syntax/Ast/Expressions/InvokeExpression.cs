using CSharpCompiler.Semantics.TypeSystem;
using System.Collections.Generic;
using CSharpCompiler.Semantics.Metadata;
using System;
using CSharpCompiler.Semantic.Cil;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class InvokeExpression : Expression
    {
        public string MethodName { get; private set; }

        public List<Argument> Arguments { get; private set; }

        public InvokeExpression(string methodName, IEnumerable<Argument> arguments)
        {
            MethodName = methodName;
            Arguments = new List<Argument>(arguments);
        }

        public InvokeExpression(string methodName, params Argument[] arguments)
        {
            MethodName = methodName;
            Arguments = new List<Argument>(arguments);
        }

        public override IType InferType()
        {
            return KnownType.Void;
        }

        public override void Build(MethodBuilder builder)
        {
            builder.Build(this);
        }
    }
}
