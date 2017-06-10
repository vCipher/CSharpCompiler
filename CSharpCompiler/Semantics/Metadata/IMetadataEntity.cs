namespace CSharpCompiler.Semantics.Metadata
{
    public interface IMetadataEntity : System.IEquatable<IMetadataEntity>
    {
        void Accept(IMetadataEntityVisitor visitor);
    }
}
