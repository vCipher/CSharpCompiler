namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class FieldDefinition : IMetadataEntity
    {
        public FieldDefinition()
        { }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitFieldDefinition(this);
        }
    }
}
