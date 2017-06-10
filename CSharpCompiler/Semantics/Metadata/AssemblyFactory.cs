using CSharpCompiler.PE;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.IO;
using System.Reflection;

namespace CSharpCompiler.Semantics.Metadata
{
    public static class AssemblyFactory
    {
        private static Func<AssemblyReference, AssemblyDefinition> _getAssemblyDefinition = 
            Func.Memoize<AssemblyReference, AssemblyDefinition>(CreateAssemblyDefinition);

        private static Func<AssemblyDefinition, AssemblyReference> _getAssemblyReference =
            Func.Memoize<AssemblyDefinition, AssemblyReference>(AssemblyReferenceResolver.Resolve);

        public static AssemblyDefinition GetAssemblyDefinition(IAssemblyInfo assembly)
        {
            if (assembly is AssemblyDefinition) return (AssemblyDefinition)assembly;
            return _getAssemblyDefinition((AssemblyReference)assembly);
        }

        public static AssemblyReference GetAssemblyReference(IAssemblyInfo assembly)
        {
            if (assembly is AssemblyReference) return (AssemblyReference)assembly;
            return _getAssemblyReference((AssemblyDefinition)assembly);
        }

        private static AssemblyDefinition CreateAssemblyDefinition(AssemblyReference assemblyRef)
        {
            var assemblyName = new AssemblyName(assemblyRef.ToString());
            var assembly = Assembly.Load(assemblyName);

            using (var stream = File.OpenRead(assembly.Location))
            using (var reader = new AssemblyReader(stream))
            {
                return reader.ReadAssembly();
            }
        }

        private sealed class AssemblyReferenceResolver : IAssemblyReferenceResolver
        {
            private AssemblyDefinition _assemblyDef;

            private AssemblyReferenceResolver(AssemblyDefinition assemblyDef)
            {
                _assemblyDef = assemblyDef;
            }

            public static AssemblyReference Resolve(AssemblyDefinition assemlbyDef)
            {
                var resolver = new AssemblyReferenceResolver(assemlbyDef);
                return new AssemblyReference(resolver);
            }

            public AssemblyAttributes GetAttributes()
            {
                return (_assemblyDef.Attributes & ~AssemblyAttributes.PublicKey);
            }

            public string GetCulture()
            {
                return _assemblyDef.Culture;
            }

            public byte[] GetHash()
            {
                return Empty<byte>.Array;
            }

            public AssemblyHashAlgorithm GetHashAlgorithm()
            {
                return _assemblyDef.HashAlgorithm;
            }

            public string GetName()
            {
                return _assemblyDef.Name;
            }

            public byte[] GetPublicKey()
            {
                return Empty<byte>.Array;
            }

            public byte[] GetPublicKeyToken()
            {
                return _assemblyDef.PublicKeyToken;
            }

            public Version GetVersion()
            {
                return _assemblyDef.Version;
            }
        }
    }
}
