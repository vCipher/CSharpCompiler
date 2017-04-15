using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface ITypeInfo : IMetadataEntity, IEquatable<ITypeInfo>
    {
        string Name { get; }
        string Namespace { get; }
        ElementType ElementType { get; }
        IAssemblyInfo Assembly { get; }
    }
}