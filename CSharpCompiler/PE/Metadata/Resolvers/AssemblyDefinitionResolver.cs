using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class AssemblyDefinitionResolver : IAssemblyDefinitionResolver
    {
        private MetadataToken _token;
        private AssemblyRow _row;
        private MetadataSystem _metadata;

        private AssemblyDefinitionResolver(uint rid, AssemblyRow row, MetadataSystem metadata)
        {
            _token = new MetadataToken(MetadataTokenType.Assembly, rid);
            _row = row;
            _metadata = metadata;
        }

        public static AssemblyDefinition Resolve(AssemblyRow row, MetadataSystem metadata)
        {
            var resolver = new AssemblyDefinitionResolver(1, row, metadata);
            return new AssemblyDefinition(resolver);
        }

        public static AssemblyDefinition Resolve(uint rid, AssemblyRow row, MetadataSystem metadata)
        {
            var resolver = new AssemblyDefinitionResolver(rid, row, metadata);
            return new AssemblyDefinition(resolver);
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

        public byte[] GetPublicKey()
        {
            return _metadata.ResolveBytes(_row.PublicKey);
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
            return _row.HashAlgId;
        }

        public byte[] GetHash()
        {
            return Empty<byte>.Array;
        }

        public byte[] GetPublicKeyToken()
        {
            return ComputePublicKeyToken();
        }

        public ModuleDefinition GetModule()
        {
            return _metadata.Module;
        }

        public Collection<CustomAttribute> GetCustomAttributes()
        {
            return _metadata
                .GetCustomAttributes(_token)
                .ToCollection();
        }

        public Collection<AssemblyReference> GetReferences()
        {
            return new Collection<AssemblyReference>(_metadata.AssemblyReferences);
        }

        public MethodDefinition GetEntryPoint()
        {
            return _metadata.EntryPoint;
        }

        private byte[] ComputePublicKeyToken()
        {
            var publicKey = _metadata.ResolveBytes(_row.PublicKey);
            var hash = ComputeHash(publicKey);
            var token = new byte[8];

            Array.Copy(hash, (hash.Length - 8), token, 0, 8);
            Array.Reverse(token, 0, 8);

            return token;
        }

        private byte[] ComputeHash(byte[] bytes)
        {
            switch (_row.HashAlgId)
            {
                case AssemblyHashAlgorithm.MD5:
                    using (var algorithm = MD5.Create())
                    {
                        return algorithm.ComputeHash(GetPublicKey());
                    }
                case AssemblyHashAlgorithm.SHA1:
                case AssemblyHashAlgorithm.None:
                default:
                    using (var algorithm = SHA1.Create())
                    {
                        return algorithm.ComputeHash(GetPublicKey());
                    }
            }
        }
    }
}
