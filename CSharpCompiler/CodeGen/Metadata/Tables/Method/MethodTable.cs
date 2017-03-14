using System;
using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Method
{
    public sealed class MethodTable : MetadataTable<MethodRow>
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
                uint rid = Add(CreateMethodRow(methodDef));
                methodDef.ResolveToken(rid);
            }
            return start;
        }

        private MethodRow CreateMethodRow(MethodDefinition methodDef)
        {
            MethodRow row = new MethodRow();
            row.RVA = _metadata.WriteMethod(methodDef);
            row.ImplAttributes = methodDef.ImplAttributes;
            row.Attributes = methodDef.Attributes;
            row.Name = _metadata.RegisterString(methodDef.Name);
            row.Signature = _metadata.WriteSignature(methodDef);
            row.ParamList = _metadata.ParameterTable.AddRange(methodDef.Parameters);

            return row;
        }
    }
}
