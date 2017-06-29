using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyDefinition : IAssemblyInfo, ICustomAttributeProvider
    {
        private object _syncLock;
        private IAssemblyDefinitionResolver _resolver;

        private LazyWrapper<string> _name;
        private LazyWrapper<string> _culture;
        private LazyWrapper<byte[]> _publicKey;
        private LazyWrapper<byte[]> _publicKeyToken;
        private LazyWrapper<Version> _version;
        private LazyWrapper<byte[]> _hash;
        private LazyWrapper<AssemblyAttributes> _attributes;
        private LazyWrapper<AssemblyHashAlgorithm> _hashAlgorithm;
        private LazyWrapper<ModuleDefinition> _module;
        private LazyWrapper<MethodDefinition> _entryPoint;
        private LazyWrapper<Collection<AssemblyReference>> _references;
        private LazyWrapper<Collection<CustomAttribute>> _customAttributes;

        public string Name => _name.GetValue(ref _syncLock, _resolver.GetName);
        public string Culture => _culture.GetValue(ref _syncLock, _resolver.GetCulture);
        public byte[] PublicKey => _publicKey.GetValue(ref _syncLock, _resolver.GetPublicKey);
        public byte[] PublicKeyToken => _publicKeyToken.GetValue(ref _syncLock, _resolver.GetPublicKeyToken);
        public Version Version => _version.GetValue(ref _syncLock, _resolver.GetVersion);
        public byte[] Hash => _hash.GetValue(ref _syncLock, _resolver.GetHash);
        public AssemblyAttributes Attributes => _attributes.GetValue(ref _syncLock, _resolver.GetAttributes);
        public AssemblyHashAlgorithm HashAlgorithm => _hashAlgorithm.GetValue(ref _syncLock, _resolver.GetHashAlgorithm);
        public ModuleDefinition Module => _module.GetValue(ref _syncLock, _resolver.GetModule);
        public MethodDefinition EntryPoint => _entryPoint.GetValue(ref _syncLock, _resolver.GetEntryPoint);
        public Collection<AssemblyReference> References => _references.GetValue(ref _syncLock, _resolver.GetReferences);
        public Collection<CustomAttribute> CustomAttributes => _customAttributes.GetValue(ref _syncLock, _resolver.GetCustomAttributes);

        public AssemblyDefinition(IAssemblyDefinitionResolver resolver)
        {
            _syncLock = new object();
            _resolver = resolver;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitAssemblyDefinition(this);
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
            return (obj is AssemblyDefinition) && Equals((AssemblyDefinition)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is AssemblyDefinition) && Equals((AssemblyDefinition)other);
        }

        public bool Equals(AssemblyDefinition other)
        {
            return AssemblyInfoComparer.Default.Equals(this, other);
        }
    }
}
