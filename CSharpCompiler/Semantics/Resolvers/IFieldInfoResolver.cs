using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IFieldInfoResolver
    {
        string GetName();
        ITypeInfo GetFieldType();
        ITypeInfo GetDeclaringType();
    }
}
