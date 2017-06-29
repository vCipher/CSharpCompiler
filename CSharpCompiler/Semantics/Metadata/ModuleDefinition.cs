using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ModuleDefinition : IMetadataEntity
    {
        private object _syncLock;
        private IModuleDefinitionResolver _resolver;

        private LazyWrapper<Guid> _mvid;
        private LazyWrapper<string> _name;
        private LazyWrapper<AssemblyDefinition> _assembly;
        private LazyWrapper<MethodDefinition> _entryPoint;
        private LazyWrapper<Collection<TypeDefinition>> _types;

        private RuntimeBindingCollection<TypeDefinition> _typeMapping;

        public Guid Mvid => _mvid.GetValue(ref _syncLock, _resolver.GetMvid);
        public string Name => _name.GetValue(ref _syncLock, _resolver.GetName);
        public AssemblyDefinition Assembly => _assembly.GetValue(ref _syncLock, _resolver.GetAssembly);
        public MethodDefinition EntryPoint => _entryPoint.GetValue(ref _syncLock, _resolver.GetEntryPoint);
        public Collection<TypeDefinition> Types => _types.GetValue(ref _syncLock, _resolver.GetTypes);

        public ModuleDefinition(IModuleDefinitionResolver resolver)
        {
            _syncLock = new object();
            _resolver = resolver;
            _typeMapping = new RuntimeBindingCollection<TypeDefinition>(
                () => Types, RuntimeBindingSignature.GetTypeSignature);
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
