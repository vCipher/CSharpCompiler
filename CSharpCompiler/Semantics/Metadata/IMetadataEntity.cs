using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface IMetadataEntity
    {
        MetadataToken Token { get; }
        void ResolveToken(uint rid);
    }
}
