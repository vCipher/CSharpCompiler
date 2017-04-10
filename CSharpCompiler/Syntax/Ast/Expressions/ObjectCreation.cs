using System;
using System.Collections.Generic;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ObjectCreation : Expression
    {
        private AstType _type;
        private IList<Argument> _arguments;
        private bool _isStmtExpression;

        public ObjectCreation(AstType type, IList<Argument> arguments, bool isStmtExpression)
        {
            _type = type;
            _arguments = arguments;
            _isStmtExpression = isStmtExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override IType InferType()
        {
            return _type.ToType();
        }
    }
}
