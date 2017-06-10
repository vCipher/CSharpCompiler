using CSharpCompiler.PE.Cil;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataBuildResult
    {
        public MetadataToken EntryPointToken { get; private set; }
        public MetadataHeaps Heaps { get; private set; }
        public ILCodeWriter ILCode { get; private set; }

        public MetadataBuildResult(MetadataToken entryPointToken, MetadataHeaps heaps, ILCodeWriter ilCode)
        {
            EntryPointToken = entryPointToken;
            Heaps = heaps;
            ILCode = ilCode;
        }
    }
}
