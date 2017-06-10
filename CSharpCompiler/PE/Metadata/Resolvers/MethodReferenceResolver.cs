using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class MethodReferenceResolver : IMethodReferenceResolver
    {
        private MemberRefRow _row;
        private ByteBuffer _signature;
        private MetadataSystem _metadata;

        private MethodReferenceResolver(MemberRefRow row, ByteBuffer signature, MetadataSystem metadata)
        {
            _row = row;
            _signature = signature;
            _metadata = metadata;
        }

        public static MethodReference Resolve(MemberRefRow row, ByteBuffer signature, MetadataSystem metadata)
        {
            var resolver = new MethodReferenceResolver(row, signature, metadata);
            return new MethodReference(resolver);
        }

        public CallingConventions GetCallingConventions()
        {
            throw new NotImplementedException();
        }

        public ITypeInfo GetDeclaringType()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return _metadata.ResolveString(_row.Name);
        }

        public Collection<ParameterDefinition> GetParameters(MethodReference method)
        {
            throw new NotImplementedException();
        }

        public ITypeInfo GetReturnType()
        {
            throw new NotImplementedException();
        }
    }
}
