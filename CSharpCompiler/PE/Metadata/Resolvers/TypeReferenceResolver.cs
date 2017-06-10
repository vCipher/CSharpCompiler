using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class TypeReferenceResolver : ITypeReferenceResolver
    {
        private TypeRefRow _row;
        private MetadataSystem _metadata;

        private TypeReferenceResolver(TypeRefRow row, MetadataSystem metadata)
        {
            _row = row;
            _metadata = metadata;
        }

        public static TypeReference Resolve(TypeRefRow row, MetadataSystem metadata)
        {
            var resolver = new TypeReferenceResolver(row, metadata);
            return new TypeReference(resolver);
        }

        public IAssemblyInfo GetAssembly()
        {
            return _metadata.GetAssemblyInfo(_row.ResolutionScope);
        }

        public ElementType GetElementType(ITypeInfo type)
        {
            var knownTypeCode = KnownType.GetTypeCode(type);
            if (knownTypeCode != KnownTypeCode.None)
                return KnownType.GetElementType(knownTypeCode);

            return ElementType.Class;
        }

        public string GetName()
        {
            return _metadata.ResolveString(_row.Name);
        }

        public string GetNamespace()
        {
            return _metadata.ResolveString(_row.Namespace);
        }
    }
}
