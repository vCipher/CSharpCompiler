using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class CustomAttribute : IMetadataEntity
    {
        private Lazy<IAssemblyInfo> _assembly;
        private Lazy<IMethodInfo> _constructor;
        private Lazy<ICustomAttributeProvider> _owner;

        public string Name { get; private set; }
        public string Namespace { get; private set; }

        public IAssemblyInfo Assembly => _assembly.Value;
        public IMethodInfo Constructor => _constructor.Value;
        public ICustomAttributeProvider Owner => _owner.Value;
        
        public CustomAttribute(ICustomAttributeResolver resolver)
        {
            Name = resolver.GetName();
            Namespace = resolver.GetNamespace();
            _assembly = new Lazy<IAssemblyInfo>(resolver.GetAssembly);
            _constructor = new Lazy<IMethodInfo>(resolver.GetConstructor);
            _owner = new Lazy<ICustomAttributeProvider>(resolver.GetOwner);
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitCustomAttribute(this);
        }

        public override int GetHashCode()
        {
            return RuntimeBindingSignature
                .GetAttributeSignature(this)
                .GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is CustomAttribute) && Equals((CustomAttribute)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is CustomAttribute) && Equals((CustomAttribute)other);
        }

        public bool Equals(CustomAttribute other)
        {
            return string.Equals(Name, other.Name)
                && string.Equals(Namespace, other.Namespace)
                && Assembly.Equals(other.Assembly)
                && Constructor.Equals(other.Constructor)
                && Owner.Equals(other.Owner);
        }
    }
}
