using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface ICustomAttributeResolver
    {
        string GetName();
        string GetNamespace();
        IAssemblyInfo GetAssembly();
        IMethodInfo GetConstructor();
        ICustomAttributeProvider GetOwner();
    }
}
