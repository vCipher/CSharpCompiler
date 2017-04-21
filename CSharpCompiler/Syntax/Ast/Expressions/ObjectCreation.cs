using System;
using System.Collections.Generic;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ObjectCreation : Expression
    {
        public TypeNode Type { get; private set; }
        public IList<Argument> Arguments { get; private set; }
        public bool IsStatementExpression { get; private set; }

        public ObjectCreation(TypeNode type, IList<Argument> arguments, bool isStatementExpression)
        {
            Type = type;
            Arguments = arguments;
            IsStatementExpression = isStatementExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override ITypeInfo InferType()
        {
            return Type.ToType();
        }
    }
}
