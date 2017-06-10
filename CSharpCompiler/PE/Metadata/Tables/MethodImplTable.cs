using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct MethodImplRow
    {
        public readonly MetadataToken Class;
        public readonly MetadataToken MethodBody;
        public readonly MetadataToken MethodDeclaration;

        public MethodImplRow(MetadataToken @class, MetadataToken methodBody, MetadataToken methodDeclaration) : this()
        {
            Class = @class;
            MethodBody = methodBody;
            MethodDeclaration = methodDeclaration;
        }
    }

    public sealed class MethodImplTable : MetadataTable<MethodImplRow>
    {
        public MethodImplTable() : base() { }
        public MethodImplTable(int count) : base(count) { }

        protected override MethodImplRow ReadRow(TableHeap heap)
        {
            return new MethodImplRow(
                heap.ReadToken(MetadataTokenType.TypeDef),
                heap.ReadCodedToken(CodedTokenType.MethodDefOrRef),
                heap.ReadCodedToken(CodedTokenType.MethodDefOrRef)
            );
        }

        protected override void WriteRow(MethodImplRow row, TableHeap heap)
        {
            heap.WriteToken(row.Class);
            heap.WriteCodedToken(row.MethodBody, CodedTokenType.MethodDefOrRef);
            heap.WriteCodedToken(row.MethodDeclaration, CodedTokenType.MethodDefOrRef);
        }
    }
}

