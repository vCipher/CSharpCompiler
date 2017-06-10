using System;
using System.Collections.ObjectModel;
using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class ModuleDefinitionResolver : IModuleDefinitionResolver
    {
        private MetadataToken _token;
        private ModuleRow _row;
        private MetadataSystem _metadata;

        private ModuleDefinitionResolver(uint rid, ModuleRow row, MetadataSystem metadata)
        {
            _token = new MetadataToken(MetadataTokenType.Module, rid);
            _row = row;
            _metadata = metadata;
        }

        public static ModuleDefinition Resolve(ModuleRow row, MetadataSystem metadata)
        {
            var resolver = new ModuleDefinitionResolver(1, row, metadata);
            return new ModuleDefinition(resolver);
        }

        public static ModuleDefinition Resolve(uint rid, ModuleRow row, MetadataSystem metadata)
        {
            var resolver = new ModuleDefinitionResolver(rid, row, metadata);
            return new ModuleDefinition(resolver);
        }

        public AssemblyDefinition GetAssembly()
        {
            return _metadata.Assembly;
        }

        public MethodDefinition GetEntryPoint()
        {
            return _metadata.EntryPoint;
        }

        public Guid GetMvid()
        {
            return _metadata.ResolveGuid(_row.Mvid);
        }

        public string GetName()
        {
            return _metadata.ResolveString(_row.Name);
        }

        public Collection<TypeDefinition> GetTypes()
        {
            return new Collection<TypeDefinition>(_metadata.Types);
        }
    }
}
