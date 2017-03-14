using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyDefinition : IAssemblyInfo, ICustomAttributeProvider
    {
        public string Name { get; private set; }
        public byte[] PublicKey { get; private set; }
        public byte[] PublicKeyToken { get; private set; }
        public MetadataToken Token { get; private set; }
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
            Token = new MetadataToken(MetadataTokenType.Assembly, 0);
            Version = new System.Version(0, 0, 0, 0);
            Attributes = AssemblyAttributes.None;
            Module = moduleDef;
            Token = new MetadataToken(MetadataTokenType.Assembly, 0);
            References = new Collection<AssemblyReference>();
            CustomAttributes = new Collection<CustomAttribute>();
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
