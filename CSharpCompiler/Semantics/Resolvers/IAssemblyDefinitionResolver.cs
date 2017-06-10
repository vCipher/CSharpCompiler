using CSharpCompiler.Semantics.Metadata;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IAssemblyDefinitionResolver : IAssemblyInfoResolver
    {
        MethodDefinition GetEntryPoint();
        ModuleDefinition GetModule();
        Collection<CustomAttribute> GetCustomAttributes();
        Collection<AssemblyReference> GetReferences();
    }
}