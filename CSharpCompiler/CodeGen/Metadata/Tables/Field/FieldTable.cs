using System;
using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Field
{
    public sealed class FieldTable : MetadataTable<FieldDefinition, FieldRow>
    {
        private MetadataBuilder _metadata;

        public FieldTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public ushort AddRange(Collection<FieldDefinition> fields)
        {
            ushort start = Position;
            foreach (FieldDefinition fieldDef in fields.EmptyIfNull())
            {
                Add(fieldDef, CreateFieldRow(fieldDef));
            }
            return start;
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.Field;
        }

        private FieldRow CreateFieldRow(FieldDefinition fieldDef)
        {
            throw new NotImplementedException();
        }
    }
}
