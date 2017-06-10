using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Security.Cryptography;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class AssemblyReferenceResolver : IAssemblyReferenceResolver
    {
        private MetadataToken _token;
        private AssemblyRefRow _row;
        private MetadataSystem _metadata;

        private AssemblyReferenceResolver(uint rid, AssemblyRefRow row, MetadataSystem metadata)
        {
            _token = new MetadataToken(MetadataTokenType.AssemblyRef, rid);
            _row = row;
            _metadata = metadata;
        }

        public static AssemblyReference Resolve(uint rid, AssemblyRefRow row, MetadataSystem metadata)
        {
            var resolver = new AssemblyReferenceResolver(rid, row, metadata);
            return new AssemblyReference(resolver);
        }

        public string GetName()
        {
            return _metadata.ResolveString(_row.Name);
        }

        public string GetCulture()
        {
            return _metadata
                .ResolveString(_row.Culture)
                .EmptyIfNull();
        }

        public byte[] GetHash()
        {
            return _metadata.ResolveBytes(_row.HashValue);
        }

        public byte[] GetPublicKey()
        {
            return (_row.Attributes & AssemblyAttributes.PublicKey) != 0
                ? _metadata.ResolveBytes(_row.PublicKeyOrToken)
                : Empty<byte>.Array;
        }

        public byte[] GetPublicKeyToken()
        {
            return (_row.Attributes & AssemblyAttributes.PublicKey) != 0
                ? ComputePublicKeyToken()
                : _metadata.ResolveBytes(_row.PublicKeyOrToken);
        }

        public Version GetVersion()
        {
            return new Version(_row.MajorVersion, _row.MinorVersion, _row.BuildNumber, _row.RevisionNumber);
        }

        public AssemblyAttributes GetAttributes()
        {
            return _row.Attributes;
        }

        public AssemblyHashAlgorithm GetHashAlgorithm()
        {
            return AssemblyHashAlgorithm.None;
        }

        private byte[] ComputePublicKeyToken()
        {
            var publicKey = _metadata.ResolveBytes(_row.PublicKeyOrToken);
            var hash = ComputeHash(publicKey);
            var token = new byte[8];

            Array.Copy(hash, (hash.Length - 8), token, 0, 8);
            Array.Reverse(token, 0, 8);

            return token;
        }

        private byte[] ComputeHash(byte[] bytes)
        {
            using (var algorithm = SHA1.Create())
            {
                return algorithm.ComputeHash(bytes);
            }
        }
    }
}
