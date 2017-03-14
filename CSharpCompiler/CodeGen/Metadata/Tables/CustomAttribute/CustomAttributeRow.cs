using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.CustomAttribute
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CustomAttributeRow
    {
        public ushort Parent;
        public ushort Type;
        public ushort Value;
    }
}
