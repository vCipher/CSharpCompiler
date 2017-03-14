using CSharpCompiler.CodeGen.Sections.Text;
using CSharpCompiler.CodeGen.Metadata.Heaps;

namespace CSharpCompiler.CodeGen.Metadata
{
    public sealed class MetadataContainer
    {
        public TableHeap Tables { get; private set; }
        public StringHeap Strings { get; private set; }
        public BlobHeap Blobs { get; private set; }
        public GuidHeap Guids { get; private set; }
        public UserStringHeap UserStrings { get; private set; }
        public ILCodeBuffer ILCode { get; private set; }

        public MetadataContainer(ILCodeBuffer ilCode)
        {
            Tables = new TableHeap(this);
            Strings = new StringHeap();
            Blobs = new BlobHeap();
            Guids = new GuidHeap();
            UserStrings = new UserStringHeap();
            ILCode = ilCode;
        }
    }
}
