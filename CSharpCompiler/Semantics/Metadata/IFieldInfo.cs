namespace CSharpCompiler.Semantics.Metadata
{
    public interface IFieldInfo : IMetadataEntity
    {
        string Name { get; }
        ITypeInfo FieldType { get; }
        ITypeInfo DeclaringType { get; }
    }
}
