using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Metadata.Tables.TypeRef
{
    public sealed class TypeRefTable : MetadataTable<TypeRefRow>
    {
        private Dictionary<TypeReference, MetadataToken> _typeRefMap;
        private MetadataBuilder _metadata;

        public TypeRefTable(MetadataBuilder metadata)
        {
            _typeRefMap = new Dictionary<TypeReference, MetadataToken>(new TypeComparer());
            _metadata = metadata;
        }

        public MetadataToken GetTypeRefToken(TypeReference typeRef)
        {
            if (typeRef.Token.RID != 0)
                return typeRef.Token;
            
            MetadataToken token;
            if (_typeRefMap.TryGetValue(typeRef, out token))
            {
                typeRef.ResolveToken(token.RID);
                return token;
            }

            uint rid = Add(CreateTypeRefRow(typeRef));
            typeRef.ResolveToken(rid);
            _typeRefMap.Add(typeRef, typeRef.Token);

            return typeRef.Token;
        }

        private TypeRefRow CreateTypeRefRow(TypeReference typeRef)
        {
            TypeRefRow row = new TypeRefRow();
            row.ResolutionScope = _metadata.GetCodedRID(typeRef.Assembly, CodedTokenType.ResolutionScope);
            row.Namespace = _metadata.RegisterString(typeRef.Namespace);
            row.Name = _metadata.RegisterString(typeRef.Name);

            return row;
        }
    }
}
