using CSharpCompiler.Semantics.Resolvers;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeDefinition : ITypeInfo, ICustomAttributeProvider
    {
        private Lazy<string> _name;
        private Lazy<ElementType> _elementType;
        private Lazy<IAssemblyInfo> _assembly;
        private Lazy<ITypeInfo> _baseType;
        private Lazy<Collection<MethodDefinition>> _methods;
        private Lazy<Collection<FieldDefinition>> _fields;
        private Lazy<Collection<CustomAttribute>> _attributes;

        private RuntimeBindingCollection<MethodDefinition> _methodMapping;
        private RuntimeBindingCollection<FieldDefinition> _fieldMapping;

        public string Namespace { get; private set; }
        public TypeAttributes Attributes { get; private set; }

        public string Name => _name.Value;
        public ElementType ElementType => _elementType.Value;
        public IAssemblyInfo Assembly => _assembly.Value;
        public ITypeInfo BaseType => _baseType.Value;
        public Collection<MethodDefinition> Methods => _methods.Value;
        public Collection<FieldDefinition> Fields => _fields.Value;
        public Collection<CustomAttribute> CustomAttributes => _attributes.Value;

        public bool IsModuleType => Name == "<Module>";

        public TypeDefinition(ITypeDefinitionResolver resolver)
        {
            Namespace = resolver.GetNamespace();
            Attributes = resolver.GetAttributes();

            _name = new Lazy<string>(resolver.GetName);
            _elementType = new Lazy<ElementType>(() => resolver.GetElementType(this));
            _assembly = new Lazy<IAssemblyInfo>(resolver.GetAssembly);
            _baseType = new Lazy<ITypeInfo>(resolver.GetBaseType);
            _methods = new Lazy<Collection<MethodDefinition>>(() => resolver.GetMethods(this));
            _fields = new Lazy<Collection<FieldDefinition>>(() => resolver.GetFields(this));
            _attributes = new Lazy<Collection<CustomAttribute>>(() => resolver.GetCustomAttributes(this));

            _methodMapping = new RuntimeBindingCollection<MethodDefinition>(
                () => _methods.Value,
                RuntimeBindingSignature.GetMethodSignature);

            _fieldMapping = new RuntimeBindingCollection<FieldDefinition>(
                () => _fields.Value,
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
