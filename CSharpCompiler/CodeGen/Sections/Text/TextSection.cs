using CSharpCompiler.CodeGen.Metadata;

namespace CSharpCompiler.CodeGen.Sections.Text
{
    public sealed class TextSection : ISection
    {
        public SectionHeader Header { get; set; }
        public CLRHeader CLRHeader { get; set; }
        public TextMap Map { get; set; }
        public MetadataContainer Metadata { get; set; }
        public ByteBuffer Resources { get; set; }
        public ByteBuffer Data { get; set; }
    }
}
