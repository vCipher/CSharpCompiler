namespace CSharpCompiler.CodeGen.Sections.Resources
{
    public sealed class RsrcSection : ISection
    {
        public SectionHeader Header { get; set; }
        public Win32ResourceBuffer Buffer { get; set; }
    }
}
