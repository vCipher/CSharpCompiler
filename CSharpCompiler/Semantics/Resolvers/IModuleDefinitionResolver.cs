using CSharpCompiler.Semantics.Metadata;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IModuleDefinitionResolver
    {
        string GetName();
        Guid GetMvid();
        Collection<TypeDefinition> GetTypes();
        MethodDefinition GetEntryPoint();
        AssemblyDefinition GetAssembly();
    }
}