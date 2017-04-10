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
        private string _methodName;
        private IList<Argument> _arguments;
        private bool _isStmtExpression;
        
        public InvokeExpression(string methodName, IList<Argument> arguments, bool isStmtExpression)
        {
            _methodRef = new Lazy<MethodReference>(GetMethodReference);
            _methodName = methodName;
            _arguments = arguments;
            _isStmtExpression = isStmtExpression;
        }

        public override IType InferType()
        {
            return _methodRef.Value
                .ReturnType
                .DeclaringType;
        }

        public override void Build(MethodBuilder builder)
        {
            foreach (var arg in _arguments)
            {
                arg.Value.Build(builder);
            }

            builder.Emit(OpCodes.Call, _methodRef.Value);
            if (NeedStackBalancing()) builder.Emit(OpCodes.Pop);
        }

        private bool NeedStackBalancing()
        {
            return _isStmtExpression && !InferType().Equals(KnownType.Void);
        }

        private MethodReference GetMethodReference()
        {
            if (_methodName != "writeLine") throw new NotImplementedException();
            if (_arguments.Count != 1) throw new NotImplementedException();

            Expression arg = _arguments.First().Value;
            switch (arg.InferType().ElementType)
            {
                case ElementType.Int32: return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }));
                case ElementType.String: return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            }

            throw new NotImplementedException();
        }
    }
}
