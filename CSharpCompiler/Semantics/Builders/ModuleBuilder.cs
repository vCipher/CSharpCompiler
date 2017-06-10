using CSharpCompiler.Compilation;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Builders
{
    public sealed class ModuleBuilder : IModuleDefinitionResolver
    {
        private CompilationContext _context;

        private ModuleBuilder(CompilationContext context)
        {
            _context = context;
        }

        public static ModuleDefinition Build(CompilationContext context)
        {
            var builder = new ModuleBuilder(context);
            return new ModuleDefinition(builder);
        }

        public AssemblyDefinition GetAssembly()
        {
            return _context.Assembly;
        }

        public MethodDefinition GetEntryPoint()
        {
            return _context.TypeDefinition.GetMethod("Main");
        }

        public Guid GetMvid()
        {
            return _context.Options.Mvid;
        }

        public string GetName()
        {
            return _context.Options.AssemblyName;
        }

        public Collection<TypeDefinition> GetTypes()
        {
            return new Collection<TypeDefinition> { _context.TypeDefinition };
        }
    }
}
