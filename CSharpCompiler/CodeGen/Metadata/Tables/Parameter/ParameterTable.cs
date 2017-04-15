using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Parameter
{
    public sealed class ParameterTable : MetadataTable<ParameterDefinition, ParameterRow>
    {
        private MetadataBuilder _metadata;

        public ParameterTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public ushort AddRange(Collection<ParameterDefinition> parameters)
        {
            ushort start = Position;
            foreach (ParameterDefinition parameterDef in parameters)
            {
                Add(parameterDef, CreateParamRow(parameterDef));
            }
            return start;
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.Param;
        }

        private ParameterRow CreateParamRow(ParameterDefinition parameterDef)
        {
            return new ParameterRow()
            {
                Attributes = parameterDef.Attributes,
                Sequence = (ushort)(parameterDef.Index + 1),
                Name = _metadata.RegisterString(parameterDef.Name)
            };
        }
    }
}
