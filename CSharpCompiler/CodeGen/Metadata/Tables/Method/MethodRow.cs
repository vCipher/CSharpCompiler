using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Method
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MethodRow
    {
        public uint RVA;
        public MethodImplAttributes ImplAttributes;
        public MethodAttributes Attributes;
        public ushort Name;
        public ushort Signature;
        public ushort ParamList;
    }
}
