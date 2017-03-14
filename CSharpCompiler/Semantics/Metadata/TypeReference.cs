using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeReference : ITypeInfo
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public MetadataToken Token { get; private set; }
        public IType DeclaringType { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        
        public TypeReference(System.Type type)
        {
            Name = type.Name;
            Namespace = type.Namespace;
            Token = new MetadataToken(MetadataTokenType.TypeRef, 0);
            DeclaringType = TypeFactory.Create(type.GetKnownTypeCode());
            Assembly = new AssemblyReference(type.Assembly.GetName());
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
