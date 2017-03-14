namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ParameterDefinition : IMetadataEntity
    {
        public int Index { get { return Method.Parameters.IndexOf(this); } }
        public string Name { get; private set; }
        public TypeReference Type { get; private set; }
        public MethodReference Method { get; private set; }
        public MetadataToken Token { get; private set; }
        public ParameterAttributes Attributes { get; private set; }

        public ParameterDefinition(string name, TypeReference type, MethodReference method)
        {
            Name = name;
            Type = type;
            Method = method;
            Token = new MetadataToken(MetadataTokenType.Param, 0);
            Attributes = ParameterAttributes.None;
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(MetadataTokenType.Param, rid);
        }
    }
}
