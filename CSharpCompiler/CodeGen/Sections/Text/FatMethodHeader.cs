using CSharpCompiler.CodeGen.Metadata;
using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Sections.Text
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FatMethodHeader
    {
        public MethodBodyAttributes Attributes;
        public byte Size;
        public ushort MaxStack;
        public uint CodeSize;
        public MetadataToken LocalVarSigTok;
    }
}
