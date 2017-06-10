using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IMethodInfoResolver
    {
        string GetName();
        ITypeInfo GetReturnType();
        ITypeInfo GetDeclaringType();
        CallingConventions GetCallingConventions();
    }
}
