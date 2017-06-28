﻿using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public struct FieldMarshalRow
    {
        public readonly MetadataToken Parent;
        public readonly uint NativeType;

        public FieldMarshalRow(MetadataToken parent, uint nativeType) : this()
        {
            Parent = parent;
            NativeType = nativeType;
        }
    }

    public sealed class FieldMarshalTable : MetadataTable<FieldMarshalRow>
    {
        public FieldMarshalTable() : base() { }
        public FieldMarshalTable(int count) : base(count) { }

        protected override FieldMarshalRow ReadRow(TableHeapReader heap)
        {
            return new FieldMarshalRow(
                heap.ReadCodedToken(CodedTokenType.HasFieldMarshal),
                heap.ReadBlobOffset()
            );
        }

        protected override void WriteRow(FieldMarshalRow row, TableHeapWriter heap)
        {
            heap.WriteCodedToken(row.Parent, CodedTokenType.HasFieldMarshal);
            heap.WriteBlob(row.NativeType);
        }
    }
}
