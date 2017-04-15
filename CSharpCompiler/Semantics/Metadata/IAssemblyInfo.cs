using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface IAssemblyInfo : IMetadataEntity, IEquatable<IAssemblyInfo>
    {
        string Name { get; }
        string Culture { get; }
        AssemblyAttributes Attributes { get; }
        Version Version { get; }
        byte[] PublicKey { get; }
        byte[] PublicKeyToken { get; }
        byte[] Hash { get; }
    }
}