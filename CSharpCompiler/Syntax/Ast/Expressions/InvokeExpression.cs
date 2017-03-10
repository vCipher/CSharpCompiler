using CSharpCompiler.Lexica.Tokens;
using System.Collections.Generic;

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
    }
}
