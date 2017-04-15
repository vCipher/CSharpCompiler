using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ArrayCreation : Expression
    {
        public TypeNode ContainedType { get; private set; }
        public Expression Initializer { get; private set; }
        public bool IsStmtExpression { get; private set; }

        public ArrayCreation(TypeNode containedType, Expression initializer, bool isStmtExpression)
        {
            ContainedType = containedType;
            Initializer = initializer;
            IsStmtExpression = isStmtExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            Initializer.Build(builder);
            builder.Emit(OpCodes.Newarr, ContainedType.ToType());

            if (IsStmtExpression)
                builder.Emit(OpCodes.Pop);
        }

        public override ITypeInfo InferType()
        {
            return ContainedType.ToType();
        }
    }
}
