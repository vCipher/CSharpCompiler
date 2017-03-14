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
            Assembly = new AssemblyReference(type.Assembly.GetName());
            Constructor = new MethodReference(ctorInfo);
            Owner = owner;
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
