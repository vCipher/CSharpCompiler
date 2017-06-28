using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct InterfaceImplRow
    {
        public readonly MetadataToken Class;
        public readonly MetadataToken Interface;

        public InterfaceImplRow(MetadataToken @class, MetadataToken @interface) : this()
        {
            Class = @class;
            Interface = @interface;
        }
    }

    public sealed class InterfaceImplTable : MetadataTable<InterfaceImplRow>
    {
        public InterfaceImplTable() : base() { }
        public InterfaceImplTable(int count) : base(count) { }

        protected override InterfaceImplRow ReadRow(TableHeapReader heap)
        {
            return new InterfaceImplRow(
                heap.ReadToken(MetadataTokenType.TypeDef),
                heap.ReadCodedToken(CodedTokenType.TypeDefOrRef)
            );
        }

        protected override void WriteRow(InterfaceImplRow row, TableHeapWriter heap)
        {
            heap.WriteToken(row.Class);
            heap.WriteCodedToken(row.Interface, CodedTokenType.TypeDefOrRef);
        }
    }
}
