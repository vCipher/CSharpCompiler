using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.StandAloneSig
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StandAloneSigRow
    {
        public ushort Signature;
    }
}
