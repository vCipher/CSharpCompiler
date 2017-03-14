using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ModuleDefinition : IMetadataEntity
    {
        /// <summary>
        /// A Guid used to distinguish between two versions of the same module
        /// </summary>
        public Guid Mvid { get; private set; }
        public string Name { get; private set; }
        public MetadataToken Token { get; private set; }
        public Collection<TypeDefinition> Types { get; set; }
        
        public ModuleDefinition(Guid guid)
        {
            Mvid = guid;
            Name = "Target.exe";
            Token = new MetadataToken(MetadataTokenType.Module, 0);
            Types = new Collection<TypeDefinition>();
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(MetadataTokenType.Module, rid);
        }
    }
}
