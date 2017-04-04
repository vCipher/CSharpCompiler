using CSharpCompiler.CodeGen.Sections.Text;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Sections.Relocations
{
    public sealed class RelocationBuffer : ByteBuffer
    {
        public RelocationBuffer(TextSection text) : base()
        {
            const uint blockSize = 0x000c;
            uint relocRVA = text.Map[TextSegment.StartupStub].VirtualAddress + 2;
            uint pageRVA = relocRVA & ~0xfffu;

            WriteUInt32(pageRVA);
            WriteUInt32(blockSize);
            WriteUInt32(0x3000 + relocRVA - pageRVA);
        }
    }
}
