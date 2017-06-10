using CSharpCompiler.Semantics.Resolvers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ModuleDefinition : IMetadataEntity
    {
        private Lazy<AssemblyDefinition> _assembly;
        private Lazy<MethodDefinition> _entryPoint;
        private Lazy<Collection<TypeDefinition>> _types;

        private RuntimeBindingCollection<TypeDefinition> _typeMapping;

        public Guid Mvid { get; private set; }
        public string Name { get; private set; }

        public AssemblyDefinition Assembly => _assembly.Value;
        public MethodDefinition EntryPoint => _entryPoint.Value;
        public Collection<TypeDefinition> Types => _types.Value;

        public ModuleDefinition(IModuleDefinitionResolver resolver)
        {
            Mvid = resolver.GetMvid();
            Name = resolver.GetName();
            
            _assembly = new Lazy<AssemblyDefinition>(resolver.GetAssembly);
            _entryPoint = new Lazy<MethodDefinition>(resolver.GetEntryPoint);
            _types = new Lazy<Collection<TypeDefinition>>(resolver.GetTypes);

            _typeMapping = new RuntimeBindingCollection<TypeDefinition>(
                () => _types.Value, RuntimeBindingSignature.GetTypeSignature);
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitModuleDefinition(this);
        }

        public TypeDefinition GetType(string name, string @namespace)
        {
            var signature = RuntimeBindingSignature.GetTypeSignature(name, @namespace, Assembly);
            return _typeMapping.Get(signature);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<Guid>.Default.GetHashCode(Mvid);
        }

        public override bool Equals(object obj)
        {
            return (obj is ModuleDefinition) && Equals((ModuleDefinition)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is ModuleDefinition) && Equals((ModuleDefinition)other);
        }

        public bool Equals(ModuleDefinition other)
        {
            return EqualityComparer<Guid>.Default.Equals(Mvid, other.Mvid);
        }
    }
}
