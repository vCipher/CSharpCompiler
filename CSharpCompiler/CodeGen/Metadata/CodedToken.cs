using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CodedToken
    {
        public readonly uint Value;

        public CodedToken(uint value)
        {
            Value = value;
        }
    }
}
