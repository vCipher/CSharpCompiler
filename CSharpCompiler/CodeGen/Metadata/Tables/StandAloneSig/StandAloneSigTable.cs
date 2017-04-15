using CSharpCompiler.Semantics.Metadata;
using System.Collections.ObjectModel;

namespace CSharpCompiler.CodeGen.Metadata.Tables.StandAloneSig
{
    public sealed class StandAloneSigTable : MetadataTable<StandAloneSignature, StandAloneSigRow>
    {
        private MetadataBuilder _metadata;

        public StandAloneSigTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken GetStandAloneSigToken(Collection<VariableDefinition> variables)
        {
            StandAloneSignature signature = StandAloneSignature.GetVariablesSignature(variables);
            return GetToken(signature);
        }

        public override MetadataToken GetToken(StandAloneSignature signature)
        {
            MetadataToken token;
            if (TryGetToken(signature, out token))
                return token;

            uint rid = Add(signature, CreateStandAloneSigRow(signature));
            return new MetadataToken(GetTokenType(), rid);
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.Signature;
        }

        private StandAloneSigRow CreateStandAloneSigRow(StandAloneSignature signature)
        {
            return new StandAloneSigRow()
            {
                Signature = _metadata.RegisterBlob(signature)
            };
        }
    }
}
