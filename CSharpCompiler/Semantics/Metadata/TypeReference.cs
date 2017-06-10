using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeReference : ITypeInfo
    {
        private Lazy<ElementType> _elementType;
        private Lazy<IAssemblyInfo> _assembly;

        public string Name { get; private set; }
        public string Namespace { get; private set; }
        
        public IAssemblyInfo Assembly => _assembly.Value;
        public ElementType ElementType => _elementType.Value;

        public TypeReference(ITypeReferenceResolver resolver)
        {
            Name = resolver.GetName();
            Namespace = resolver.GetNamespace();
            _assembly = new Lazy<IAssemblyInfo>(resolver.GetAssembly);
            _elementType = new Lazy<ElementType>(() => resolver.GetElementType(this));
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitTypeReference(this);
        }

        public MethodReference GetConstructor(params ITypeInfo[] types)
        {
            return GetMethod(".ctor", CallingConventions.HasThis, types);
        }

        public MethodReference GetMethod(string name, params ITypeInfo[] types)
        {
            return GetMethod(name, CallingConventions.Default, types);
        }

        public MethodReference GetMethod(string name, CallingConventions callConv, params ITypeInfo[] types)
        {
            var assemblyDef = AssemblyFactory.GetAssemblyDefinition(Assembly);
            var typeDef = assemblyDef.Module.GetType(Name, Namespace);
            var methodDef = typeDef.GetMethod(name, callConv, types);
            return MethodFactory.GetMethodReference(methodDef);
        }

        public FieldReference GetField(string name)
        {
            var assemblyDef = AssemblyFactory.GetAssemblyDefinition(Assembly);
            var typeDef = assemblyDef.Module.GetType(Name, Namespace);
            var fieldDef = typeDef.GetField(name);
            return FieldFactory.GetFieldReference(fieldDef);
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
            return (obj is TypeReference) && TypeInfoComparer.Default.Equals(this, (TypeReference)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is TypeReference) && Equals((TypeReference)other);
        }

        public bool Equals(TypeReference other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
