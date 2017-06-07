using CSharpCompiler.Semantics.TypeSystem;
using System;
using System.Reflection;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class CustomAttribute : IMetadataEntity, IEquatable<CustomAttribute>
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        public IMethodInfo Constructor { get; private set; }
        public ICustomAttributeProvider Owner { get; private set; }
        
        public CustomAttribute(Type type, ConstructorInfo ctorInfo, ICustomAttributeProvider owner)
        {
            Name = type.Name;
            Namespace = type.Namespace;
            Assembly = AssemblyFactory.Create(type.GetTypeInfo().Assembly.GetName());
            Constructor = new MethodReference(ctorInfo);
            Owner = owner;
        }

        public static CustomAttribute Get<TAttribute>(ICustomAttributeProvider owner) where TAttribute : Attribute
        {
            Type type = typeof(TAttribute);
            return new CustomAttribute(type, type.GetConstructor(new Type[0]), owner);
        }

        public static CustomAttribute Get<TAttribute>(ICustomAttributeProvider owner, params Type[] types) where TAttribute : Attribute
        {
            Type type = typeof(TAttribute);
            return new CustomAttribute(type, type.GetConstructor(types), owner);
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitCustomAttribute(this);
        }

        public override int GetHashCode()
        {
            return StandAloneSignature.GetAttributeSignature(this).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is CustomAttribute) && Equals((CustomAttribute)obj);
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
