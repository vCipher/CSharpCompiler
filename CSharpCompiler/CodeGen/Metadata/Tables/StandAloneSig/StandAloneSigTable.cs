using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpCompiler.CodeGen.Metadata.Tables.StandAloneSig
{
    public sealed class StandAloneSigTable : MetadataTable<StandAloneSigRow>
    {
        private Dictionary<SingatureBuffer, MetadataToken> _standAloneSigMap;
        private MetadataBuilder _metadata;

        public StandAloneSigTable(MetadataBuilder metadata)
        {
            _standAloneSigMap = new Dictionary<SingatureBuffer, MetadataToken>(new ByteBufferComparer());
            _metadata = metadata;
        }

        public MetadataToken GetStandAloneSigToken(Collection<VariableDefinition> variables)
        {
            SingatureBuffer signature = SingatureBuffer.GetVariablesSignature(variables);
            return GetStandAloneSigToken(signature);
        }

        public MetadataToken GetStandAloneSigToken(SingatureBuffer signature)
        {
            MetadataToken token;
            if (_standAloneSigMap.TryGetValue(signature, out token))
                return token;

            uint rid = Add(CreateStandAloneSigRow(signature));
            token = new MetadataToken(MetadataTokenType.Signature, rid);
            _standAloneSigMap.Add(signature, token);

            return token;
        }

        private StandAloneSigRow CreateStandAloneSigRow(SingatureBuffer signature)
        {
            return new StandAloneSigRow()
            {
                Signature = _metadata.RegisterBlob(signature)
            };
        }
    }
}
