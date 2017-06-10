using CSharpCompiler.PE.Metadata.Signatures;
using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class MethodDefinitionResolver : IMethodDefinitionResolver
    {
        private MetadataToken _token;
        private MethodRow _row;
        private MetadataSystem _metadata;
        private TypeDefinition _declaringType;
        private Lazy<MethodSignatureReader> _signature;

        private MethodDefinitionResolver(uint rid, MethodRow row, MetadataSystem metadata, TypeDefinition declaringType)
        {
            _token = new MetadataToken(MetadataTokenType.Method, rid);
            _row = row;
            _metadata = metadata;
            _declaringType = declaringType;
            _signature = new Lazy<MethodSignatureReader>(() => new MethodSignatureReader(_row, _metadata));
        }

        public static MethodDefinition Resolve(uint rid, MethodRow row, MetadataSystem metadata, TypeDefinition declaringType)
        {
            var resolver = new MethodDefinitionResolver(rid, row, metadata, declaringType);
            return new MethodDefinition(resolver);
        }

        public MethodAttributes GetAttributes()
        {
            return _row.Attributes;
        }

        public CallingConventions GetCallingConventions()
        {
            return _signature.Value.CallingConventions;
        }

        public Collection<CustomAttribute> GetCustomAttributes(MethodDefinition method)
        {
            return _metadata.GetCustomAttributes(_token)
                .ToCollection();
        }

        public ITypeInfo GetDeclaringType()
        {
            return _declaringType;
        }

        public MethodImplAttributes GetImplAttributes()
        {
            return _row.ImplAttributes;
        }

        public MethodBody GetMethodBody()
        {
            return null;
        }

        public string GetName()
        {
            return _metadata.ResolveString(_row.Name);
        }

        public Collection<ParameterDefinition> GetParameters(MethodDefinition method)
        {
            return _metadata.GetParameters(_row, _signature.Value, method);
        }

        public ITypeInfo GetReturnType()
        {
            return _signature.Value.ReturnType;
        }
    }
}
