using CSharpCompiler.Semantics.TypeSystem;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeDefinition : ITypeInfo, ICustomAttributeProvider
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public MetadataToken Token { get; private set; }
        public IType DeclaringType { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        public TypeAttributes Attributes { get; private set; }
        public ITypeInfo BaseType { get; private set; }
        public Collection<MethodDefinition> Methods { get; private set; }
        public Collection<FieldDefinition> Fields { get; private set; }
        public Collection<CustomAttribute> CustomAttributes { get; private set; }

        public TypeDefinition(string name, string @namespace, TypeAttributes attributes, IAssemblyInfo assembly)
        {
            Name = name;
            Namespace = @namespace;
            DeclaringType = new UserType();
            Assembly = assembly;
            Attributes = attributes;
            BaseType = new TypeReference(typeof(object));
            Token = new MetadataToken(MetadataTokenType.TypeDef, 0);
            Methods = new Collection<MethodDefinition>();
            Fields = new Collection<FieldDefinition>();
            CustomAttributes = new Collection<CustomAttribute>();
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
