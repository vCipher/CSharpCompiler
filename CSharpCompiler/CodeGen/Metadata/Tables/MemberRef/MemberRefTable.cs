using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Metadata.Tables.MemberRef
{
    public sealed class MemberRefTable : MetadataTable<MemberRefRow>
    {
        private Dictionary<MethodReference, MetadataToken> _memberRefMap;
        private MetadataBuilder _metadata;

        public MemberRefTable(MetadataBuilder metadata)
        {
            _memberRefMap = new Dictionary<MethodReference, MetadataToken>();
            _metadata = metadata;
        }

        public MetadataToken GetMemberRefToken(MethodReference methodRef)
        {
            if (methodRef.Token.RID != 0)
                return methodRef.Token;
            
            MetadataToken token;
            if (_memberRefMap.TryGetValue(methodRef, out token))
            {
                methodRef.ResolveToken(token.RID);
                return token;
            }

            uint rid = Add(CreateMemberRefRow(methodRef));
            methodRef.ResolveToken(rid);
            _memberRefMap.Add(methodRef, methodRef.Token);

            return methodRef.Token;
        }

        private MemberRefRow CreateMemberRefRow(MethodReference methodRef)
        {
            MemberRefRow row = new MemberRefRow();
            row.Class = _metadata.GetCodedRID(methodRef.DeclaringType, CodedTokenType.MemberRefParent);
            row.Name = _metadata.RegisterString(methodRef.Name);
            row.Signature = _metadata.WriteSignature(methodRef);

            return row;
        }
    }
}
