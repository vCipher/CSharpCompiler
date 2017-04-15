using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.TypeRef
{
    public sealed class TypeRefTable : MetadataTable<TypeReference, TypeRefRow>
    {
        private MetadataBuilder _metadata;

        public TypeRefTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public override MetadataToken GetToken(TypeReference typeRef)
        {
            MetadataToken token;
            if (TryGetToken(typeRef, out token))
                return token;

            uint rid = Add(typeRef, CreateTypeRefRow(typeRef));
            return new MetadataToken(GetTokenType(), rid);
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.TypeRef;
        }

        private TypeRefRow CreateTypeRefRow(TypeReference typeRef)
        {
            return new TypeRefRow()
            {
                ResolutionScope = _metadata.GetCodedRid(typeRef.Assembly, CodedTokenType.ResolutionScope),
                Namespace = _metadata.RegisterString(typeRef.Namespace),
                Name = _metadata.RegisterString(typeRef.Name)
            };
        }
    }
}
