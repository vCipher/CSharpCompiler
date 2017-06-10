using System.Runtime.InteropServices;

namespace CSharpCompiler.PE.Metadata.Tokens
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CodedToken
    {
        public readonly uint Value;

        public CodedToken(uint value) : this()
        {
            Value = value;
        }
    }
}
