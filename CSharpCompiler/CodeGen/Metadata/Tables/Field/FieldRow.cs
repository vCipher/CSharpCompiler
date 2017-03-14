using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Field
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FieldRow
    {
        public FieldAttributes Attributes;
        public ushort Name;
        public ushort Signature;
    }
}
