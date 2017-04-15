using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class ElementStore : Expression
    {
        public Expression Array { get; private set; }
        public Expression Index { get; private set; }
        public Expression Value { get; private set; }
        public bool IsStmtExpression { get; private set; }

        public ElementStore(Expression array, Expression index, Expression value, bool isStmtExpression)
        {
            Array = array;
            Index = index;
            Value = value;
            IsStmtExpression = isStmtExpression;
        }

        public override ITypeInfo InferType()
        {
            var arrayType = Array.InferType() as ArrayType;
            var valueType = Value.InferType();

            if (arrayType == null) throw new TypeInferenceException("Array access expression must have an array type");
            if (Index.InferType() != KnownType.Int32) throw new TypeInferenceException("Array element store index must have only a int32 type");
            if (!arrayType.ContainedType.Equals(valueType)) throw new TypeInferenceException("Array contained type must be assignable from a value type");
            
            return arrayType;
        }

        public override void Build(MethodBuilder builder)
        {
            Array.Build(builder);
            Index.Build(builder);
            Value.Build(builder);

            if (!IsStmtExpression)
                builder.Emit(OpCodes.Dup);

            builder.Emit(OpCodes.Stelem_I4);
        }
    }
}
