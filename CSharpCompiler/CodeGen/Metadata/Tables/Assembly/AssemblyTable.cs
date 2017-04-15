using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Assembly
{
    public sealed class AssemblyTable : MetadataTable<AssemblyDefinition, AssemblyRow>
    {
        private MetadataBuilder _metadata;

        public AssemblyTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void Add(AssemblyDefinition assemblyDef)
        {
            Add(assemblyDef, CreateAssemblyRow(assemblyDef));
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.Assembly;
        }

        private AssemblyRow CreateAssemblyRow(AssemblyDefinition assemblyDef)
        {
            return new AssemblyRow()
            {
                HashAlgId = HashAlgorithm.SHA1,
                MajorVersion = (ushort)assemblyDef.Version.Major,
                MinorVersion = (ushort)assemblyDef.Version.Minor,
                BuildNumber = (ushort)assemblyDef.Version.Build,
                RevisionNumber = (ushort)assemblyDef.Version.Revision,
                Attributes = AssemblyAttributes.None,
                PublicKey = _metadata.RegisterBlob(assemblyDef.PublicKey),
                Name = _metadata.RegisterString(assemblyDef.Name),
                Culture = _metadata.RegisterString(assemblyDef.Culture)
            };
        }
    }
}
