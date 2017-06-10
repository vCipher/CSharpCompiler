using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class ParameterDefinitionResolver : IParameterDefinitionResolver
    {
        private uint _rid;
        private ParamRow _row;
        private MetadataSystem _metadata;
        private IMethodInfo _method;
        private ITypeInfo _type;

        private ParameterDefinitionResolver(uint rid, ParamRow row, MetadataSystem metadata, IMethodInfo method, ITypeInfo type)
        {
            _rid = rid;
            _row = row;
            _metadata = metadata;
            _method = method;
            _type = type;
        }

        public static ParameterDefinition Resolve(uint rid, ParamRow row, MetadataSystem metadata, IMethodInfo method, ITypeInfo type)
        {
            var resolver = new ParameterDefinitionResolver(rid, row, metadata, method, type);
            return new ParameterDefinition(resolver);
        }

        public ParameterAttributes GetAttributes()
        {
            return _row.Attributes;
        }

        public int GetIndex()
        {
            return _row.Sequence;
        }

        public IMethodInfo GetMethod()
        {
            return _method;
        }

        public string GetName()
        {
            return _metadata.ResolveString(_row.Name);
        }

        public ITypeInfo GetParameterType()
        {
            return _type;
        }
    }
}
