using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ParameterDefinition : IMetadataEntity, IEquatable<ParameterDefinition>
    {
        private Lazy<ITypeInfo> _type;
        private Lazy<IMethodInfo> _method;

        public int Index { get; private set; }
        public string Name { get; private set; }
        public ParameterAttributes Attributes { get; private set; }

        public ITypeInfo Type => _type.Value;
        public IMethodInfo Method => _method.Value;

        public ParameterDefinition(IParameterDefinitionResolver resolver)
        {
            Index = resolver.GetIndex();
            Name = resolver.GetName();
            Attributes = resolver.GetAttributes();
            _type = new Lazy<ITypeInfo>(resolver.GetParameterType);
            _method = new Lazy<IMethodInfo>(resolver.GetMethod);
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
