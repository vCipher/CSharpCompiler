using CSharpCompiler.Semantics.Metadata;
using System.Reflection;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class KnownAssembly
    {
        public static readonly AssemblyReference System = AssemblyFactory.Create(
            typeof(object)
                .GetTypeInfo()
                .Assembly
                .GetName());
    }
}
