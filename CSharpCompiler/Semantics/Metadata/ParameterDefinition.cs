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

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= TypeInfoComparer.Default.GetHashCode(Type);
                res ^= MethodInfoComparer.Default.GetHashCode(Method);
                res ^= Attributes.GetHashCode();
                return res;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MethodReference)) return false;
            return Equals((MethodReference)obj);
        }

        public bool Equals(ParameterDefinition other)
        {
            return TypeInfoComparer.Default.Equals(Type, other.Type)
                && MethodInfoComparer.Default.Equals(Method, other.Method)
                && Attributes == other.Attributes;
        }
    }
}
