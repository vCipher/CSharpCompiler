using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class InvokeExpression : Expression
    {
        public QualifiedIdentifier MethodName { get; private set; }
        public IList<Argument> Arguments { get; private set; }
        public bool IsStatementExpression { get; private set; }

        public InvokeExpression(QualifiedIdentifier methodName, IList<Argument> arguments, bool isStatementExpression)
        {
            MethodName = methodName;
            Arguments = arguments;
            IsStatementExpression = isStatementExpression;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitInvokeExpression(this);
        }
    }
}
