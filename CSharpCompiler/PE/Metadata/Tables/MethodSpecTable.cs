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

        protected override MethodSpecRow ReadRow(TableHeap heap)
        {
            return new MethodSpecRow(
                heap.ReadCodedToken(CodedTokenType.MethodDefOrRef),
                heap.ReadBlob()
            );
        }

        protected override void WriteRow(MethodSpecRow row, TableHeap heap)
        {
            heap.WriteCodedToken(row.Method, CodedTokenType.MethodDefOrRef);
            heap.WriteBlob(row.Instantiation);
        }
    }
}