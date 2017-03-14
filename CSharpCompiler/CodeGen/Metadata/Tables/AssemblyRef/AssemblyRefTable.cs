using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Metadata.Tables.AssemblyRef
{
    public sealed class AssemblyRefTable : MetadataTable<AssemblyRefRow>
    {
        private Dictionary<AssemblyReference, MetadataToken> _assemblyRefMap;
        private MetadataBuilder _metadata;

        public AssemblyRefTable(MetadataBuilder metadata)
        {
            _assemblyRefMap = new Dictionary<AssemblyReference, MetadataToken>(new AssemblyComparer());
            _metadata = metadata;
        }

        public MetadataToken GetAssemblyRefToken(AssemblyReference assemblyRef)
        {
            if (assemblyRef.Token.RID != 0)
                return assemblyRef.Token;
            
            MetadataToken token;
            if (_assemblyRefMap.TryGetValue(assemblyRef, out token))
            {
                assemblyRef.ResolveToken(token.RID);
                return token;
            }

            uint rid = Add(GetAssemblyRefValue(assemblyRef));
            assemblyRef.ResolveToken(rid);
            _assemblyRefMap.Add(assemblyRef, assemblyRef.Token);

            return assemblyRef.Token;
        }

        private AssemblyRefRow GetAssemblyRefValue(AssemblyReference assemblyRef)
        {
            AssemblyRefRow value = new AssemblyRefRow();
            value.MajorVersion = (ushort)assemblyRef.Version.Major;
            value.MinorVersion = (ushort)assemblyRef.Version.Minor;
            value.BuildNumber = (ushort)assemblyRef.Version.Build;
            value.RevisionNumber = (ushort)assemblyRef.Version.Revision;
            value.Attributes = assemblyRef.Attributes;
            value.PublicKeyOrToken = _metadata.RegisterBlob(
                assemblyRef.PublicKeyToken.IsNullOrEmpty()
                    ? assemblyRef.PublicKey
                    : assemblyRef.PublicKeyToken);
            value.Name = _metadata.RegisterString(assemblyRef.Name);
            value.Culture = _metadata.RegisterString(assemblyRef.Culture);
            value.HashValue = _metadata.RegisterBlob(assemblyRef.Hash);

            return value;
        }
    }
}
