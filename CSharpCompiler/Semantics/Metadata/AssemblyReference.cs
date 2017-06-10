using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Globalization;
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
        public AssemblyHashAlgorithm HashAlgorithm { get; private set; }

        public AssemblyReference(string fullName)
        {
            if (fullName == null) throw new ArgumentNullException("fullName");
            if (fullName.Length == 0) throw new ArgumentException("Name can not be empty", "fullName");

            var tokens = fullName.Split(',');
            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i].Trim();

                if (i == 0)
                {
                    Name = token;
                    continue;
                }

                var parts = token.Split('=');
                if (parts.Length != 2)
                    throw new ArgumentException("Malformed name", "fullName");

                switch (parts[0].ToLowerInvariant())
                {
                    case "version":
                        Version = new Version(parts[1]);
                        break;
                    case "culture":
                        Culture = (parts[1] == "neutral") ? string.Empty : parts[1];
                        break;
                    case "publickeytoken":
                        PublicKeyToken = ParseBytes(parts[1]);
                        break;
                }
            }
        }

        public AssemblyReference(IAssemblyReferenceResolver resolver)
        {
            Name = resolver.GetName();
            PublicKey = resolver.GetPublicKey();
            PublicKeyToken = resolver.GetPublicKeyToken();
            Version = resolver.GetVersion();
            Culture = resolver.GetCulture();
            Hash = resolver.GetHash();
            Attributes = resolver.GetAttributes();
            HashAlgorithm = resolver.GetHashAlgorithm();
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitAssemblyReference(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);

            if (Version != null)
            {
                sb.AppendFormat(", Version = {0}", Version);
            }
            
            sb.AppendFormat(", Culture = {0}", string.IsNullOrEmpty(Culture) ? "neutral" : Culture);
            sb.Append(", PublicKeyToken = ");

            if (!PublicKeyToken.IsNullOrEmpty())
            {
                sb.Append(BitConverter.ToString(PublicKeyToken)
                    .Replace("-", "")
                    .ToLower());
            }
            else
            {
                sb.Append("null");
            }

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

        public bool Equals(IMetadataEntity other)
        {
            return (other is AssemblyReference) && Equals((AssemblyReference)other);
        }

        public bool Equals(AssemblyReference other)
        {
            return AssemblyInfoComparer.Default.Equals(this, other);
        }

        private byte[] ParseBytes(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "null")
                return Empty<byte>.Array;

            var bytes = new byte[value.Length / 2];
            for (int j = 0; j < bytes.Length; j++)
            {
                bytes[j] = Byte.Parse(value.Substring(j * 2, 2), NumberStyles.HexNumber);
            }

            return bytes;
        }
    }
}
