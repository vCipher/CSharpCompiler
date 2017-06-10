using CSharpCompiler.PE.Sections.Text;
using CSharpCompiler.Utility;

namespace CSharpCompiler.PE.Sections.Relocations
{
    public sealed class RelocationBuffer : ByteBuffer
    {
        public RelocationBuffer(TextSection text) : base()
        {
            const uint blockSize = 0x000c;
            uint relocRVA = text.Map[TextSegment.StartupStub].RVA + 2;
            uint pageRVA = relocRVA & ~0xfffu;

            WriteUInt32(pageRVA);
            WriteUInt32(blockSize);
            WriteUInt32(0x3000 + relocRVA - pageRVA);
        }
    }
}
