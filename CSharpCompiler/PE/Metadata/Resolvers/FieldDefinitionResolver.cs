using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class FieldDefinitionResolver : IFieldDefinitionResolver
    {
        private MetadataToken _token;
        private FieldRow _row;
        private MetadataSystem _metadata;
        private TypeDefinition _declaringType;

        private FieldDefinitionResolver(uint rid, FieldRow row, MetadataSystem metadata, TypeDefinition declaringType)
        {
            _token = new MetadataToken(MetadataTokenType.Field, rid);
            _row = row;
            _metadata = metadata;
            _declaringType = declaringType;
        }

        public static FieldDefinition Resolve(uint rid, FieldRow row, MetadataSystem metadata, TypeDefinition declaringType)
        {
            var resolver = new FieldDefinitionResolver(rid, row, metadata, declaringType);
            return new FieldDefinition(resolver);
        }

        public FieldAttributes GetAttributes()
        {
            return _row.Attributes;
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
            return _metadata.ResolveString(_row.Name);
        }
    }
}
