using CSharpCompiler.Utility;
using System.Collections.ObjectModel;

namespace CSharpCompiler.CodeGen.Metadata.Tables.CustomAttribute
{
    public sealed class CustomAttributeTable : MetadataTable<CustomAttributeRow>
    {
        private MetadataBuilder _metadata;

        public CustomAttributeTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void AddRange(Collection<Semantics.Metadata.CustomAttribute> attributes)
        {
            attributes.EmptyIfNull().ForEach(attr => Add(attr));
        }

        public void Add(Semantics.Metadata.CustomAttribute attribute)
        {
            uint rid = Add(CreateCustomAttributeRow(attribute));
            attribute.ResolveToken(rid);
        }

        private CustomAttributeRow CreateCustomAttributeRow(Semantics.Metadata.CustomAttribute attribute)
        {
            CustomAttributeRow row = new CustomAttributeRow();
            row.Parent = _metadata.GetCodedRID(attribute.Owner, CodedTokenType.HasCustomAttribute);
            row.Type = _metadata.GetCodedRID(attribute.Constructor, CodedTokenType.CustomAttributeType);
            row.Value = _metadata.WriteSignature(attribute);

            return row;
        }
    }
}
