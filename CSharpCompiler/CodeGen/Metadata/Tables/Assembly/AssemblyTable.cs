using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Assembly
{
    public sealed class AssemblyTable : MetadataTable<AssemblyRow>
    {
        private MetadataBuilder _metadata;

        public AssemblyTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void Add(AssemblyDefinition assemblyDef)
        {
            AssemblyRow row = CreateAssemblyRow(assemblyDef);
            assemblyDef.ResolveToken(Add(row));
        }

        private AssemblyRow CreateAssemblyRow(AssemblyDefinition assemblyDef)
        {
            AssemblyRow row = new AssemblyRow();
            row.HashAlgId = HashAlgorithm.SHA1;
            row.MajorVersion = (ushort)assemblyDef.Version.Major;
            row.MinorVersion = (ushort)assemblyDef.Version.Minor;
            row.BuildNumber = (ushort)assemblyDef.Version.Build;
            row.RevisionNumber = (ushort)assemblyDef.Version.Revision;
            row.Attributes = AssemblyAttributes.None;
            row.PublicKey = _metadata.RegisterBlob(assemblyDef.PublicKey);
            row.Name = _metadata.RegisterString(assemblyDef.Name);
            row.Culture = _metadata.RegisterString(assemblyDef.Culture);

            return row;
        }
    }
}
