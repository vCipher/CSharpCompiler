using CSharpCompiler.Semantics.Metadata;
using System;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IAssemblyInfoResolver
    {
        string GetName();
        string GetCulture();
        byte[] GetHash();
        byte[] GetPublicKey();
        byte[] GetPublicKeyToken();
        Version GetVersion();
        AssemblyAttributes GetAttributes();
        AssemblyHashAlgorithm GetHashAlgorithm();
    }
}
