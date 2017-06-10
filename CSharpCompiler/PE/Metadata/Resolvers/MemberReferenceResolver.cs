using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public static class MemberReferenceResolver
    {
        private const int FIELD_SIG = 0x06;

        public static IMemberReference Resolve(MemberRefRow row, MetadataSystem metadata)
        {
            var blob = metadata.ResolveBlob(row.Signature);
            blob.MoveTo(ByteBuffer.START_POSITION);

            var sig = blob.ReadByte();
            blob.MoveTo(ByteBuffer.START_POSITION);

            if (sig == FIELD_SIG)
                return FieldReferenceResolver.Resolve(row, blob, metadata);

            return MethodReferenceResolver.Resolve(row, blob, metadata);
        }
    }
}
