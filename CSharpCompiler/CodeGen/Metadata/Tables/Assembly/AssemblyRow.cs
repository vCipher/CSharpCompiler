using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Assembly
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyRow
    {
        public HashAlgorithm HashAlgId;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort BuildNumber;
        public ushort RevisionNumber;
        public AssemblyAttributes Attributes;
        public ushort PublicKey;
        public ushort Name;
        public ushort Culture;
    }
}
