using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.MemberRef
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MemberRefRow
    {
        public ushort Class;
        public ushort Name;
        public ushort Signature;
    }
}
