using System;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyDefinition : IAssemblyInfo, ICustomAttributeProvider
    {
        public string Name { get; private set; }
        public byte[] PublicKey { get; private set; }
        public byte[] PublicKeyToken { get; private set; }
        public Version Version { get; private set; }
        public string Culture { get; private set; }
        public byte[] Hash { get; private set; }
        public AssemblyAttributes Attributes { get; private set; }
        public ModuleDefinition Module { get; private set; }
        public MethodDefinition EntryPoint { get; set; }
        public Collection<AssemblyReference> References { get; private set; }
        public Collection<CustomAttribute> CustomAttributes { get; private set; }

        public AssemblyDefinition(string name, ModuleDefinition moduleDef)
        {
            Name = name;
            Version = new Version(0, 0, 0, 0);
            Attributes = AssemblyAttributes.None;
            Module = moduleDef;
            References = new Collection<AssemblyReference>();
            CustomAttributes = new Collection<CustomAttribute>();
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitAssemblyDefinition(this);
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
            return (obj is AssemblyDefinition) && Equals((AssemblyDefinition)obj);
        }

        public bool Equals(IAssemblyInfo other)
        {
            return (other is AssemblyDefinition) && Equals((AssemblyDefinition)other);
        }

        public bool Equals(AssemblyDefinition other)
        {
            return AssemblyInfoComparer.Default.Equals(this, other);
        }
    }
}
