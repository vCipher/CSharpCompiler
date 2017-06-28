using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct MethodSpecRow
    {
        public readonly MetadataToken Method;
        public readonly uint Instantiation;

        public MethodSpecRow(MetadataToken method, uint instantiation) : this()
        {
            Method = method;
            Instantiation = instantiation;
        }
    }

    public sealed class MethodSpecTable : MetadataTable<MethodSpecRow>
    {
        public MethodSpecTable() : base() { }
        public MethodSpecTable(int count) : base(count) { }

        protected override MethodSpecRow ReadRow(TableHeapReader heap)
        {
            return new MethodSpecRow(
                heap.ReadCodedToken(CodedTokenType.MethodDefOrRef),
                heap.ReadBlobOffset()
            );
        }

        protected override void WriteRow(MethodSpecRow row, TableHeapWriter heap)
        {
            heap.WriteCodedToken(row.Method, CodedTokenType.MethodDefOrRef);
            heap.WriteBlob(row.Instantiation);
        }
    }
}