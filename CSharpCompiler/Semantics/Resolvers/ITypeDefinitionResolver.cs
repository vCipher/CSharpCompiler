using CSharpCompiler.Semantics.Metadata;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface ITypeDefinitionResolver : ITypeInfoResolver
    {
        TypeAttributes GetAttributes();
        ITypeInfo GetBaseType();
        Collection<MethodDefinition> GetMethods(TypeDefinition typeDef);
        Collection<FieldDefinition> GetFields(TypeDefinition typeDef);
        Collection<CustomAttribute> GetCustomAttributes(TypeDefinition typeDef);
    }
}