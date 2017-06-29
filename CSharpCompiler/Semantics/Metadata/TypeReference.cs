using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeReference : ITypeInfo
    {
        private object _syncLock;
        private ITypeReferenceResolver _resolver;

        private LazyWrapper<string> _name;
        private LazyWrapper<string> _namespace;
        private LazyWrapper<ElementType> _elementType;
        private LazyWrapper<IAssemblyInfo> _assembly;

        public string Name => _name.GetValue(ref _syncLock, _resolver.GetName);
        public string Namespace => _namespace.GetValue(ref _syncLock, _resolver.GetNamespace);
        public IAssemblyInfo Assembly => _assembly.GetValue(ref _syncLock, _resolver.GetAssembly);
        public ElementType ElementType => _elementType.GetValue(ref _syncLock, () => _resolver.GetElementType(this));

        public TypeReference(ITypeReferenceResolver resolver)
        {
            _syncLock = new object();
            _resolver = resolver;
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
