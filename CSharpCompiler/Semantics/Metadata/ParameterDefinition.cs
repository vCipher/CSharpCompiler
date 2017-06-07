using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ParameterDefinition : IMetadataEntity, IEquatable<ParameterDefinition>
    {
        public int Index { get { return Method.Parameters.IndexOf(this); } }
        public string Name { get; private set; }
        public ITypeInfo Type { get; private set; }
        public IMethodInfo Method { get; private set; }
        public ParameterAttributes Attributes { get; private set; }

        public ParameterDefinition(string name, ITypeInfo type, IMethodInfo method)
        {
            Name = name;
            Type = type;
            Method = method;
            Attributes = ParameterAttributes.None;
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

        public bool Equals(ParameterDefinition other)
        {
            return TypeInfoComparer.Default.Equals(Type, other.Type)
                && MethodInfoComparer.Default.Equals(Method, other.Method)
                && Attributes == other.Attributes;
        }
    }
}
