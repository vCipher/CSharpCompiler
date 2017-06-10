namespace CSharpCompiler.PE.Sections.Relocations
{
    public sealed class RelocSection : ISection
    {
        public SectionHeader Header { get; set; }
        public RelocationBuffer Buffer { get; set; }
    }
}
