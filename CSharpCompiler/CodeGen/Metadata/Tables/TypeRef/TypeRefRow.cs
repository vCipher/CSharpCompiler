using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.TypeRef
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TypeRefRow
    {
        public ushort ResolutionScope;
        public ushort Name;
        public ushort Namespace;
    }
}
