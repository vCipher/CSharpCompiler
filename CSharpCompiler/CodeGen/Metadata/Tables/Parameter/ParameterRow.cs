using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Parameter
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ParameterRow
    {
        public ParameterAttributes Attributes;
        public ushort Sequence;
        public ushort Name;
    }
}
