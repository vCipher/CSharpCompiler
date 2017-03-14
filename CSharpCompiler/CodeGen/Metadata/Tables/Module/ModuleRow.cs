using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Module
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ModuleRow
    {
        public ushort Generation;
        public ushort NameIndex;
        public ushort Mvid;
        public ushort EncId;
        public ushort EncBaseId;
    }
}