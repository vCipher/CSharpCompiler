namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class FieldDefinition : IMetadataEntity
    {
        public MetadataToken Token { get; private set; }

        public FieldDefinition()
        {
            Token = new MetadataToken(MetadataTokenType.Field, 0);
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(MetadataTokenType.Field, rid);
        }
    }
}
