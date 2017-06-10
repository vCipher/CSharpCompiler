using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class CustomAttributeResolver : ICustomAttributeResolver
    {
        private CustomAttributeRow _row;
        private MetadataSystem _metadata;

        private CustomAttributeResolver(CustomAttributeRow row, MetadataSystem metadata)
        {
            _row = row;
            _metadata = metadata;
        }

        public static CustomAttribute Resolve(CustomAttributeRow row, MetadataSystem metadata)
        {
            var resolver = new CustomAttributeResolver(row, metadata);
            return new CustomAttribute(resolver);
        }

        public IAssemblyInfo GetAssembly()
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetConstructor()
        {
            return _metadata.GetMethodInfo(_row.Type);
        }

        public string GetName()
        {
            return string.Empty;
        }

        public string GetNamespace()
        {
            return string.Empty;
        }

        public ICustomAttributeProvider GetOwner()
        {
            var token = _row.Parent;
            switch (token.Type)
            {
                case MetadataTokenType.Assembly: return _metadata.Assembly;
                case MetadataTokenType.Method: return _metadata.GetMethodDefinition(token.RID);
                default: throw new NotSupportedException();
            }
        }
    }
}
