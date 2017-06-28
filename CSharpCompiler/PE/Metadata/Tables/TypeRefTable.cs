using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct TypeRefRow
    {
        public readonly MetadataToken ResolutionScope;
        public readonly uint Name;
        public readonly uint Namespace;

        public TypeRefRow(MetadataToken resolutionScope, uint name, uint @namespace) : this()
        {
            ResolutionScope = resolutionScope;
            Name = name;
            Namespace = @namespace;
        }
    }

    public sealed class TypeRefTable : MetadataTable<TypeRefRow>
    {
        public TypeRefTable() : base() { }
        public TypeRefTable(int count) : base(count) { }
        
        protected override TypeRefRow ReadRow(TableHeapReader heap)
        {
            return new TypeRefRow(
                heap.ReadCodedToken(CodedTokenType.ResolutionScope),
                heap.ReadStringOffset(),
                heap.ReadStringOffset()
            );
        }

        protected override void WriteRow(TypeRefRow row, TableHeapWriter heap)
        {
            heap.WriteCodedToken(row.ResolutionScope, CodedTokenType.ResolutionScope);
            heap.WriteString(row.Name);
            heap.WriteString(row.Namespace);
        }
    }
}
