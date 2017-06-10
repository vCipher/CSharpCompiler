using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class FieldReferenceResolver : IFieldReferenceResolver
    {
        private MemberRefRow _row;
        private ByteBuffer _signature;
        private MetadataSystem _metadata;

        private FieldReferenceResolver(MemberRefRow row, ByteBuffer signature, MetadataSystem metadata)
        {
            _row = row;
            _signature = signature;
            _metadata = metadata;
        }

        public static FieldReference Resolve(MemberRefRow row, ByteBuffer signature, MetadataSystem metadata)
        {
            var resolver = new FieldReferenceResolver(row, signature, metadata);
            return new FieldReference(resolver);
        }

        public ITypeInfo GetDeclaringType()
        {
            throw new NotImplementedException();
        }

        public ITypeInfo GetFieldType()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
