using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ParameterDefinition : IMetadataEntity, IEquatable<ParameterDefinition>
    {
        private object _syncLock;
        private IParameterDefinitionResolver _resolver;

        private LazyWrapper<int> _index;
        private LazyWrapper<string> _name;
        private LazyWrapper<ParameterAttributes> _attributes;
        private LazyWrapper<ITypeInfo> _type;
        private LazyWrapper<IMethodInfo> _method;

        public int Index => _index.GetValue(ref _syncLock, _resolver.GetIndex);
        public string Name => _name.GetValue(ref _syncLock, _resolver.GetName);
        public ParameterAttributes Attributes => _attributes.GetValue(ref _syncLock, _resolver.GetAttributes);
        public ITypeInfo Type => _type.GetValue(ref _syncLock, _resolver.GetParameterType);
        public IMethodInfo Method => _method.GetValue(ref _syncLock, _resolver.GetMethod);

        public ParameterDefinition(IParameterDefinitionResolver resolver)
        {
            _syncLock = new object();
            _resolver = resolver;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitParameterDefinition(this);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + TypeInfoComparer.Default.GetHashCode(Type);
                hash = hash * 23 + MethodInfoComparer.Default.GetHashCode(Method);
                hash = hash * 23 + Attributes.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is MethodReference) && Equals((MethodReference)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is MethodReference) && Equals((MethodReference)other);
        }

        public bool Equals(ParameterDefinition other)
        {
            return TypeInfoComparer.Default.Equals(Type, other.Type)
                && MethodInfoComparer.Default.Equals(Method, other.Method)
                && Attributes == other.Attributes;
        }
    }
}
