using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.Reflection;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class AssemblyFactory
    {
        public static AssemblyReference Create(AssemblyName assemblyName)
        {
            return new AssemblyReference(
                assemblyName.Name,
                assemblyName.GetPublicKey(),
                assemblyName.GetPublicKeyToken(),
                assemblyName.Version,
                assemblyName.CultureName,
                Empty<byte>.Array,
                AssemblyAttributes.None);
        }
    }
}
