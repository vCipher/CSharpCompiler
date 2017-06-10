using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public static class TypeFactory
    {
        private static Func<TypeDefinition, TypeReference> _getTypeReference = 
            Func.Memoize<TypeDefinition, TypeReference>(TypeReferenceResolver.Resolve);

        public static TypeReference GetTypeReference(ITypeInfo type)
        {
            if (type is TypeReference) return (TypeReference)type;
            return _getTypeReference((TypeDefinition)type);
        }

        private sealed class TypeReferenceResolver : ITypeReferenceResolver
        {
            private TypeDefinition _typeDef;

            private TypeReferenceResolver(TypeDefinition typeDef)
            {
                _typeDef = typeDef;
            }

            public static TypeReference Resolve(TypeDefinition typeDef)
            {
                var resolver = new TypeReferenceResolver(typeDef);
                return new TypeReference(resolver);
            }

            public IAssemblyInfo GetAssembly()
            {
                return AssemblyFactory.GetAssemblyReference(_typeDef.Assembly);
            }

            public ElementType GetElementType(ITypeInfo type)
            {
                return _typeDef.ElementType;
            }

            public string GetName()
            {
                return _typeDef.Name;
            }

            public string GetNamespace()
            {
                return _typeDef.Namespace;
            }
        }
    }
}
