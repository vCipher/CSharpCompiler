using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyDefinition : IAssemblyInfo, ICustomAttributeProvider
    {
        private Lazy<ModuleDefinition> _module;
        private Lazy<MethodDefinition> _entryPoint;
        private Lazy<Collection<AssemblyReference>> _references;
        private Lazy<Collection<CustomAttribute>> _customAttributes;

        public string Name { get; private set; }
        public string Culture { get; private set; }
        public byte[] PublicKey { get; private set; }
        public byte[] PublicKeyToken { get; private set; }
        public Version Version { get; private set; }
        public byte[] Hash { get; private set; }
        public AssemblyAttributes Attributes { get; private set; }
        public AssemblyHashAlgorithm HashAlgorithm { get; private set; }

        public ModuleDefinition Module => _module.Value;
        public MethodDefinition EntryPoint => _entryPoint.Value;
        public Collection<AssemblyReference> References => _references.Value;
        public Collection<CustomAttribute> CustomAttributes => _customAttributes.Value;

        public AssemblyDefinition(IAssemblyDefinitionResolver resolver)
        {
            Name = resolver.GetName();
            PublicKey = resolver.GetPublicKey();
            PublicKeyToken = resolver.GetPublicKeyToken();
            Version = resolver.GetVersion();
            Culture = resolver.GetCulture();
            Hash = resolver.GetHash();
            Attributes = resolver.GetAttributes();
            HashAlgorithm = resolver.GetHashAlgorithm();
            _module = new Lazy<ModuleDefinition>(resolver.GetModule);
            _entryPoint = new Lazy<MethodDefinition>(resolver.GetEntryPoint);
            _references = new Lazy<Collection<AssemblyReference>>(resolver.GetReferences);
            _customAttributes = new Lazy<Collection<CustomAttribute>>(resolver.GetCustomAttributes);
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
