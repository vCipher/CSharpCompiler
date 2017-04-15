using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Method
{
    public sealed class MethodTable : MetadataTable<MethodDefinition, MethodRow>
    {
        private MetadataBuilder _metadata;

        public MethodTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public ushort AddRange(Collection<MethodDefinition> methods)
        {
            ushort start = Position;
            foreach (MethodDefinition methodDef in methods.EmptyIfNull())
            {
                Add(methodDef, CreateMethodRow(methodDef));
            }
            return start;
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.Method;
        }

        private MethodRow CreateMethodRow(MethodDefinition methodDef)
        {
            return new MethodRow()
            {
                RVA = _metadata.WriteMethod(methodDef),
                ImplAttributes = methodDef.ImplAttributes,
                Attributes = methodDef.Attributes,
                Name = _metadata.RegisterString(methodDef.Name),
                Signature = _metadata.WriteSignature(methodDef),
                ParamList = _metadata.ParameterTable.AddRange(methodDef.Parameters)
            };
        }
    }
}
