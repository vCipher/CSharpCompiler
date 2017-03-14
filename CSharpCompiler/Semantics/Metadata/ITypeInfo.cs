using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface ITypeInfo : IMetadataEntity
    {
        string Name { get; }
        string Namespace { get; }
        IType DeclaringType { get; }
        IAssemblyInfo Assembly { get; }        
    }
}