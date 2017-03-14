using System;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public abstract class Expression : AstNode
    {
        public abstract IType InferType();
        public abstract void Build(MethodBuilder builder);
    }
}
