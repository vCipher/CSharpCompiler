namespace CSharpCompiler.Semantics.Metadata
{
    public interface IMemberReference : IMetadataEntity
    {
        string Name { get; }
        ITypeInfo DeclaringType { get; }
    }
}
