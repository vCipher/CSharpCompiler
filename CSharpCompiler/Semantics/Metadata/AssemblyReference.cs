using System;
using System.Text;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyReference : IAssemblyInfo
    {
        public string Name { get; private set; }
        public byte[] PublicKey { get; private set; }
        public byte[] PublicKeyToken { get; private set; }
        public Version Version { get; private set; }
        public string Culture { get; private set; }
        public byte[] Hash { get; private set; }
        public AssemblyAttributes Attributes { get; private set; }
        
        public AssemblyReference(string name, byte[] publicKey, byte[] publicKeyToken, Version version, string culture, byte[] hash, AssemblyAttributes attributes)
        {
            Name = name;
            PublicKey = publicKey;
            PublicKeyToken = publicKeyToken;
            Version = version;
            Culture = culture;
            Hash = hash;
            Attributes = attributes;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitAssemblyReference(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
            sb.AppendFormat(", Version = {0}", Version);
            sb.AppendFormat(", Culture = {0}", Culture);
            sb.AppendFormat(", PublicKeyToken = {0}", BitConverter.ToString(PublicKeyToken)
                .Replace("-", "")
                .ToLower());

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return AssemblyInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is AssemblyReference) && Equals((AssemblyReference)obj);
        }

        public bool Equals(IAssemblyInfo other)
        {
            return (other is AssemblyReference) && Equals((AssemblyReference)other);
        }

        public bool Equals(AssemblyReference other)
        {
            return AssemblyInfoComparer.Default.Equals(this, other);
        }
    }
}
