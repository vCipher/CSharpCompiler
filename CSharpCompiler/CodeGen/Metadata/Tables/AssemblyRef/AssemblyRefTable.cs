using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Metadata.Tables.AssemblyRef
{
    public sealed class AssemblyRefTable : MetadataTable<AssemblyReference, AssemblyRefRow>
    {
        private MetadataBuilder _metadata;

        public AssemblyRefTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public override MetadataToken GetToken(AssemblyReference assemblyRef)
        {
            MetadataToken token;
            if (TryGetToken(assemblyRef, out token))
                return token;

            uint rid = Add(assemblyRef, GetAssemblyRefValue(assemblyRef));
            return new MetadataToken(GetTokenType(), rid);
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.AssemblyRef;
        }

        private AssemblyRefRow GetAssemblyRefValue(AssemblyReference assemblyRef)
        {
            return new AssemblyRefRow()
            {
                MajorVersion = (ushort)assemblyRef.Version.Major,
                MinorVersion = (ushort)assemblyRef.Version.Minor,
                BuildNumber = (ushort)assemblyRef.Version.Build,
                RevisionNumber = (ushort)assemblyRef.Version.Revision,
                Attributes = assemblyRef.Attributes,
                PublicKeyOrToken = _metadata.RegisterBlob(
                assemblyRef.PublicKeyToken.IsNullOrEmpty()
                    ? assemblyRef.PublicKey
                    : assemblyRef.PublicKeyToken),
                Name = _metadata.RegisterString(assemblyRef.Name),
                Culture = _metadata.RegisterString(assemblyRef.Culture),
                HashValue = _metadata.RegisterBlob(assemblyRef.Hash)
            };
        }
    }
}
