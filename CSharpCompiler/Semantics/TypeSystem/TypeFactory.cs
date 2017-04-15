using CSharpCompiler.Semantics.Metadata;
using System.Reflection;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class TypeFactory
    {
        public static ITypeInfo Create(System.Type type)
        {
            KnownTypeCode knownTypeCode = type.GetKnownTypeCode();
            if (knownTypeCode != KnownTypeCode.None) return KnownType.Get(knownTypeCode);
            if (type.IsArray) return new ArrayType(Create(type.GetElementType()), type.GetArrayRank());

            return new TypeReference(
                type.Name,
                type.Namespace,
                ElementType.Class,
                AssemblyFactory.Create(type.GetTypeInfo().Assembly.GetName()));
        }
    }
}
