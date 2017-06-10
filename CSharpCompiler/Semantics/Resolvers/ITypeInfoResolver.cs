using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface ITypeInfoResolver
    {
        string GetName();
        string GetNamespace();
        IAssemblyInfo GetAssembly();
        ElementType GetElementType(ITypeInfo type);
    }
}
