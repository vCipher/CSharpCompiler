using CSharpCompiler.Semantics.TypeSystem;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodDefinition : IMethodInfo
    {
        public string Name { get; private set; }
        public ITypeInfo ReturnType { get; private set; }
        public ITypeInfo DeclaringType { get; private set; }
        public CallingConventions CallingConventions { get; private set; }
        public Collection<ParameterDefinition> Parameters { get; private set; }
        public MethodBody Body { get; private set; }
        public MethodImplAttributes ImplAttributes { get; private set; }
        public MethodAttributes Attributes { get; private set; }

        public bool HasThis
        {
            get { return !Attributes.HasFlag(MethodAttributes.Static); }
        }

        public MethodDefinition(string name, MethodAttributes attributes, TypeDefinition typeDef)
        {
            Name = name;
            ReturnType = KnownType.Void;
            DeclaringType = typeDef;
            Body = new MethodBody();
            ImplAttributes = MethodImplAttributes.IL | MethodImplAttributes.Managed;
            Attributes = attributes;
            Parameters = new Collection<ParameterDefinition>();
            CallingConventions = GetCallingConvention();
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitMethodDefinition(this);
        }

        public override int GetHashCode()
        {
            return MethodInfoComparer.Default.GetHashCode(this);
        }

        public override bool  Equals(object obj)
        {
            return (obj is MethodDefinition) && Equals((MethodDefinition)obj);
        }

        public bool Equals(IMethodInfo other)
        {
            return (other is MethodDefinition) && Equals((MethodDefinition)other);
        }

        public bool Equals(MethodDefinition other)
        {
            return MethodInfoComparer.Default.Equals(this, other);
        }

        private CallingConventions GetCallingConvention()
        {
            var convention = CallingConventions.Default;
            if (HasThis) convention |= CallingConventions.HasThis;
            return convention;
        }
    }
}
