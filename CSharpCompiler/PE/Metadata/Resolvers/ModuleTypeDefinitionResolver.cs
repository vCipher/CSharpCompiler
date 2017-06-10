using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using System.Collections.ObjectModel;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class ModuleTypeDefinitionResolver : ITypeDefinitionResolver
    {
        public static TypeDefinition Resolve()
        {
            return new TypeDefinition(new ModuleTypeDefinitionResolver());
        }

        public IAssemblyInfo GetAssembly()
        {
            return null;
        }

        public TypeAttributes GetAttributes()
        {
            return TypeAttributes.NotPublic
                | TypeAttributes.AutoLayout
                | TypeAttributes.Class
                | TypeAttributes.AnsiClass;
        }

        public ITypeInfo GetBaseType()
        {
            return null;
        }

        public Collection<CustomAttribute> GetCustomAttributes(TypeDefinition typeDef)
        {
            return new Collection<CustomAttribute>();
        }

        public ElementType GetElementType(ITypeInfo type)
        {
            return ElementType.None;
        }

        public Collection<FieldDefinition> GetFields(TypeDefinition typeDef)
        {
            return new Collection<FieldDefinition>();
        }

        public Collection<MethodDefinition> GetMethods(TypeDefinition typeDef)
        {
            return new Collection<MethodDefinition>();
        }

        public string GetName()
        {
            return "<Module>";
        }

        public string GetNamespace()
        {
            return string.Empty;
        }
    }
}
