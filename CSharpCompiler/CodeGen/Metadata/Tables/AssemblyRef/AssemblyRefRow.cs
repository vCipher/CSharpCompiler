using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.AssemblyRef
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyRefRow
    {
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort BuildNumber;
        public ushort RevisionNumber;
        public AssemblyAttributes Attributes;
        public ushort PublicKeyOrToken;
        public ushort Name;
        public ushort Culture;
        public ushort HashValue;
    }
}
