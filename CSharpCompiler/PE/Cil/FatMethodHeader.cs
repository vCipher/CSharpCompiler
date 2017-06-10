using CSharpCompiler.PE.Metadata.Tokens;
using System.Runtime.InteropServices;

namespace CSharpCompiler.PE.Cil
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
