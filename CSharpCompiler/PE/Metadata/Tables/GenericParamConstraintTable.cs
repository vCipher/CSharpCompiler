using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct GenericParamConstraintRow
    {
        public readonly MetadataToken Owner;
        public readonly MetadataToken Constraint;

        public GenericParamConstraintRow(MetadataToken owner, MetadataToken constraint) : this()
        {
            Owner = owner;
            Constraint = constraint;
        }
    }

    public sealed class GenericParamConstraintTable : MetadataTable<GenericParamConstraintRow>
    {
        public GenericParamConstraintTable() : base() { }
        public GenericParamConstraintTable(int count) : base(count) { }

        protected override GenericParamConstraintRow ReadRow(TableHeapReader heap)
        {
            return new GenericParamConstraintRow(
                heap.ReadToken(MetadataTokenType.GenericParam),
                heap.ReadCodedToken(CodedTokenType.TypeDefOrRef)
            );
        }

        protected override void WriteRow(GenericParamConstraintRow row, TableHeapWriter heap)
        {
            heap.WriteToken(row.Owner);
            heap.WriteCodedToken(row.Constraint, CodedTokenType.TypeDefOrRef);
        }
    }
}