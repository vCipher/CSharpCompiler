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
        public string MethodName { get; private set; }
        public IList<Argument> Arguments { get; private set; }
        public bool IsStatementExpression { get; private set; }
        public MethodReference MethodReference { get; private set; }

        public InvokeExpression(string methodName, IList<Argument> arguments, bool isStatementExpression)
        {
            MethodName = methodName;
            Arguments = arguments;
            MethodReference = GetMethodReference(methodName, arguments);
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitInvokeExpression(this);
        }

        private MethodReference GetMethodReference(string methodName, IList<Argument> arguments)
        {
            if (methodName != "writeLine") throw new NotImplementedException();
            if (arguments.Count != 1) throw new NotImplementedException();

            var arg = arguments.First().Value;
            var argType = TypeInference.InferType(arg);

            switch (argType.ElementType)
            {
                case ElementType.Int32: return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }));
                case ElementType.String: return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            }

            throw new NotImplementedException();
        }
    }
}
