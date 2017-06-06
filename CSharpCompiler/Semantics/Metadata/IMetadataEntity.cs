namespace CSharpCompiler.Semantics.Metadata
{
    public interface IMetadataEntity
    {
        void Accept(IMetadataEntityVisitor visitor);
    }
}
