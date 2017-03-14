using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Parameter
{
    public sealed class ParameterTable : MetadataTable<ParameterRow>
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
                uint rid = Add(CreateParamRow(parameterDef));
                parameterDef.ResolveToken(rid);
            }
            return start;
        }

        private ParameterRow CreateParamRow(ParameterDefinition parameterDef)
        {
            ParameterRow row = new ParameterRow();
            row.Attributes = parameterDef.Attributes;
            row.Sequence = (ushort)(parameterDef.Index + 1);
            row.Name = _metadata.RegisterString(parameterDef.Name);

            return row;
        }
    }
}
