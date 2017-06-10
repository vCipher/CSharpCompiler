using CSharpCompiler.PE.Metadata;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Utility;

namespace CSharpCompiler.PE.Sections.Text
{
    public sealed class TextSection : ISection
    {
        public SectionHeader Header { get; set; }
        public CLRHeader CLRHeader { get; set; }
        public TextMap Map { get; set; }
        public MetadataHeader MetadataHeader { get; set; }
        public MetadataToken EntryPointToken { get; set; }
        public MetadataBuildResult Metadata { get; set; }
        public ByteBuffer ILCode { get; set; }
        public ByteBuffer Resources { get; set; }
        public ByteBuffer Data { get; set; }
    }
}
