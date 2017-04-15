using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;
using System;

namespace CSharpCompiler.CodeGen.Metadata.Tables.MemberRef
{
    public sealed class MemberRefTable : MetadataTable<MethodReference, MemberRefRow>
    {
        private MetadataBuilder _metadata;

        public MemberRefTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public override MetadataToken GetToken(MethodReference methodRef)
        {
            MetadataToken token;
            if (TryGetToken(methodRef, out token))
                return token;

            uint rid = Add(methodRef, CreateMemberRefRow(methodRef));
            return new MetadataToken(GetTokenType(), rid);
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.MemberRef;
        }

        private MemberRefRow CreateMemberRefRow(MethodReference methodRef)
        {
            return new MemberRefRow()
            {
                Class = _metadata.GetCodedRid(methodRef.DeclaringType, CodedTokenType.MemberRefParent),
                Name = _metadata.RegisterString(methodRef.Name),
                Signature = _metadata.WriteSignature(methodRef)
            };
        }
    }
}
