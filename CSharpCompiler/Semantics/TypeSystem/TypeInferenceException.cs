using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class TypeInferenceException : SemanticException
    {
        public TypeInferenceException(ITypeInfo leftType, ITypeInfo rightType) 
            : base("Can't infer type for: {0} and for: {1}", leftType, rightType)
        { }

        public TypeInferenceException(string format, params object[] args) 
            : base(format, args)
        { }
    }
}
