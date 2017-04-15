using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ElementAccess : Expression
    {
        public Expression Array { get; private set; }
        public Expression Index { get; private set; }

        public ElementAccess(Expression array, Expression index)
        {
            Array = array;
            Index = index;
        }

        public override ITypeInfo InferType()
        {
            var arrayType = Array.InferType() as ArrayType;
            var indexType = Index.InferType();

            if (arrayType == null) throw new TypeInferenceException("Array access expression must have an array type");
            if (indexType != KnownType.Int32) throw new TypeInferenceException("Array element access index must have only a int32 type");

            return indexType;
        }

        public override void Build(MethodBuilder builder)
        {
            Array.Build(builder);
            Index.Build(builder);
            builder.Emit(OpCodes.Ldelem_I4);
        }
    }
}
