using CSharpCompiler.Semantics.TypeSystem;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class VariableDefinition : IEquatable<VariableDefinition>
    {
        public string Name { get; private set; }
        public IType Type { get; private set; }
        public MethodBody MethodBody { get; private set; }

        public int Index
        {
            get { return MethodBody.Variables.IndexOf(this); }
        }

        public VariableDefinition(string name, IType type, MethodBody methodBody)
        {
            Name = name;
            Type = type;
            MethodBody = methodBody;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int res = 31;
                res ^= (Name == null) ? 0 : Name.GetHashCode();
                res ^= (Type == null) ? 0 : Type.GetHashCode();
                return res;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is VariableDefinition)
                return Equals((VariableDefinition)obj);

            return false;
        }

        public bool Equals(VariableDefinition other)
        {
            return string.Equals(Name, other.Name, StringComparison.Ordinal) &&
                Type.Equals(other.Type);
        }
    }
}
