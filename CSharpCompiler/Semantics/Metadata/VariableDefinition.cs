using System;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class VariableDefinition : IEquatable<VariableDefinition>
    {
        public string Name { get; private set; }
        public ITypeInfo Type { get; private set; }
        public MethodBody MethodBody { get; private set; }

        public int Index
        {
            get { return MethodBody.Variables.IndexOf(this); }
        }

        public VariableDefinition(string name, ITypeInfo type, MethodBody methodBody)
        {
            Name = name;
            Type = type;
            MethodBody = methodBody;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(Name);
                hash = hash * 23 + TypeInfoComparer.Default.GetHashCode(Type);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is VariableDefinition) && Equals((VariableDefinition)obj);
        }

        public bool Equals(VariableDefinition other)
        {
            return string.Equals(Name, other.Name) 
                && Type.Equals(other.Type);
        }
    }
}
