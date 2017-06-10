using CSharpCompiler.Compilation;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Builders
{
    public sealed class AssemblyBuilder : IAssemblyDefinitionResolver
    {
        private CompilationContext _context;

        private AssemblyBuilder(CompilationContext context)
        {
            _context = context;
        }

        public static AssemblyDefinition Build(CompilationContext context)
        {
            var builder = new AssemblyBuilder(context);
            return new AssemblyDefinition(builder);
        }

        public AssemblyAttributes GetAttributes()
        {
            return AssemblyAttributes.None;
        }

        public string GetCulture()
        {
            return string.Empty;
        }

        public Collection<CustomAttribute> GetCustomAttributes()
        {
            return new Collection<CustomAttribute>();
        }

        public MethodDefinition GetEntryPoint()
        {
            return _context.Module.EntryPoint;
        }

        public byte[] GetHash()
        {
            return Empty<byte>.Array;
        }

        public AssemblyHashAlgorithm GetHashAlgorithm()
        {
            return AssemblyHashAlgorithm.SHA1;
        }

        public ModuleDefinition GetModule()
        {
            return _context.Module;
        }

        public string GetName()
        {
            return _context.Options.AssemblyName;
        }

        public byte[] GetPublicKey()
        {
            return Empty<byte>.Array;
        }

        public byte[] GetPublicKeyToken()
        {
            return Empty<byte>.Array;
        }

        public Collection<AssemblyReference> GetReferences()
        {
            return new Collection<AssemblyReference>
            {
                KnownAssembly.System_Console
            };
        }

        public Version GetVersion()
        {
            return new Version(1, 0, 0, 0);
        }
    }
}
