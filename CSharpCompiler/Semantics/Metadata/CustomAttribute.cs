using System;
using System.Reflection;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class CustomAttribute : IMetadataEntity
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public MetadataToken Token { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        public MethodReference Constructor { get; private set; }
        public ICustomAttributeProvider Owner { get; private set; }
        
        public CustomAttribute(Type type, ConstructorInfo ctorInfo, ICustomAttributeProvider owner)
        {
            Name = type.Name;
            Namespace = type.Namespace;
            Token = new MetadataToken(MetadataTokenType.CustomAttribute, 0);
            Assembly = new AssemblyReference(type.GetTypeInfo().Assembly.GetName());
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

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
