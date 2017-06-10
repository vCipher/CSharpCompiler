using System;
using System.Globalization;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface IAssemblyInfo : IMetadataEntity
    {
        string Name { get; }
        string Culture { get; }
        AssemblyAttributes Attributes { get; }
        AssemblyHashAlgorithm HashAlgorithm { get; }
        Version Version { get; }
        byte[] PublicKey { get; }
        byte[] PublicKeyToken { get; }
        byte[] Hash { get; }
    }
}