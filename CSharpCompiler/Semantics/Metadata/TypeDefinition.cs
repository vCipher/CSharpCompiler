using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeDefinition : ITypeInfo, ICustomAttributeProvider
    {
        private object _syncLock;
        private ITypeDefinitionResolver _resolver;

        private LazyWrapper<string> _name;
        private LazyWrapper<string> _namespace;
        private LazyWrapper<TypeAttributes> _attributes;
        private LazyWrapper<ElementType> _elementType;
        private LazyWrapper<IAssemblyInfo> _assembly;
        private LazyWrapper<ITypeInfo> _baseType;
        private LazyWrapper<Collection<MethodDefinition>> _methods;
        private LazyWrapper<Collection<FieldDefinition>> _fields;
        private LazyWrapper<Collection<CustomAttribute>> _customAttributes;

        private RuntimeBindingCollection<MethodDefinition> _methodMapping;
        private RuntimeBindingCollection<FieldDefinition> _fieldMapping;
        
        public string Name => _name.GetValue(ref _syncLock, _resolver.GetName);
        public string Namespace => _namespace.GetValue(ref _syncLock, _resolver.GetNamespace);
        public TypeAttributes Attributes => _attributes.GetValue(ref _syncLock, _resolver.GetAttributes);
        public ElementType ElementType => _elementType.GetValue(ref _syncLock, () => _resolver.GetElementType(this));
        public IAssemblyInfo Assembly => _assembly.GetValue(ref _syncLock, _resolver.GetAssembly);
        public ITypeInfo BaseType => _baseType.GetValue(ref _syncLock, _resolver.GetBaseType);
        public Collection<MethodDefinition> Methods => _methods.GetValue(ref _syncLock, () => _resolver.GetMethods(this));
        public Collection<FieldDefinition> Fields => _fields.GetValue(ref _syncLock, () => _resolver.GetFields(this));
        public Collection<CustomAttribute> CustomAttributes => _customAttributes.GetValue(ref _syncLock, () => _resolver.GetCustomAttributes(this));

        public bool IsModuleType => Name == "<Module>";

        public TypeDefinition(ITypeDefinitionResolver resolver)
        {
            _syncLock = new object();
            _resolver = resolver;

            _methodMapping = new RuntimeBindingCollection<MethodDefinition>(
                () => Methods,
                RuntimeBindingSignature.GetMethodSignature);

            _fieldMapping = new RuntimeBindingCollection<FieldDefinition>(
                () => Fields,
                RuntimeBindingSignature.GetFieldSignature);
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitTypeDefinition(this);
        }

        public MethodDefinition GetConstructor(params ITypeInfo[] types)
        {
            return GetMethod(".ctor", CallingConventions.HasThis, types);
        }

        public MethodDefinition GetMethod(string name, params ITypeInfo[] types)
        {
            return GetMethod(name, CallingConventions.Default, types);
        }

        public MethodDefinition GetMethod(string name, CallingConventions callConv, params ITypeInfo[] types)
        {
            var signature = RuntimeBindingSignature.GetMethodSignature(name, callConv, types);
            return _methodMapping.Get(signature);
        }

        public FieldDefinition GetField(string name)
        {
            var signature = RuntimeBindingSignature.GetFieldSignature(name);
            return _fieldMapping.Get(signature);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}, {2}", Namespace, Name, Assembly);
        }

        public override int GetHashCode()
        {
            return TypeInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is TypeDefinition) && Equals((TypeDefinition)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is TypeDefinition) && Equals((TypeDefinition)other);
        }

        public bool Equals(TypeDefinition other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
