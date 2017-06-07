using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ModuleDefinition : IMetadataEntity, IEquatable<ModuleDefinition>
    {
        /// <summary>
        /// A Guid used to distinguish between two versions of the same module
        /// </summary>
        public Guid Mvid { get; private set; }
        public string Name { get; private set; }
        public Collection<TypeDefinition> Types { get; set; }
        
        public ModuleDefinition(Guid guid, string name)
        {
            Mvid = guid;
            Name = name;
            Types = new Collection<TypeDefinition>();
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitModuleDefinition(this);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<Guid>.Default.GetHashCode(Mvid);
        }

        public override bool Equals(object obj)
        {
            return (obj is ModuleDefinition) && Equals((ModuleDefinition)obj);
        }

        public bool Equals(ModuleDefinition other)
        {
            return EqualityComparer<Guid>.Default.Equals(Mvid, other.Mvid);
        }
    }
}
