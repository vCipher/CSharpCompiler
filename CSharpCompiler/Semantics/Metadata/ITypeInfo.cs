namespace CSharpCompiler.Semantics.Metadata
{
    public interface ITypeInfo : IMetadataEntity
    {
        string Name { get; }
        string Namespace { get; }
        ElementType ElementType { get; }
        IAssemblyInfo Assembly { get; }
    }
}