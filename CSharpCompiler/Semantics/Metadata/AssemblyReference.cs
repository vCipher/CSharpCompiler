using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyReference : IAssemblyInfo
    {
        public string Name { get; private set; }
        public byte[] PublicKey { get; private set; }
        public byte[] PublicKeyToken { get; private set; }
        public MetadataToken Token { get; private set; }
        public Version Version { get; private set; }
        public string Culture { get; private set; }
        public byte[] Hash { get; private set; }
        public AssemblyAttributes Attributes { get; private set; }
        
        public AssemblyReference(System.Reflection.AssemblyName assemblyName)
        {
            Name = assemblyName.Name;
            PublicKey = assemblyName.GetPublicKey();
            PublicKeyToken = assemblyName.GetPublicKeyToken();
            Token = new MetadataToken(MetadataTokenType.AssemblyRef, 0);
            Version = assemblyName.Version;
            Culture = assemblyName.CultureName;
            Attributes = AssemblyAttributes.None;
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
