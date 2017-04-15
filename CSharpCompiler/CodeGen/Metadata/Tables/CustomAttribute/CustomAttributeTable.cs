using CSharpCompiler.Utility;
using System.Collections.ObjectModel;

namespace CSharpCompiler.CodeGen.Metadata.Tables.CustomAttribute
{
    public sealed class CustomAttributeTable : MetadataTable<Semantics.Metadata.CustomAttribute, CustomAttributeRow>
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
            Add(attribute, CreateCustomAttributeRow(attribute));
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.CustomAttribute;
        }

        private CustomAttributeRow CreateCustomAttributeRow(Semantics.Metadata.CustomAttribute attribute)
        {
            return new CustomAttributeRow()
            {
                Parent = _metadata.GetCodedRid(attribute.Owner, CodedTokenType.HasCustomAttribute),
                Type = _metadata.GetCodedRid(attribute.Constructor, CodedTokenType.CustomAttributeType),
                Value = _metadata.WriteSignature(attribute)
            };
        }
    }
}
