using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class InvokeExpression : Expression
    {
        private Lazy<MethodReference> _methodRef;

        public string MethodName { get; private set; }
        public IList<Argument> Arguments { get; private set; }
        public bool IsStmtExpression { get; private set; }

        public InvokeExpression(string methodName, IList<Argument> arguments, bool isStmtExpression)
        {
            _methodRef = new Lazy<MethodReference>(GetMethodReference);
            MethodName = methodName;
            Arguments = arguments;
            IsStmtExpression = isStmtExpression;
        }

        public override ITypeInfo InferType()
        {
            return _methodRef.Value.ReturnType;
        }

        public override void Build(MethodBuilder builder)
        {
            foreach (var arg in Arguments)
            {
                arg.Value.Build(builder);
            }

            builder.Emit(OpCodes.Call, _methodRef.Value);

            if (NeedStackBalancing())
                builder.Emit(OpCodes.Pop);
        }

        private bool NeedStackBalancing()
        {
            return IsStmtExpression && !InferType().Equals(KnownType.Void);
        }

        private MethodReference GetMethodReference()
        {
            if (MethodName != "writeLine") throw new NotImplementedException();
            if (Arguments.Count != 1) throw new NotImplementedException();

            Expression arg = Arguments.First().Value;
            switch (arg.InferType().ElementType)
            {
                case ElementType.Int32: return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }));
                case ElementType.String: return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            }

            throw new NotImplementedException();
        }
    }
}
